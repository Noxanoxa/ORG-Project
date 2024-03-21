using Microsoft.Data.SqlClient;
using Org.Domains.Persons;
using System.Data;
using IAGE.Shared;

namespace Org.Storages;

public class RoleStorage
{
    private readonly string connectionString;

    public RoleStorage(string connectionString) =>
        this.connectionString = connectionString;

    private const string insertRoleCommand = "INSERT dbo.ROLES (RoleId,RoleCode,RoleName) VALUES(@aRoleId, @aRoleCode, @aRoleName)";

    public async ValueTask<bool> InsertRole(Role role)
    {
        try
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertRoleCommand, connection);

            cmd.Parameters.AddWithValue("@aRoleId", role.RoleId);
            cmd.Parameters.AddWithValue("@aRoleCode", role.RoleCode);
            cmd.Parameters.AddWithValue("@aRoleName", role.RoleName);

            connection.Open();

            int insertedRows = (int)cmd.ExecuteNonQuery();
            return insertedRows != 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.AsString());
        }
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

    private const string selectRoleCountByCodeQuery =
        "SELECT COUNT(*) FROM ROLES WHERE upper(RoleCode) = upper(@aRoleCode)";

    public async Task<bool> RoleCodeExists(string roleCode)

    {
        await using var connection = new SqlConnection(connectionString);
        SqlCommand cmd = new(selectRoleCountByCodeQuery, connection);
        cmd.Parameters.AddWithValue("@aRoleCode", roleCode);
        SqlDataAdapter da = new(cmd);
        connection.Open();

        int result = (int)(await cmd.ExecuteScalarAsync());
        return result > 0;
    }
}