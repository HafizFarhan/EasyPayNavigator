

namespace Installment.Helpers.Interfaces
{
    public interface IPageModelHelper
    {
        IConfiguration Configuration { get; }
        IHttpContextAccessor HttpContextAccessor { get; }
        //IApiHttpClient ApiHttpClient { get; }
        //TenantInfo TenantInfo { get; }

        //IApiEntityClient<TDTO> GetApiEntityClient<TDTO>(ApiController apiController) where TDTO : class;
    }
}