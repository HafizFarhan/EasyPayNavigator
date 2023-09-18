
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
        public class TopNotification
        {
            public eTopNotification Type { get; }
            public string Title { get; }
            public string Content { get; }
            public string Action { get; }
            public string ActionText { get; }

            public TopNotification(eTopNotification type, string title, string content, string action, string actionText)
            {
                Type = type;
                Title = title;
                Content = content;
                Action = action;
                ActionText = actionText;
            }
        }
        public enum eTopNotification
        {
            Primary,
            Secondary,
            Success,
            Danger,
            Warning
        }

        public List<int> SalesData { get; set; }
        public List<int> TrafficData { get; set; }
        public List<int> ProductData { get; set; }
        public List<int> SourceData { get; set; }
        public List<int> RevenueData { get; set; }
        public List<int> AbandonedCartData { get; set; }
        public TopNotification Notification { get; set; }

        public void OnGet()
        {
            // Generate fake data for analytics
            SalesData = new List<int> { 10, 15, 25, 30, 35, 45, 50 };
            TrafficData = new List<int> { 500, 700, 900, 1200, 1500, 2000, 2500 };
            ProductData = new List<int> { 20, 30, 40, 50, 60, 70, 80 };
            SourceData = new List<int> { 30, 20, 10, 15, 5, 20 };
            RevenueData = new List<int> { 5000, 7000, 9000, 12000, 15000, 20000, 25000 };
            AbandonedCartData = new List<int> { 5, 8, 10, 12, 15, 18, 20 };
            //notification object
            Notification = new TopNotification(eTopNotification.Warning, "Confirmation Needed",
                "Your email address still needs confirmation. Confirm your email to secure your account and unlock the full potential of our cutting-edge AI tools.",
                "~/DesginSystem", "Resend Email Confirmation");
               
        }

        
    }
    
    
}
