using Microsoft.EntityFrameworkCore;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess.EfCore
{
    public class NodeRepository : INodeRepository
    {
        private readonly NodeEditorContext context;

        public NodeRepository(NodeEditorContext context) {
            this.context = context;
        
        }

        public async Task<IEnumerable<Node>> GetAll()
        {
            return await this.context.Nodes.AsNoTracking().ToListAsync();
        }
    }
}
