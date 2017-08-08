using DXzonghejiaofei.Models.Request;
using DXzonghejiaofei.Models.Response;

namespace DXzonghejiaofei.BLL
{
    public interface ITopClient
    {
        TopResponse<T> Execute<T>(ITopRequest request);
    }
}