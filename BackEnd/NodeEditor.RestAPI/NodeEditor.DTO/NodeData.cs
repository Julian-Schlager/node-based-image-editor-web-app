using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DTO
{
    public class NodeData
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public NodeType? NodeType { get; set; }
        public Guid? NodeTypeId { get; set; }
        public ICollection<DataInputValueData>? DataInputValues { get; set; }

        public NodeGroupData? NodeGroup { get; set; }
        public Guid? NodeGroupId { get; set; }

        public Guid? PreviousNodeId { get; set; }
        public NodeData? PreviousNode { get; set; }
        public NodeData[]? NextNodes { get; set; }
    }
}
