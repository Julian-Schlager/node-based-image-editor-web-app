using Microsoft.AspNetCore.Mvc;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DTO;
using NodeEditor.Entities;
using NodeEditor.RestAPI.UserManagment;

namespace NodeEditor.RestAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NodeGroupController : ControllerBase
    {
      
        private readonly ILogger<NodeGroupController> _logger;
        private readonly INodeGroupService nodeGroupService;
        private User CurrentUser => (User) ControllerContext.HttpContext.Items["User"];

        public NodeGroupController(ILogger<NodeGroupController> logger, INodeGroupService nodeGroupService)
        {
            _logger = logger;
            this.nodeGroupService = nodeGroupService;
        }

        [HttpPost("Save")]
        public async Task<NodeGroup> Save([FromBody] NodeGroupData nodeGroup)
        {
            return await this.nodeGroupService.Save(nodeGroup, CurrentUser.Id);
        }

        [HttpGet("Load")]
        public async Task<IEnumerable<NodeGroup>> Load()
        {
            return await this.nodeGroupService.Load(CurrentUser.Id);
        }

        [HttpDelete("Delete/{nodeGroupId}")]
        public async Task<bool> Delete(Guid nodeGroupId)
        {
            return await this.nodeGroupService.Delete(nodeGroupId, CurrentUser.Id);
        }
    }
}