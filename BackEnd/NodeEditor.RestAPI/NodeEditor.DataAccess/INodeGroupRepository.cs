using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess
{
    public interface INodeGroupRepository
    {
        Task<NodeGroup> Save(NodeGroup nodeGroup);
        Task<IEnumerable<NodeGroup>> Load(Guid userId);
        Task<bool> Delete(Guid nodeGroupId);
        Task<bool> Exists(Guid nodeGroupId, Guid userId);
    }
}
