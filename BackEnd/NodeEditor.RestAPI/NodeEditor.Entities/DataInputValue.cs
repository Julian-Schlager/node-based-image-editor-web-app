using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class DataInputValue:EntityBase
    {
        public string Value { get; set; }
        public Guid DataInputId { get; set; }
        public DataInput DataInput { get; set; }
        public Guid NodeId { get; set; }
        public Node Node { get; set; }
    }
}
