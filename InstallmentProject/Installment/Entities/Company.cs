﻿using EasyRepository.EFCore.Abstractions;

namespace Installment.Entities
{
    public class Company : EasyBaseEntity<int>, IEasyCreateDateEntity, IEasyUpdateDateEntity, IEasySoftDeleteEntity
    {
        public string CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public byte[]? LogoImage { get; set; }
    }
}
