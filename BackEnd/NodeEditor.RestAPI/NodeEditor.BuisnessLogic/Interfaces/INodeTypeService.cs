using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Interfaces
{
    public interface INodeTypeService
    {
        Task<IEnumerable<NodeType>> GetAll();
    }
}
