using NodeEditor.DTO;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Interfaces
{
    public interface INodeGroupService
    {
        Task<NodeGroup> Save(NodeGroupData nodeGroup, Guid id);
        Task<IEnumerable<NodeGroup>> Load(Guid userId);
        Task<bool> Delete(Guid nodeGroupId, Guid userId);
    }
}
