namespace Org.Domains.Persons
{
    public record Role
    {
        public Guid RoleId { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        private Role() => RoleId = Guid.NewGuid();
        private Role(Guid roleId, string roleCode, string roleName) : this()
        {
            RoleId = roleId;
            RoleCode = roleCode;
            RoleName = roleName;
        }

        public static Role Create(Guid roleId, string roleCode, string roleName) =>
            new(roleId, roleCode, roleName);
        public static Role Create(string roleCode, string roleName) =>
            new(Guid.NewGuid(), roleCode, roleName);
    }
}