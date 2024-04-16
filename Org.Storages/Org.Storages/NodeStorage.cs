using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Domains.Nodes;
using Org.Domains.Persons;
using System.Data;
using IAGE.Shared;

namespace Org.Storages
{
    public class NodeStorage
    {
        private readonly string connectionString;

        public NodeStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string insertNodeCommand = "INSERT dbo.NODES VALUES(@aId, @aTypeId, @aCode, @aName)";
        private const string insertNodePersonCommand = "INSERT dbo.POSTES VALUES(@aNodeId, @aPersonId, @aRoleId)";
        private const string insertSubNodeCommand = "INSERT dbo.SUBNODES VALUES(@aNodeId, @aSubNodeId)";
        private const string selectNodesQuery = "SELECT * FROM NODES";


        public async ValueTask<bool> InsertNode(Node node)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertNodeCommand, connection);

            cmd.Parameters.AddWithValue("@aId", node.NodeId);
            cmd.Parameters.AddWithValue("@aTypeId", node.TypeId);
            cmd.Parameters.AddWithValue("@aName", node.Name);
            cmd.Parameters.AddWithValue("@aCode", node.Code);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        public async ValueTask<bool> AddPersonToNode(Guid nodeId, NodePerson person)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertNodePersonCommand, connection);

            cmd.Parameters.AddWithValue("@aNodeId", nodeId);
            cmd.Parameters.AddWithValue("@aPersonId", person.PersonId);
            cmd.Parameters.AddWithValue("@aRoleId", person.RoleId);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        public async ValueTask<bool> AddSubNodeToNode(Guid nodeId, Node subnode)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(insertSubNodeCommand, connection);

            cmd.Parameters.AddWithValue("@aNodeIdParent", nodeId);
            cmd.Parameters.AddWithValue("@aPersonIdChild", subnode.NodeId);

            await connection.OpenAsync();
            int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

            return insertedRows != 0;
        }

        public async ValueTask<List<Node>> SelectSubNodes()
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new(selectNodesQuery, connection);
            SqlDataAdapter da = new(cmd);
            connection.Open();
            DataTable dt = new();
            da.Fill(dt);

            List<Node> nodes = new List<Node>();
            foreach (DataRow row in dt.Rows)
            {
                nodes.Add(Node.Create(
                row["NodeId"].AsGuid(),
                row["NodeType"].AsGuid(),
                row["Code"].AsString(),
                row["Name"].AsString()));
            }

            return  nodes;
        }

    }
}