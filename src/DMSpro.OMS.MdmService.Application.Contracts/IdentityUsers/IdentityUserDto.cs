using JetBrains.Annotations;
using System;

namespace DMSpro.OMS.MdmService.IdentityUsers
{
    public class IdentityUserDto
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string UserName { get; set; }
        [CanBeNull]
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [CanBeNull]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
    }
}
