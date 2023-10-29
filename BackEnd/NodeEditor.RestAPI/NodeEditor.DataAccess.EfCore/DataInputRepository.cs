using Microsoft.EntityFrameworkCore;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess.EfCore
{
    public class DataInputRepository : IDataInputRepository
    {
        private readonly NodeEditorContext context;

        public DataInputRepository(NodeEditorContext context) {
            this.context = context;
        
        }

        public async Task<IEnumerable<DataInput>> GetAll()
        {
            return await this.context.DataInputs.AsNoTracking().ToListAsync();
        }
    }
}
