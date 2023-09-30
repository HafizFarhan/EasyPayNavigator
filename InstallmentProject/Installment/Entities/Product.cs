using EasyRepository.EFCore.Abstractions;

namespace Installment.Entities
{
    public class Product : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CompanyId { get; set; }
        

    }
}
