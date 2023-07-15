using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class DataInput:EntityBase
    {
        public DataInputType DataInputType { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public Guid NodeTypeId { get; set; }
        public NodeType NodeType { get; set; }
        public ICollection<DataInputValue>? DataInputValues { get; set; }
    }

    public enum DataInputType
    {
        Number=0,
        Text=1,
        Boolean=2
    }
}
