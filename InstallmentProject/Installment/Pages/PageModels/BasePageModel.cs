using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;

using Installment.Helpers.Interfaces;

namespace Installment.Pages.PageModels
{
    public class BasePageModel : PageModel
    {
        private readonly IPageModelHelper pageModelHelper;
        //private readonly DataSourceManager dataSourceManager;
        public string GoogleAuthenticationPage { get; }

        public BasePageModel(IPageModelHelper pageModelHelper)
        {
            this.pageModelHelper = pageModelHelper;
            GoogleAuthenticationPage = pageModelHelper.Configuration["Authentication:Google:RedirectUri"] ?? throw new ArgumentNullException("GoogleAuthenticationPage");
            //dataSourceManager = new DataSourceManager(pageModelHelper);
        }

        //public string AddOrUpdateQueryParam(string paramName, string paramValue)
        //{
        //    return UrlManager.AddOrUpdateQueryParam(HttpContext, HttpContext.Request.GetEncodedUrl(), paramName, paramValue);
        //}

        //public string RemoveQueryParam(string paramName)
        //{
        //    return UrlManager.RemoveQueryParam(HttpContext, HttpContext.Request.GetEncodedUrl(), paramName);
        //}

        //public async Task<int?> GetCurrentDataSourceIdAsync()
        //{
        //    int dataSourceId = await dataSourceManager.GetDataSourceIdAsync();
        //    if (dataSourceId <= 0)
        //        return null;
        //    return dataSourceId;
        //}

        //public async Task<DateTime?> GetLastImportDateAsync()
        //{
        //    DateTime? lastImportDate = await dataSourceManager.GetLastImportDateAsync();
        //    return lastImportDate;
        //}

        //public async Task<bool> CheckIfGoogleAccessTokenIsExpiredAsync()
        //{
        //    try
        //    {
        //        var response = await pageModelHelper.ApiHttpClient.GetAsync<bool>(CoreClasses.Enums.ApiController.Google, "IsAccessTokenExpired");
        //        return response.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw;
        //    }
        //}


    }
}
