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
    public class NodeTypeService: INodeTypeService
    {
        private readonly INodeTypeRepository nodeTypeRepository;

        public NodeTypeService(INodeTypeRepository nodeTypeRepository) 
        {
            this.nodeTypeRepository = nodeTypeRepository;
        }

        public async Task<IEnumerable<NodeType>> GetAll()
        {
            return await this.nodeTypeRepository.GetAll();
        }
    }
}
