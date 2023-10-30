using EasyRepository.EFCore.Abstractions;

namespace Installment.Entities
{
    public class Product : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public string? Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string CompanyName { get; set; }
        public int Qty { get; set; }
        public string? Description { get; set; }
        public int CompanyId { get; set; }
        

    }
}
