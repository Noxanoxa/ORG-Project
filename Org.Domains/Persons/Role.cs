namespace Org.Domains.Persons
{
    public record Role
    {
        private Role(Guid roleId, string roleCode, string roleName)
        {
            RoleId = roleId;
            RoleCode = roleCode;
            RoleName = roleName;
        }

        public static Role Create(Guid roleId, string roleCode, string roleName) =>
            new(roleId, roleCode, roleName);
        public static Role Create(string roleCode, string roleName) =>
            new(Guid.NewGuid(), roleCode, roleName);
        public Guid RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public Role()
        {
            RoleId = Guid.NewGuid();
        }
    }
}