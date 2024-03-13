using IAGE.Shared;
using Microsoft.Data.SqlClient;
using Org.Domains.Nodes;
using Org.Domains.Persons;
using System.Data;

namespace Org.Storages
{
    public class OrgTypeStorage
    {
        private readonly string connectionString;

        public OrgTypeStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string insertNodeTypeCommand = "INSERT orgTypes.NODES VALUES(@aId, @aCode, @aName)";

        public async ValueTask<bool> InsertNodeType(Guid id, string code, string name)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertNodeTypeCommand, connection);

            cmd.Parameters.AddWithValue("@aId", id);
            cmd.Parameters.AddWithValue("@aCode", code);
            cmd.Parameters.AddWithValue("@aName", name);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        private const string addRoleToNodeTypeCommand =
            "INSERT orgTypes.ROLES VALUES(@aNodeId, @aRoleId, @aMinValue, @aMaxValue)";

        public async ValueTask<bool> AddRoleToNodeType(Guid nodeId, NodeRole nodeRole)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(addRoleToNodeTypeCommand, connection);

            cmd.Parameters.AddWithValue("@aNodeId", nodeId);
            cmd.Parameters.AddWithValue("@aRoleId", nodeRole.Role.RoleId);
            cmd.Parameters.AddWithValue("@aMinValue", nodeRole.MinValue);
            cmd.Parameters.AddWithValue("@aMaxValue", nodeRole.MaxValue);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        private const string addSubNodeToNodeTypeCommand =
            "INSERT orgTypes.SUBNODES VALUES(@aNodeId, @aSubNodeId, @aMinValue, @aMaxValue)";

        public async ValueTask<bool> AddSubNodeToNodeType(Guid nodeId, NodeChild nodeChild)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(addSubNodeToNodeTypeCommand, connection);

            cmd.Parameters.AddWithValue("@aNodeId", nodeId);
            cmd.Parameters.AddWithValue("@aSubNodeId", nodeChild.NodeType.Id);
            cmd.Parameters.AddWithValue("@aMinValue", nodeChild.MinValue);
            cmd.Parameters.AddWithValue("@aMaxValue", nodeChild.MaxValue);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        private const string selectNodeTypeByIdQuery = "SELECT * FROM OrgTypes.NODES WHERE ID = @aId";

        public async ValueTask<NodeType> SelectNodeTypeById(Guid id)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(selectNodeTypeByIdQuery, connection);

            cmd.Parameters.AddWithValue("@aId", id);

            SqlDataAdapter da = new(cmd);
            connection.Open();
            DataTable dt = new();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return NodeType.Create(
                dt.Rows[0]["Id"].AsGuid(),
                dt.Rows[0]["Code"].AsString(),
                dt.Rows[0]["Name"].AsString());
        }

        private const string selectNodeTypeByCodeQuery = "SELECT * FROM OrgTypes.NODES WHERE Code = @aCode";

        public async ValueTask<NodeType> SelectNodeTypeByCode(string code)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(selectNodeTypeByCodeQuery, connection);

            cmd.Parameters.AddWithValue("@aCode", code);

            SqlDataAdapter da = new(cmd);
            connection.Open();
            DataTable dt = new();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return NodeType.Create(
                dt.Rows[0]["Id"].AsGuid(),
                dt.Rows[0]["Code"].AsString(),
                dt.Rows[0]["Name"].AsString());
        }
    }
}