
using Installment.Helpers.Interfaces;
using Installment.Pages.PageModels;
using static Installment.Pages.IndexModel;

namespace Installment.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IPageModelHelper pageModelHelper;


        public IndexModel(IPageModelHelper pageModelHelper)
            : base(pageModelHelper)
        {
            this.pageModelHelper = pageModelHelper;
        }
       
             

        

       

        
    }
    
    
}
