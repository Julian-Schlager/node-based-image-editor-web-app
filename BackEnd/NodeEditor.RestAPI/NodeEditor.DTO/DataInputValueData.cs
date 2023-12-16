using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DTO
{
    public class DataInputValueData
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? Value { get; set; }
        public Guid? DataInputId { get; set; }
        public DataInput? DataInput { get; set; }
        public Guid? NodeId { get; set; }
        public Node? Node { get; set; }
    }
}
