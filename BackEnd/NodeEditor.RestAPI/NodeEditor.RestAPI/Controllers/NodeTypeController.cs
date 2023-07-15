using Microsoft.AspNetCore.Mvc;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.Entities;

namespace NodeEditor.RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeTypeController : ControllerBase
    {
      
        private readonly ILogger<NodeController> _logger;
        private readonly INodeTypeService nodeTypeService;

        public NodeTypeController(ILogger<NodeController> logger, INodeTypeService nodeTypeService)
        {
            _logger = logger;
            this.nodeTypeService = nodeTypeService;
        }

        [HttpGet(Name = "GetAllNodeTypes")]
        public async Task<IEnumerable<NodeType>> Get()
        {
            return await this.nodeTypeService.GetAll();
        }
    }
}