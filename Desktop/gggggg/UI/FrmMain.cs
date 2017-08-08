using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DXzonghejiaofei.BLL;
using DXzonghejiaofei.BLL.Consumer;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.Enum;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;
using DXzonghejiaofei.Models.Response;
using DXzonghejiaofei.Properties;
using GoodsType = DXzonghejiaofei.Enum.GoodsType;
using Newtonsoft.Json.Linq;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace DXzonghejiaofei.UI
{
    public partial class FrmMain:Form
    {
        #region 成员字段

        private static List<FlowOrder> _orderList=new List<FlowOrder>(); 
        private static readonly BindingSource OrderBindSource = new BindingSource();
        private CancellationTokenSource _cts;
        private CancellationToken _token;
        private static SystemConfig _sysConfig;
        public static UserConfig _usrConfig;
        private static List<ManualResetEvent> _manualEvents;
        private static string _callbackUrl;
        private static string provice;
        #endregion

        #region 委托声明
        private delegate void PrintLogDelegate(string logs);

        private delegate void SetButtonDelegate(Button btn,bool enabled);
        private delegate void UpdateBalanceDelegate(string balance);
        #endregion

        #region 构造方法
        public FrmMain()
        {
            InitializeComponent();
        }  
        #endregion

        #region UI事件处理
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender,EventArgs e)
        {
            //初始化界面显示
            Text=ConfigurationManager.AppSettings["appName"];
            notifyIcon1.Text=Text;
            notifyIcon1.Visible=false;
            OrderBindSource.DataSource = _orderList;
            try
            {
                Startup.Configuration(ConfigurationManager.AppSettings["callbackUrl"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请使用管理员打开");
                Close();

            }
            //检查数据库文件是否存在
            if (!File.Exists("sqlite.db"))
            {
                MessageBox.Show(Resources.DbNotFoundMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            provice= ConfigurationManager.AppSettings["Province"];
            
            GetInit();
        }

        private void GetInit()
        {
            //读取系统配置
            var sysconfig = SystemConfigManager.Get();
            if (!sysconfig.IsSuccess)
            {
                MessageBox.Show(sysconfig.Message, Resources.TipMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _sysConfig = sysconfig.DataView;

            //初始化系统内配置
            DefaultTopClient.Init(_sysConfig.ServerUrl, _sysConfig.ChannelId, _sysConfig.Secretkey);

            //读取用户配置
            var usrConfig = UserConfigManager.Get();
            if (!usrConfig.IsSuccess)
            {
                MessageBox.Show(usrConfig.Message, Resources.TipMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                _usrConfig = usrConfig.DataView;
                Consumer.Init(_usrConfig, _callbackUrl, provice);
        }
        /// <summary>
        /// 超链接公司主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslbcompany_Click(object sender,EventArgs e)
        {
            Process.Start(@"C:\Program Files\Internet Explorer\IEXPLORE.EXE","http://ejiaofei.com/");
        }

        /// <summary>
        /// 点击开始充值按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender,EventArgs e)
        {
            btnPause.Enabled=true;
            btnStart.Enabled=false;
            _manualEvents = new List<ManualResetEvent>();

            _cts=new CancellationTokenSource();
            _token = _cts.Token;

            var mre1 = new ManualResetEvent(false);
            _manualEvents.Add(mre1);
            var getOrderThread = new Thread(StartToGetOrders) { IsBackground = true };
            getOrderThread.Start(mre1);

            var mre2 = new ManualResetEvent(false);
            _manualEvents.Add(mre2);
            var submitOrderThread = new Thread(StartToSubmitOrder) { IsBackground = true };
            submitOrderThread.Start(mre2);
            
            var mre3 = new ManualResetEvent(false);
            _manualEvents.Add(mre3);
            var dealOrderThread = new Thread(StartToDealOrder) { IsBackground = true };
            dealOrderThread.Start(mre3);

            var mre4 = new ManualResetEvent(false);
            _manualEvents.Add(mre4);
            var queryOrderThread = new Thread(StartToQueryOrders) { IsBackground = true };
            queryOrderThread.Start(mre4);

            #region 余额线程
            var mre5 = new ManualResetEvent(false);
            _manualEvents.Add(mre5);
            var querybalanceThread = new Thread(QueryAgentBalance) { IsBackground = true };
            querybalanceThread.Start(mre5);
            #endregion

        }

        /// <summary>
        /// 点击暂停充值按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender,EventArgs e)
        {
            if (_token.IsCancellationRequested)
            {
                return;
            }
            _cts.Cancel();

            var waitThread=new Thread(() =>
            {
                // ReSharper disable once CoVariantArrayConversion
                WaitHandle.WaitAll(_manualEvents.ToArray());
                OnSetButton(btnPause,false);
                OnSetButton(btnStart,true);
            });
            waitThread.Start();
        }

        /// <summary>
        /// 点击系统配置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfigure_Click(object sender,EventArgs e)
        {
            var frmConfig = new FrmConfig(_sysConfig,_usrConfig);
            frmConfig.ShowDialog();
            GetInit();
        }

        /// <summary>
        /// 实现NotifyIcon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_SizeChanged(object sender,EventArgs e)
        {
            if(WindowState!=FormWindowState.Minimized) return;
            ShowInTaskbar=false;
            notifyIcon1.Visible=true;
        }

        /// <summary>
        /// 实现NotifyIcon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_Click(object sender,EventArgs e)
        {
            if(WindowState!=FormWindowState.Minimized) return;
            Show();
            WindowState=FormWindowState.Normal;
            notifyIcon1.Visible=false;
            ShowInTaskbar=true;
        }

        private void FrmMain_FormClosing(object sender,FormClosingEventArgs e)
        {
            if(_cts!=null&&!_cts.IsCancellationRequested)
            {
                MessageBox.Show(Resources.PausMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                e.Cancel=true;
            }
            else if (btnPause.Enabled)
            {
                MessageBox.Show(Resources.ClosingMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                e.Cancel=true;
            }
            else if (TopQueue<FlowOrder>.SubmitOrders.Count > 0 || TopQueue<FlowOrder>.DealOrders.Count > 0)
            {
                if (DialogResult.OK !=MessageBox.Show(Resources.CloseMsg, Resources.TipMsg, MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if(!notifyIcon1.Visible)
                {
                    notifyIcon1.Visible=true;
                }
                notifyIcon1.ShowBalloonTip(5*1000,"提示",$"{Text}已退出!",ToolTipIcon.None);
            }
        }
        #endregion

        #region 业务处理
        /// <summary>
        /// 获取订单
        /// </summary>
        private void StartToGetOrders(object obj)
        {
            OnPrintLog("获取订单线程已开启");
            while(!_token.IsCancellationRequested)
            {
                try
                {
                    var getOrdersResponse = Producer<FlowOrder>.GetOrders(GoodsType.FlowGoods);
                    if (!getOrdersResponse.IsSuccess)
                    {
                        OnPrintLog($"{getOrdersResponse.Message}");
                        continue;
                    }
                    if (getOrdersResponse.DataView.Data == null||getOrdersResponse.DataView.Data.Orders.Count<=0)
                    {
                        continue;
                    }

                    foreach (var order in getOrdersResponse.DataView.Data.Orders)
                    {
                        order.StartTime=DateTime.Now;                    
                        var addOrder = OrderManager<FlowOrder>.Add(order);
                        if (!addOrder.IsSuccess)
                        {
                            OnPrintLog($"{order}|{addOrder.Message}");
                            continue;
                        }

                        order.Id = long.Parse(addOrder.DataView.ToString());
                        TopQueue<FlowOrder>.SubmitOrders.Enqueue(order);
                        OnPrintLog($"{order}|进入提交订单队列");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString());
                }
                finally
                {
                    Thread.Sleep(5*1000);
                }
            }
            var mre = (ManualResetEvent)obj;
            mre.Set();
            OnPrintLog("获取订单线程已停止");
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        private void StartToSubmitOrder(object obj)
        {
            OnPrintLog("提交订单线程已开启");
            while (!_token.IsCancellationRequested)
            {
                FlowOrder order;
                if (TopQueue<FlowOrder>.SubmitOrders.TryDequeue(out order))
                {
                    ThreadPool.QueueUserWorkItem(SubmitOrdersHandle,(FlowOrder)order);
                }
                Thread.Sleep(5000);
            }
            var mre = (ManualResetEvent)obj;
            mre.Set();
            OnPrintLog("提交订单线程已停止");
        }

        private void SubmitOrdersHandle(object paramOrder)
        {
            FlowOrder order = (FlowOrder)paramOrder;
            try{
                var confirmOrder = Producer<FlowOrder>.RechargeConfirm(order.OrderId);
                if (!confirmOrder.IsSuccess)
                {
                    TopQueue<FlowOrder>.SubmitOrders.Enqueue(order);
                    OnPrintLog($"{order}|{confirmOrder.Message}|重新进入提交订单队列");
                    Thread.Sleep(3000);
                    return ;
                }
                if (!confirmOrder.DataView.Data.Res){
                    order.EndTime = DateTime.Now;
                    order.State = OrderState.Fail.ToString();
                    order.Remark = "充值确认不可充值放弃处理";
                    TopQueue<FlowOrder>.DealOrders.Enqueue(order);
                    OnPrintLog($"{order}|进入处理订单队列");
                    return ;
                }
                //////////////////真正的提交/////////////////
                var consumer = new Consumer(order);
                var submitOrderRespResult = consumer.SubmitOrder();
                order.EndTime = DateTime.Now;//提单时间
                order.Remark = submitOrderRespResult.Message;
                if (submitOrderRespResult.IsSuccess == false){
                    order.EndTime = DateTime.Now;
                    TopQueue<FlowOrder>.QueryOrders.Enqueue(order);
                    OnPrintLog($"[订单号]{order.OrderId}|提交订单异常进入查询队列|{submitOrderRespResult.Message}");
                }else{
                    JObject submitOrderRespJsonObj = JObject.Parse(submitOrderRespResult.DataView.ResponseReport);
                    bool IsError = submitOrderRespJsonObj["IsError"].Value<bool>();
                    string RespMessage = submitOrderRespJsonObj["Message"].Value<string>();
                    //提交成功false,请求失败true
                    if (IsError == false){
                            order.EndTime = DateTime.Now;
                            order.State = OrderState.Success.ToString();
                            order.Remark = submitOrderRespResult.Message;
                            TopQueue<FlowOrder>.QueryOrders.Enqueue(order);
                            OnPrintLog($"[订单号]{order.OrderId}|提交订单成功|进入查询队列");
                    }else{
                        order.EndTime = DateTime.Now;
                        order.State = OrderState.Fail.ToString();
                        order.Remark = submitOrderRespResult.Message;
                        TopQueue<FlowOrder>.DealOrders.Enqueue(order);
                        OnPrintLog($"[订单号]{order.OrderId}|提交失败进入处理队列|{RespMessage}");
                        return;
                    }
                }
            }catch (Exception e){
                OnPrintLog($"[订单号]{order.OrderId}|{"上游网络暂时不通"},提单异常");
            }
        }

        /// <summary>
        /// 从查询队列中取出订单到供货商平台进行查询
        /// 已有最终结果的反馈到业务平台
        /// </summary>
        private void StartToQueryOrders(object obj)
        {
            OnPrintLog("查询订单线程已开启");
            while (!_token.IsCancellationRequested){
                FlowOrder order;
                if (TopQueue<FlowOrder>.QueryOrders.TryDequeue(out order)){
                    try{
                        var interval = (DateTime.Now - order.EndTime).TotalMilliseconds;
                        if (interval < 30000){
                            Thread.Sleep(Convert.ToInt32(30000 - interval));
                        }
                        //查询订单状态
                        var queryOrderRespResult = new Consumer(order).QueryOrder();
                        //JObject queryJsonObj = JObject.Parse(queryOrderRespResult.DataView);
                      //  bool respSuccess = queryOrderRespResult["IsError"].Value<bool>();
                        if (queryOrderRespResult.IsSuccess == false){
                            TopQueue<FlowOrder>.QueryOrders.Enqueue(order);
                            OnPrintLog($"[订单号]{order.OrderId}|提交订单异常进入查询队列");
                        }else{
                             var  rechargeStatus = queryOrderRespResult.DataView.Data.Status;
                            // 订单充值状态:5-充值失败(异常分析)，10-充值成功，1-充值中
                            switch (rechargeStatus) {
                                case "10":
                                    order.EndTime = DateTime.Now;
                                    order.State = OrderState.Success.ToString();
                                    TopQueue<FlowOrder>.DealOrders.Enqueue(order);
                                    OnPrintLog($"[订单号]{order.OrderId}|上游消息:充值成功");
                                    break;
                                case "5":
                                    order.EndTime = DateTime.Now;
                                    order.State = OrderState.Fail.ToString();
                                    order.Remark = "充值失败";
                                    TopQueue<FlowOrder>.DealOrders.Enqueue(order);
                                    OnPrintLog($"[订单号]{order.OrderId}|上游消息:充值失败");
                                    break;
                                case "1":
                                    order.State = OrderState.Unknown.ToString();
                                    TopQueue<FlowOrder>.QueryOrders.Enqueue(order);
                                    OnPrintLog($"[订单号]{order.OrderId}|上游消息:充值中-继续查单");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }catch (Exception e){
                        TopQueue<FlowOrder>.QueryOrders.Enqueue(order);
                        OnPrintLog($"[订单号]{order.OrderId}|查询异常进入查询队列|{e.ToString()}");
                    }
                }
                Thread.Sleep(1000);
            }
            var mre = (ManualResetEvent)obj;
            mre.Set();
            OnPrintLog("查询订单线程已暂停");
        }

        /// <summary>
        /// 从充值队列中取出订单提交到供货商平台
        /// 加入查询队列
        /// </summary>
        private void StartToDealOrder(object obj)
        {
            OnPrintLog("处理订单线程已开启");
            while(!_token.IsCancellationRequested)
            {
                FlowOrder order;
                if(TopQueue<FlowOrder>.DealOrders.TryDequeue(out order))
                {
                    if (order.State==null)
                    {
                        throw new Exception("订单没有状态");
                    }
                    var updateOrder = OrderManager<FlowOrder>.Update(order);                   
                    OnPrintLog($"{order}|{updateOrder.Message}");

                    var dealOrder = Producer<FlowOrder>.Result(order.OrderId,order.State.ToString());
                    OnPrintLog($"{order}|{dealOrder.DataView}");
                    continue;
                }
                Thread.Sleep(1000);
            }
            var mre = (ManualResetEvent)obj;
            mre.Set();
            OnPrintLog("处理订单线程已停止");
        }

        /// <summary>
        /// 查询代理商余额
        /// </summary>
        private void QueryAgentBalance(object obj)
        {
            OnPrintLog("查询余额线程已开启");
            while (!_token.IsCancellationRequested)
            {
                
                var queryResult = Consumer.QueryBalance();
                if (!queryResult.IsSuccess)
                {
                    OnPrintLog($"查询代理商余额异常:{queryResult.Message}");
                    continue;
                }
                try{
                    JObject queryBalanceJsonObj = JObject.Parse(queryResult.DataView.ResponseReport);
                    bool respSuccess = queryBalanceJsonObj["IsError"].Value<bool>();
                    //int Message = queryBalanceJsonObj["Message"].Value<int>();
                    if (respSuccess == false)
                    {
                        var balance = queryBalanceJsonObj["Data"]["BanancePrice"].Value<decimal>();
                        var CreditPrice = queryBalanceJsonObj["Data"]["CreditPrice"].Value<decimal>();
                        var SMSCount = queryBalanceJsonObj["Data"]["SMSCount"].Value<int>();
                        OnPrintLog($"[余额查询]代理商余额:{balance},信用额度:{CreditPrice},通知短信剩余条数:{SMSCount}");
                        //Invoke(new UpdateBalanceDelegate(UpdateBalance), balance);
                    }
                }
                catch (Exception ex)
                {
                    OnPrintLog($"查询代理商余额异常:{ex.Message}");
                }
                finally {
                    Thread.Sleep(80000);
                }
                
            }
            var mre = (ManualResetEvent)obj;
            mre.Set();
            OnPrintLog("查询代理商余额线程已停止");
        }

       /* private void UpdateBalance(string balance)
        {
            grpBalance.Text ="余额-"+ DateTime.Now.ToLocalTime();
            lblBalance.Text = balance;
        }*/
        #endregion

        #region 实现委托

        /// <summary>
        /// 将信息输出到界面并且记录到log
        /// </summary>
        /// <param name="logs"></param>
        public void PrintLog(string logs)
        {
            if(rtxtLog.TextLength>5000) rtxtLog.Clear(); //避免消耗内存
            rtxtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}-{logs}\r\n");
            Logger.Info(logs);
        }

        /// <summary>
        /// 实现打印log的委托
        /// </summary>
        /// <param name="logs"></param>
        public void OnPrintLog(string logs)
        {
            Invoke(new PrintLogDelegate(PrintLog),logs);
        }

        public void SetButton(Button btn,bool enabled)
        {
            btn.Enabled = enabled;
        }

        public void OnSetButton(Button btn,bool enabled)
        {
            Invoke(new SetButtonDelegate(SetButton),btn,enabled);
        }
        #endregion

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.rtxtLog.Clear();
        }
   
      /*  private void btnQueryBalance_Click(object sender, EventArgs e)
        {
            var queryResult = Consumer.QueryBalance();
            OnPrintLog(queryResult.DataView.ResponseReport);
        }*/
    }
}
