using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class NodeGroup:EntityBase
    {
        public ICollection<Node> Nodes { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
