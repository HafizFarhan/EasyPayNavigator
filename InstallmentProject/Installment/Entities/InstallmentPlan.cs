using EasyRepository.EFCore.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Installment.Entities
{
    public class InstallmentPlan : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        [Key]
        public int InstallmentPlanId { get; set; }
       
        public decimal TotalPrice { get; set; }
        public decimal AdvancePayment { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
    }
}
