using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Domains.Persons;

namespace Org.Domains.Nodes
{
    public class Node
    {
        public Guid NodeId { get; set; } = Guid.NewGuid();
        public Guid TypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<NodePerson> Persons { get; set; }
        public List<Node> SubNodes { get; set; }

        public static Node Create(Guid typeId)
        {
            return new Node()
            {
                NodeId = Guid.NewGuid(),
                TypeId = typeId,
                Code = string.Empty,
                Name = string.Empty,
                Persons = new List<NodePerson>(),
                SubNodes = new List<Node>()
            };
        }
    }
}