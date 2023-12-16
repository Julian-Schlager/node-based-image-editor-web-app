using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.DTO;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Implementation
{
    public class NodeGroupService: INodeGroupService
    {
        private readonly INodeGroupRepository nodeGroupRepository;

        public NodeGroupService(INodeGroupRepository nodeGroupRepository) 
        {
            this.nodeGroupRepository = nodeGroupRepository;
        }

        public async Task<bool> Delete(Guid nodeGroupId,Guid userId)
        {
            if(await nodeGroupRepository.Exists(nodeGroupId,userId)) return await nodeGroupRepository.Delete(nodeGroupId);
            throw new ArgumentException("Nodegroup does not exist");
        }

        public Task<IEnumerable<NodeGroup>> Load(Guid userId)
        {
            return nodeGroupRepository.Load(userId);
        }

        public Task<NodeGroup> Save(NodeGroupData nodeGroupData,Guid userId) //ToDo Remove Casts
        {
            if (nodeGroupData.Nodes.Any(x=>x.NodeType.Name == "Upload")&& nodeGroupData.Nodes.Any(x => x.NodeType.Name == "Download"))
            {
                NodeGroup nodeGroup = MapNodeGroup(nodeGroupData, userId);


                return nodeGroupRepository.Save(nodeGroup);
            }
            throw new ArgumentException("Invalid NodeGroup");
        }

        private static NodeGroup MapNodeGroup(NodeGroupData nodeGroupData, Guid userId)
        {
            Guid nodeGroupId = nodeGroupData.Id ?? Guid.NewGuid();

            return new NodeGroup()
            {
                Id = nodeGroupId,
                Name = nodeGroupData.Name,
                //Nodes = nodeGroupData.Nodes?.Select(x => MapNodes(x, nodeGroupId)).ToList(),
                UserId = userId,
                LastModifiedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                FlumeNodeMap = nodeGroupData.FlumeNodeMap
            };
        }

        private static Node MapNodes(NodeData x, Guid nodeGroupId)
        {
            if (x != null && x.NodeTypeId != null)
            {
                Guid nodeId = x.Id ?? Guid.NewGuid();
                return new Node()
                {
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedAt = DateTime.UtcNow,
                    DataInputValues = x.DataInputValues?.Select(y => MapDataInputValues(y, nodeId)).ToList(),
                    Id = nodeId,
                    //NextNodes = x.NextNodes?.Select(y => MapNodes(y, nodeGroupId))?.ToArray(),
                    NodeGroupId = nodeGroupId,
                    //NodeType = x.NodeType,
                    NodeTypeId = (Guid)x.NodeTypeId,
                    //PreviousNode = MapNodes(x.PreviousNode, nodeGroupId),
                    PreviousNodeId = x.PreviousNodeId,
                };
            }
            return null;
        }

        private static DataInputValue MapDataInputValues(DataInputValueData y, Guid nodeId)
        {
            if(y != null && y.DataInputId != null)
            {
                return new DataInputValue()
                {
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedAt = DateTime.UtcNow,
                    Id = y.Id ?? Guid.NewGuid(),
                    DataInputId = (Guid)y.DataInputId,
                    NodeId = nodeId,
                    Node = y.Node,
                    Value = y.Value,
                    //DataInput = y.DataInput
                };
            }
            return null;
        }
    }
}
