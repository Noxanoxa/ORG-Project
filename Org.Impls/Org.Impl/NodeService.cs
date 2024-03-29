﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Org.Apps;
using Org.Domains.Nodes;
using Org.Storages;

namespace Org.Impl
{
    public class NodeService : INodeService
    {
        private readonly NodeStorage nodeStorage;

        public NodeService(
            IConfiguration configuration
        )
        {
            string connectionString = configuration.GetConnectionString("ORGDB");
            nodeStorage = new NodeStorage(connectionString);
        }

        public async Task CreateNode(Node node)
        {
            await nodeStorage.InsertNode(node);
        }
    }
}