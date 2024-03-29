using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Domains.Nodes
{
    public class NodePerson
    {
        public Guid PersonId { get; set; } = Guid.NewGuid();
        public Guid RoleId { get; set; }
        public string Nom { get; set; }
    }
}