using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class User:EntityBase
    {
        public ICollection<NodeGroup> NodeGroups { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
