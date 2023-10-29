import { createContext, useEffect, useReducer, useRef, useState } from "react";
import { getNodeTypes } from "../Hooks/NodeTypeService";
import { ModificationType, NodeType } from "../Models/NodeType";
import { Node } from "../Models/Node"
import { Button, ListGroup, ListGroupItem } from "react-bootstrap";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Stack from 'react-bootstrap/Stack';
import { DefaultNode, FlumeConfig, NodeEditor, NodeMap } from "flume";
import { addNodeTypes } from "../flume/flumeConfig";
import useStateWithCallback from 'use-state-with-callback';
import { Console } from "console";
import { mapFlumeNodes } from "../Hooks/NodeMappingService";
import { type } from "os";
import { Update } from "./Update";
import { Download } from "./Download";
import { SelectFile } from "./SelectFile";
import { NodeDataState } from "../Models/NodeDataState";
import { EditorState, editorStateReducer } from "../Models/EditorState";

const initialState: EditorState = { nodeDataState:{nodeTypes:[]} };

function Editor() {

    const initNodeTypes: NodeType[] = []
    // const [nodeTypeState, setNodeTypeState] = useState<NodeDataState>(
    //     {
    //         nodeTypes: initNodeTypes
    //     }
    // )
    // const [configState, setConfigState] = useState<FlumeConfig>();

    // const [nodeState, setNodeState] = useState<NodeMap>({});

    // const [firstNodeState, setFirstNodeState] = useState<Node>();

    // const [imageState, setImageState] = useState<Blob>();

    // const [currentFile, setCurrentFile] = useState<File>();

    // function handleImage(image: Blob) {
    //     setImageState(image);
    // }

    // function handleFile(file: File) {
    //     setCurrentFile(file);
    // }

    // function getFile(): File | undefined {
    //     return currentFile;
    // }

    // function handleFirstNode(firstNode: Node | undefined){
    //     setFirstNodeState(firstNode);
    // }

    // function getFirstNode(): Node | undefined{
    //     return firstNodeState;
    // }

    const [editorState,dispatch] = useReducer(editorStateReducer,initialState);

    const fetchNodeTypes = async () => {
        const nodeTypes = await getNodeTypes()
        // const results = nodeTypes
        // const users = results.map(parseUserData)
        console.log(nodeTypes)
        const newFlumeConfig = addNodeTypes(nodeTypes);
        dispatch({type:"setConfigState",value:newFlumeConfig});
        return dispatch({type:"setNodeDataState",value:{ ...editorState.nodeDataState, nodeTypes: [...nodeTypes] }})
    }

    useEffect(() => {
        fetchNodeTypes()
    },
        []
    )



    function getNodeEditor() {
        const editorDefaultNodes: DefaultNode[] = [];
        const uploadNode = editorState.nodeDataState.nodeTypes.find(x => x.modificationType === ModificationType.Upload);
        const downloadNode = editorState.nodeDataState.nodeTypes.find(x => x.modificationType === ModificationType.Download)
        if (uploadNode) {
            editorDefaultNodes.push({
                type: uploadNode.id,
                x: -500,
                y: -200
            })
        }

        if (downloadNode) {
            editorDefaultNodes.push({
                type: downloadNode.id,
                x: 500,
                y: 200
            })
        }

        if (editorState.configState) {
            return (<NodeEditor
                nodeTypes={editorState.configState.nodeTypes}
                portTypes={editorState.configState.portTypes}

                defaultNodes={editorDefaultNodes}
                nodes={editorState.nodeState}
                onChange={e => dispatch({type:"setNodeState", value:e})}
            />)
        }
        return (<h2>Loading, Please Wait...</h2>)

    }

    function getImage(){
        if(editorState.imageState){
            return (<img src={URL.createObjectURL(editorState.imageState)}></img>)
        }
        return(<h2>No image selected yet...</h2>)
    }

    return (
        <Container fluid className="h-75">
            <Row className="h-100">
                <div className="col-8">
                    {getNodeEditor()}
                </div>
                <div className="col-4">
                    <Stack direction="vertical" gap={5}>
                    <Update editorState={editorState} updateEditorState={dispatch} ></Update>
                        {getImage()}
                        <Stack direction="horizontal" gap={5}>
                            <Button>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-left" viewBox="0 0 16 16">
                                    <path fillRule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z" />
                                </svg>
                            </Button>
                            <Download editorState={editorState} updateEditorState={dispatch}></Download>
                            <Button>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-right" viewBox="0 0 16 16">
                                    <path fillRule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z" />
                                </svg>
                            </Button>
                        </Stack>
                    </Stack>
                    <SelectFile editorState={editorState} updateEditorState={dispatch}></SelectFile>



                </div>
            </Row>
        </Container>
    );
}

export default Editor;

