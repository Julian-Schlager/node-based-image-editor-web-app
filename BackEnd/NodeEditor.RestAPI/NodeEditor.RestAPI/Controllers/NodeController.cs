using Microsoft.AspNetCore.Mvc;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.Entities;

namespace NodeEditor.RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
      
        private readonly ILogger<NodeController> _logger;
        private readonly INodeService nodeService;

        public NodeController(ILogger<NodeController> logger, INodeService nodeService)
        {
            _logger = logger;
            this.nodeService = nodeService;
        }

        [HttpGet(Name = "GetAllNodes")]
        public async Task<IEnumerable<Node>> Get()
        {
            return await this.nodeService.GetAll();
        }
    }
}