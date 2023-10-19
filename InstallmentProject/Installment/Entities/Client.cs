using EasyRepository.EFCore.Abstractions;

namespace Installment.Entities
{
    public class Client : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
    }
}
