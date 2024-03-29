﻿using IAGE.Shared;
using Microsoft.Data.SqlClient;
using System.Data;
using Org.Domains.NodeTypes;

namespace Org.Storages;

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
        cmd.Parameters.AddWithValue("@aRoleId", nodeRole.RoleId);
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
        cmd.Parameters.AddWithValue("@aSubNodeId", nodeChild.NodeTypeId);
        cmd.Parameters.AddWithValue("@aMinValue", nodeChild.MinValue);
        cmd.Parameters.AddWithValue("@aMaxValue", nodeChild.MaxValue);

        await connection.OpenAsync();
        int insertedRows = (int)await cmd.ExecuteNonQueryAsync();

        return insertedRows != 0;
    }

    private const string selectNodeTypeByIdQuery = "orgTypes.GetNodeType";

    public async ValueTask<NodeType> SelectNodeTypeById(Guid id)
    {
        await using var connection = new SqlConnection(connectionString);
        SqlCommand cmd = new(selectNodeTypeByIdQuery, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@aNodeId", id);

        SqlDataAdapter da = new(cmd);
        connection.Open();
        DataSet ds = new();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
            return null;

        return getFromDataSet(ds);
    }

    private NodeType getFromDataSet(DataSet dataset)
    {
        NodeType result = new NodeType();
        DataRow row = dataset.Tables[0].Rows[0];
        result = NodeType.Create(row["Id"].AsGuid(), row["Code"].AsString(), row["Name"].AsString());

        foreach (DataRow childRow in dataset.Tables[1].Rows)
        {
            result.SubNodes.Add(new NodeChild()
            {
                NodeTypeId = childRow["SubNodeId"].AsGuid(),
                MinValue = (int)childRow["MinValue"],
                MaxValue = (int)childRow["MaxValue"]
            });
        }

        foreach (DataRow roleRow in dataset.Tables[2].Rows)
        {
            result.Roles.Add(
                NodeRole.Create(roleRow["RoleId"].AsGuid(), (int)roleRow["MinValue"],
                (int)roleRow["MaxValue"]));
        }

        return result;
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

        return getFromRow(dt.Rows[0]);
    }

    private const string selectNodeTypesQuery = "SELECT * FROM OrgTypes.NODES ";

    public async Task<List<NodeType>> SelectNodeTypes()
    {
        await using var connection = new SqlConnection(connectionString);
        SqlCommand cmd = new(selectNodeTypesQuery, connection);

        SqlDataAdapter da = new(cmd);
        connection.Open();
        DataTable dt = new();
        da.Fill(dt);

        List<NodeType> list = new List<NodeType>();
        foreach (DataRow row in dt.Rows)
            list.Add(getFromRow(row));

        return list;
    }

    private NodeType getFromRow(DataRow row) =>
        NodeType.Create(row["Id"].AsGuid(), row["Code"].AsString(), row["Name"].AsString());
}