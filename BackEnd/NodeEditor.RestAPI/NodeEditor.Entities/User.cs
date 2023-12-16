using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class User:EntityBase
    {
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        public ICollection<NodeGroup> NodeGroups { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
