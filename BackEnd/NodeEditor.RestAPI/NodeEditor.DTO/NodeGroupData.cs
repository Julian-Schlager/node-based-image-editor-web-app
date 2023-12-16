using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DTO
{
    public class NodeGroupData
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public ICollection<NodeData>? Nodes { get; set; }
        public User? User { get; set; }
        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? FlumeNodeMap { get; set; }
    }
}
