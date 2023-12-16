using Microsoft.EntityFrameworkCore;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess.EfCore
{
    public class NodeGroupRepository : INodeGroupRepository
    {
        private readonly NodeEditorContext context;

        public NodeGroupRepository(NodeEditorContext context) {
            this.context = context;
        
        }

        public async Task<bool> Delete(Guid nodeGroupId)
        {
            int count = await this.context.NodesGroups.Where(x => x.Id == nodeGroupId).ExecuteDeleteAsync();
            return count > 0;
        }

        public async Task<bool> Exists(Guid nodeGroupId, Guid userId)
        {
            return await this.context.NodesGroups.AnyAsync(x => x.Id == nodeGroupId && x.UserId == userId);
        }

        public async Task<IEnumerable<NodeGroup>> Load(Guid userId)
        {
            return await this.context.NodesGroups.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<NodeGroup> Save(NodeGroup nodeGroup)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<NodeGroup> res = await this.context.NodesGroups.AddAsync(nodeGroup);
            this.context.SaveChanges();
            return res.Entity;
        }
    }
}
