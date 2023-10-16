using EasyRepository.EFCore.Abstractions;

namespace Installment.Entities
{
    public enum UserRole
    {
        Admin,
        User,
        CompanyAdmin
    }
    public class User : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? CompanyId { get; set; }
        public UserRole Role { get; set; }
        // Navigation property to Company
        public Company? Company { get; set; }
    }
}
