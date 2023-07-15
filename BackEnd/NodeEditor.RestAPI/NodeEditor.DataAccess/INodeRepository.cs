using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess
{
    public interface INodeRepository
    {
        Task<IEnumerable<Node>> GetAll();
    }
}
