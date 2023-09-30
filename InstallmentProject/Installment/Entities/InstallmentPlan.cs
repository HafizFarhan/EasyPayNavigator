using EasyRepository.EFCore.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Installment.Entities
{
    public class InstallmentPlan : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AdvancePayment { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public int CompanyId { get; set; }
        [NotMapped]
        public decimal TotalPaid { get; set; }
        [NotMapped]
        public int NoInstallments { get; set; }
        [NotMapped]
        public decimal RemainingAmount { get; set; }
    }
}
