using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.Report
{
    public class ProductSalesModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public  List<dynamic> ProductSalesSummaries { get; set; }
        public decimal totalSalePrice { get; set; }
        public decimal totalPurchasePrice { get; set; }
        public decimal netProfit { get; set; }
        public ProductSalesModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            // Create a LINQ query to join the InstallmentPlan and Product tables and calculate values
            var productSalesQuery = from plan in _unitOfWork.Repository.GetQueryable<InstallmentPlan>()
                                    join product in _unitOfWork.Repository.GetQueryable<Product>() on plan.ProductId equals product.Id
                                    select new
                                    {
                                        ProductName = product.Name,
                                        UnitPrice = product.SalePrice, // Assuming SalePrice is the unit price
                                        PurchasePrice = product.OriginalPrice, // Assuming OriginalPrice is the purchase price
                                        Quantity = plan.ProductQty,
                                        TotalPurchasePrice = product.OriginalPrice * plan.ProductQty,
                                        TotalSale = product.SalePrice * plan.ProductQty
                                    };

            // Calculate total sale price and net profit
             totalSalePrice = productSalesQuery.Sum(ps => ps.TotalSale);
             totalPurchasePrice = productSalesQuery.Sum(ps => ps.TotalPurchasePrice);
             netProfit = totalSalePrice - totalPurchasePrice;

            ProductSalesSummaries = productSalesQuery.ToList<dynamic>();
        }
    }
}
