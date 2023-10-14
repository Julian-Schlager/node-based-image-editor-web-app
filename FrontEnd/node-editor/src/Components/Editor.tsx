import { useEffect, useRef, useState } from "react";
import { getNodeTypes } from "../Hooks/NodeTypeService";
import { NodeType } from "../Models/NodeType";
import { Button, ListGroup, ListGroupItem } from "react-bootstrap";
import { NodeEditor } from "flume";
import {flumeConfig, addNodeTypes} from "../flume/flumeConfig";
import useStateWithCallback from 'use-state-with-callback';
import { Console } from "console";


function Editor(){
    
    const initNodeTypes: NodeType[] = []
    const [nodeTypeState,setNodeTypeState] = useState(
        {
            nodeTypes: initNodeTypes
        }
    )
    const [configState, setConfigState] = useState(flumeConfig);

    const [nodeState, setNodeState] = useState({})

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
    }

    return(
        <div style={{width: 1920,height: 1080}}>
            {/* <ListGroup>
                {state.nodeTypes.map( x=> (<ListGroupItem key={x.id}>{x.description}</ListGroupItem>))}
            </ListGroup> */}

            <NodeEditor 
                nodeTypes={configState.nodeTypes}
                portTypes={configState.portTypes}
                nodes={nodeState}
                onChange={setNodeState}
            />

            <Button variant="primary" onClick={updateDiagram}>Update</Button>{' '}
        </div>
    );
}

export default Editor;

