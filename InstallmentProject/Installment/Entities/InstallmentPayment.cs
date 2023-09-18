using EasyRepository.EFCore.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Installment.Entities
{
    public class InstallmentPayment : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        [Key]
        public int PaymentID { get; set; }
        public int InstallmentPlanId { get; set; }
        public InstallmentPlan InstallmentPlan { get; set; }

        [Required(ErrorMessage = "PaymentDate is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        
        public decimal Amount { get; set; }
        public string? Status { get; set; }
    }
}
