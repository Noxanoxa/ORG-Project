using Microsoft.Data.SqlClient;
using Org.Domains.Persons;
using System.Data;
using IAGE.Shared;
using static IAGE.Shared.IAGExtensions;

namespace Org.Storages
{
    public class RoleStorage
    {
        private readonly string connectionString;

        public RoleStorage(string connectionString) =>
            this.connectionString = connectionString;

        private const string insertRoleCommand = "INSERT orgTypes.ROLES VALUES(@aRoleId, @aRoleCode, @aRoleName)";

        public async ValueTask<bool> InsertRole(Guid roleId, string roleCode, string roleName)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertRoleCommand, connection);

            cmd.Parameters.AddWithValue("@aRoleId", roleId);
            cmd.Parameters.AddWithValue("@aRoleCode", roleCode);
            cmd.Parameters.AddWithValue("@aRoleName", roleName);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

       

        private const string selectRolesQuery = "SELECT * FROM ROLES";

        public async ValueTask<List<Role>> SelectRoles()
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(selectRolesQuery, connection);
            SqlDataAdapter da = new(cmd);
            connection.Open();
            DataTable dt = new();
            da.Fill(dt);

            List<Role> roles = new List<Role>();
            foreach (DataRow row in dt.Rows)
            {
                roles.Add(Role.Create(
                    row["RoleId"].AsGuid(),
                    row["RoleCode"].AsString(),
                    row["RoleName"].AsString()));
            }

            return roles;
        }

        private const string selectRoleByIdQuery = "SELECT * FROM ROLES WHERE RoleId = @aRoleId";

        public async ValueTask<Role?> SelectRoleById(Guid roleId)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(selectRoleByIdQuery, connection);
            cmd.Parameters.AddWithValue("@aRoleId", roleId);
            SqlDataAdapter da = new(cmd);
            connection.Open();
            DataTable dt = new();
            da.Fill(dt);

            return dt.Rows.Count == 0
                ? null
                : createRoleFromRow(dt.Rows[0]);
        }

        private static Role createRoleFromRow(DataRow row) =>
            Role.Create(
                row["RoleId"].AsGuid(),
                row["RoleCode"].AsString(),
                row["RoleName"].AsString());
    }
}