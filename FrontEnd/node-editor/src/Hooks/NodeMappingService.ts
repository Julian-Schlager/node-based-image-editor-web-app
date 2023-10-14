import { FlumeNode, NodeMap, NodeType } from "flume";
import { NodeGroup as EditorNodeGroup } from "../Models/NodeGroup";
import {Node as EditorNode} from "../Models/Node";
import { NodeType as EditorNodeType } from "../Models/NodeType" ;
import Editor from "../Components/Editor";

export function mapFlumeNodes(flumeNodes:NodeMap,nodeTypes:EditorNodeType[]):EditorNodeGroup{
    
    const nodes:FlumeNode[] = Object.entries(flumeNodes).map(([key, value]) => value)
    
    const uploadNode = nodes.find(x => x.type==="upload")

    console.log(uploadNode);
    console.log(nodeTypes);

    const nodeGroup = {
        nodes: [] as EditorNode[],
        userId: "toDo",
        id: crypto.randomUUID()
    }

    const rootNode = getNodeRecursive(nodes,nodeTypes,nodeGroup,uploadNode)

    console.log(rootNode);
    if(rootNode){

        nodeGroup.nodes = createArrayOfNodes(rootNode,[])
    }

    return nodeGroup;

}

function getNodeRecursive(nodes:FlumeNode[],nodeTypes:EditorNodeType[], nodeGroup:EditorNodeGroup,currentNode?:FlumeNode,previousNodeId?:string):EditorNode|undefined{
    const currentNodeId = crypto.randomUUID()
    const nodeType = nodeTypes.find(x => x.description === currentNode?.type)
    if(currentNode && nodeType){
        return{
            id: currentNodeId,
            nextNodes: currentNode.connections.outputs["image"].map(x=>getNodeRecursive(nodes,nodeTypes,nodeGroup,nodes.find(y => y.id === x.nodeId),currentNodeId)).filter(x => x != undefined) as EditorNode[],
            nodeType: nodeType,
            nodeTypeId: nodeType.id,
            nodeGroup,
            nodeGroupId:nodeGroup.id,
            previousNodeId,
            dataInputValues: nodeType.dataInputs.map(dataInput => 
                {
                    const flumeValue = currentNode.inputData[dataInput.name][dataInput.name];

                    return{
                        dataInputId:dataInput.id,
                        value: flumeValue,
                        id: crypto.randomUUID(),
                        nodeId: currentNodeId
                    }
                })

        }
    }
    return undefined;
}

function createArrayOfNodes(currentNode:EditorNode,resultArray:EditorNode[]):EditorNode[]{
    resultArray.push(currentNode);
    currentNode.nextNodes.forEach(node => {
        createArrayOfNodes(node,resultArray)
    });
    return resultArray;
}