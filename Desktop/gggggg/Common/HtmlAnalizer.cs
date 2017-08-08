using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace DXzonghejiaofei.Common
{
    public class HtmlAnalizer
    {
        public static string GetInputValue(string html,string inputName)
        {
            var doc = new HtmlDocument();

            try
            {
                doc.LoadHtml(html);
                var node = doc.DocumentNode.SelectSingleNode($"//input[@name='{inputName}']");
                var attr = node?.Attributes["value"];
                return attr?.Value;
            }
            catch(Exception ex)
            {
                Logger.Warn(ex.ToString());
            }

            return null;
        }

        public static Dictionary<string,string> GetInputValue(string html,IEnumerable<string> inputNames)
        {
            var dict = new Dictionary<string,string>();
            var doc = new HtmlDocument();

            try
            {
                doc.LoadHtml(html);
                foreach(var inputName in inputNames)
                {
                    var node = doc.DocumentNode.SelectSingleNode($"//input[@name='{inputName}']");
                    var attr = node?.Attributes["value"];
                    dict.Add(inputName,attr?.Value);
                }
            }
            catch(Exception ex)
            {
                Logger.Warn(ex.ToString());
            }

            return dict;
        }
    }
}
