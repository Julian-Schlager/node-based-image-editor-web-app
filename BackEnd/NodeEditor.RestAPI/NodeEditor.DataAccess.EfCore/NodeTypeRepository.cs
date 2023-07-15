using Microsoft.EntityFrameworkCore;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess.EfCore
{
    public class NodeTypeRepository : INodeTypeRepository
    {
        private readonly NodeEditorContext context;

        public NodeTypeRepository(NodeEditorContext context) {
            this.context = context;
        
        }

        public async Task<IEnumerable<NodeType>> GetAll()
        {
            return await this.context.NodeTypes.AsNoTracking().ToListAsync();
        }
    }
}
