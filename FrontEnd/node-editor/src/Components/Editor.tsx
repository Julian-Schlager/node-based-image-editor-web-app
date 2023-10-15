import { useEffect, useRef, useState } from "react";
import { getNodeTypes } from "../Hooks/NodeTypeService";
import { ModificationType, NodeType } from "../Models/NodeType";
import { Button, ListGroup, ListGroupItem } from "react-bootstrap";
import { DefaultNode, FlumeConfig, NodeEditor, NodeMap } from "flume";
import { addNodeTypes} from "../flume/flumeConfig";
import useStateWithCallback from 'use-state-with-callback';
import { Console } from "console";
import { mapFlumeNodes } from "../Hooks/NodeMappingService";
import { type } from "os";


function Editor(){
    
    const initNodeTypes: NodeType[] = []
    const [nodeTypeState,setNodeTypeState] = useState(
        {
            nodeTypes: initNodeTypes
        }
    )
    const [configState, setConfigState] = useState<FlumeConfig>();

    const [nodeState, setNodeState] = useState<NodeMap>({})

    // Define NodeTypes Here
    // const [count, setCount] = useStateWithCallback(0, currentCount => {
    //     console.log('render, then callback.');
    //     console.log('otherwise use useStateWithCallbackInstant()');
    
    //     if (currentCount > 1) {
    //       console.log('Threshold of over 1 reached.');
    //     } else {
    //       console.log('No threshold reached.');
    //     }

    const fetchNodeTypes = async () => {
        const nodeTypes = await getNodeTypes()
        // const results = nodeTypes
        // const users = results.map(parseUserData)
        console.log(nodeTypes)
        const newFlumeConfig = addNodeTypes(nodeTypes);
        setConfigState(newFlumeConfig);
        return setNodeTypeState({ ...nodeTypeState, nodeTypes: [...nodeTypes] })
    }
    
    useEffect(() => {
        fetchNodeTypes()},
        []
    )

    function updateDiagram(){
        console.log(nodeState)
        console.log(mapFlumeNodes(nodeState,nodeTypeState.nodeTypes))
    }
    
    function getNodeEditor(){
        const editorDefaultNodes:DefaultNode[] = [];
        const uploadNode = nodeTypeState.nodeTypes.find(x=> x.modificationType === ModificationType.Upload);
        const downloadNode= nodeTypeState.nodeTypes.find(x=> x.modificationType === ModificationType.Download)
        if(uploadNode){
            editorDefaultNodes.push({
                type: uploadNode.id,
                x: -500,
                y: -200
            })
        }

        if(downloadNode){
            editorDefaultNodes.push({
                type: downloadNode.id,
                x: 500,
                y: 200
            })
        }
        
        if(configState){
            return(<NodeEditor 
                nodeTypes={configState.nodeTypes}
                portTypes={configState.portTypes}
                
                defaultNodes={editorDefaultNodes}
                nodes={nodeState}
                onChange={setNodeState}
            />)
        }
        return(<h2>Loading, Please Wait...</h2>)
        
    }

    return(
        <div style={{width: 1920,height: 1080}}>
            {/* <ListGroup>
                {state.nodeTypes.map( x=> (<ListGroupItem key={x.id}>{x.description}</ListGroupItem>))}
            </ListGroup> */}
            {getNodeEditor()}

            <Button variant="primary" onClick={updateDiagram}>Update</Button>{' '}
        </div>
    );
}

export default Editor;

