﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Entities
{
    public class Node:EntityBase
    {
        public NodeType NodeType { get; set; }
        public Guid NodeTypeId { get; set; }
        public ICollection<DataInputValue> DataInputValues { get; set; }

        public NodeGroup NodeGroup { get; set; }
        public Guid NodeGroupId { get; set; }

    }
}