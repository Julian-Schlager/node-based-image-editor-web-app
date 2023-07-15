using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Implementation
{
    public class NodeService: INodeService
    {
        private readonly INodeRepository nodeRepository;

        public NodeService(INodeRepository nodeRepository) 
        {
            this.nodeRepository = nodeRepository;
        }

        public async Task<IEnumerable<Node>> GetAll()
        {
            return await this.nodeRepository.GetAll();
        }
    }
}
