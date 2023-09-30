using EasyRepository.EFCore.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Installment.Entities
{
    public class InstallmentPayment : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public int InstallmentPlanId { get; set; } 
        public decimal RecentAmount { get; set; }
        
        public DateTime Date { get; set; }
        
        public string? Status { get; set; }
    }
}
