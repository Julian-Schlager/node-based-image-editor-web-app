import { FlumeNode, NodeMap, NodeType } from "flume";
import { NodeGroup as EditorNodeGroup } from "../Models/NodeGroup";
import { Node as EditorNode } from "../Models/Node";
import { NodeType as EditorNodeType, ModificationType } from "../Models/NodeType";
import Editor from "../Components/Editor";

export function mapFlumeNodes(flumeNodes: NodeMap, nodeTypes: EditorNodeType[]): EditorNode|undefined {

    const nodes: FlumeNode[] = Object.entries(flumeNodes).map(([key, value]) => value)
    console.log(nodes)
    const uploadNodeTypeId = nodeTypes.find(x => x.modificationType === ModificationType.Upload)?.id
    const uploadNode = nodes.find(x => x.type === uploadNodeTypeId)

    console.log(uploadNode);
    console.log(nodeTypes);

    // const nodeGroup = {
    //     nodes: [] as EditorNode[],
    //     id: crypto.randomUUID()
    // }

    const rootNode = getNodeRecursive(nodes, nodeTypes, uploadNode)

    console.log(rootNode);
    // if (rootNode) {

    //     nodeGroup.nodes = createArrayOfNodes(rootNode, [])
    // }

    return rootNode;

}

function getNodeRecursive(nodes: FlumeNode[], nodeTypes: EditorNodeType[], currentNode?: FlumeNode, previousNodeId?: string): EditorNode | undefined {
    const currentNodeId = crypto.randomUUID()
    const nodeType = nodeTypes.find(x => x.id === currentNode?.type)
    if (currentNode && nodeType) {
        const outputs = currentNode.connections.outputs["image"];
        let editorNextNodes : EditorNode[] = [];
        if(outputs){
            editorNextNodes = outputs
            .map(x => getNodeRecursive(nodes, nodeTypes,  nodes.find(y => y.id === x.nodeId), currentNodeId))
            .filter(x => x != undefined) as EditorNode[]
        }
        return {
            id: currentNodeId,
            nextNodes: editorNextNodes,
            nodeType: nodeType,
            nodeTypeId: nodeType.id,
            previousNodeId,
            dataInputValues: nodeType.dataInputs.map(dataInput => {
                const flumeValue = currentNode.inputData[dataInput.name][dataInput.name];

                return {
                    dataInputId: dataInput.id,
                    value: `${flumeValue}`,
                    id: crypto.randomUUID(),
                    nodeId: currentNodeId
                }
            })
        }
    }
    return undefined;
}

export function createArrayOfNodes(currentNode: EditorNode, resultArray: EditorNode[]): EditorNode[] {
    resultArray.push(currentNode);
    currentNode.nextNodes.forEach(node => {
        createArrayOfNodes(node, resultArray)
    });
    return resultArray;
}