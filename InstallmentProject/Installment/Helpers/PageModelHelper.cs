
using Installment.Helpers.Interfaces;

namespace Installment.Helpers
{
    public class PageModelHelper : IPageModelHelper
    {


        public PageModelHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            
        }

        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        //public IApiHttpClient ApiHttpClient { get; }
        //public TenantInfo TenantInfo { get; }

        //public IApiEntityClient<TDTO> GetApiEntityClient<TDTO>(ApiController apiController) where TDTO : class
        //{
        //    return new ApiEntityClient<TDTO>(ApiHttpClient, Configuration, apiController);
        //}
    }
}
