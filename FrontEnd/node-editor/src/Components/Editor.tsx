import { ChangeEvent, createContext, useEffect, useReducer, useRef, useState } from "react";
import { getNodeTypes } from "../Hooks/NodeTypeService";
import { ModificationType, NodeType } from "../Models/NodeType";
import { Node } from "../Models/Node"
import { Button, Form, InputGroup, ListGroup, ListGroupItem, Modal } from "react-bootstrap";
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
import { UserProps } from "../Models/UserProps";
import { deltecurrentNodeGroup, loadNodeGroups, saveNodeGroup } from "../Hooks/NodeGroupService";



function Editor(props: UserProps) {

    const initNodeTypes: NodeType[] = []

    const [show, setShow] = useState(false);

    function handleClose() {
        return setShow(false);
    }
    function handleShow() {
        props.updateDiagram()
        return setShow(true);
    }

    async function fetchNodeTypes() {
        const nodeTypes = await getNodeTypes();
        const newFlumeConfig = addNodeTypes(nodeTypes, { editorState: props.editorState, updateEditorState: props.updateEditorState, updateDiagram: props.updateDiagram });
        props.updateEditorState({ type: "setConfigState", value: newFlumeConfig });
        return props.updateEditorState({ type: "setNodeDataState", value: { ...props.editorState.nodeDataState, nodeTypes: [...nodeTypes] } });
    }

    useEffect(() => {
        fetchNodeTypes()
    },
        []
    )



    function getNodeEditor() {
        const editorDefaultNodes: DefaultNode[] = [];
        const uploadNode = props.editorState.nodeDataState.nodeTypes.find(x => x.modificationType === ModificationType.Upload);
        const downloadNode = props.editorState.nodeDataState.nodeTypes.find(x => x.modificationType === ModificationType.Download)
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

        if (props.editorState.configState) {
            return (<NodeEditor
                nodeTypes={props.editorState.configState.nodeTypes}
                portTypes={props.editorState.configState.portTypes}

                defaultNodes={editorDefaultNodes}
                nodes={props.editorState.nodeState}
                onChange={e => props.updateEditorState({ type: "setNodeState", value: e })}
                key={props.editorState.flumeRerenderKey}
            />)
        }
        return (<h2>Loading, Please Wait...</h2>)

    }

    function getImage() {
        if (props.editorState.imageState) {
            return (<img src={URL.createObjectURL(props.editorState.imageState)}></img>)
        }
        return (<h2>No image selected yet...</h2>)
    }

    async function save() {
        if (props.userState.email && props.userState.password && props.editorState.firstNodeState && props.editorState.nodeGroupName && props.editorState.nodeState) {
            const newGroup = await saveNodeGroup(props.userState.email, props.userState.password, props.editorState.firstNodeState, props.editorState.nodeGroupName, props.editorState.nodeState)
            if (newGroup) {
                const nodeGroups = await loadNodeGroups(props.userState.email, props.userState.password)
                props.updateEditorState({ type: "setNodeGroups", value: nodeGroups })
            }

            handleClose()
        }
        else {
            console.log(`${props.userState.email},${props.userState.password},${props.editorState.firstNodeState},${props.editorState.nodeGroupName}`)
        }
    }

    function getNodeGroupSelect() {
        if (props.userState.isLoggedIn) {
            return (<Form.Select aria-label="Default select example" className="w-50" onChange={value => onSelectNodeGroup(value)}>
                <option value={-1} key={-1}></option>
                {props.editorState.nodeGroups?.map(x => (
                    <option value={x.id} key={x.id}>{x.name}</option>
                ))}
            </Form.Select>
            );
        }
    }

    function onSelectNodeGroup(value: ChangeEvent<HTMLSelectElement>) {
        const nodeGroupId = value.target.value;
        updateSelectedNodeGroup(nodeGroupId);
    }

    function updateSelectedNodeGroup(nodeGroupId: string) {
        props.updateEditorState({ type: "setCurrentSelectedNodeGroup", value: nodeGroupId });
        if (nodeGroupId === "-1") {
            props.updateEditorState({ type: "setNodeState", value: undefined });
            props.updateEditorState({type:"setflumeRerenderKey",value:new Date().toISOString()});
        }
        if (props.editorState.nodeGroups) {
            const selectedNodeGroup = props.editorState.nodeGroups.find(x => x.id === nodeGroupId);
            if (selectedNodeGroup?.flumeNodeMap) {
                props.updateEditorState({ type: "setNodeState", value: JSON.parse(selectedNodeGroup?.flumeNodeMap) });
                props.updateEditorState({type:"setflumeRerenderKey",value:new Date().toISOString()});
            }
        }
    }

    function showSaveNodeGroupButton() {
        if (props.userState.isLoggedIn) {
            return (
                <Button
                    className="btn btn-success btn-sm"
                    onClick={handleShow}>
                    Save Node Group
                </Button>);
        }

    }

    async function delteNodeGroup(){
        if(props.editorState.currentSelectedNodeGroup && props.userState.email &&props.userState.password){
            const isDeleted = await deltecurrentNodeGroup(props.editorState.currentSelectedNodeGroup,props.userState.email,props.userState.password)
            if(isDeleted){
                let existingNodeGroups = props.editorState.nodeGroups;
                existingNodeGroups = existingNodeGroups?.filter(x=>x.id !== props.editorState.currentSelectedNodeGroup)
                updateSelectedNodeGroup("-1");
                props.updateEditorState({type:"setNodeGroups",value:existingNodeGroups})
            }
        }
    }

    function showDelteNodeGroupButton() {
        if (props.userState.isLoggedIn) {
            return (
                <Button  className="btn btn-success btn-sm" onClick={delteNodeGroup}>
                    Delete current Node Group
                </Button>
            )
        }

    }

    return (
        <Container fluid className="h-75">
            <Row className="h-100">
                <div className="col-8">
                    {getNodeEditor()}
                </div>
                <div className="col-4">
                    <Stack direction="vertical" gap={5}>
                        <Stack direction="horizontal" gap={5}>
                            <Update editorState={props.editorState} updateEditorState={props.updateEditorState} updateDiagram={props.updateDiagram}></Update>
                            {showSaveNodeGroupButton()}

                            <Modal show={show} onHide={handleClose}>
                                <Modal.Header closeButton>
                                    <Modal.Title>Node Group</Modal.Title>
                                </Modal.Header>
                                <Modal.Body>
                                    <InputGroup className="mb-3">
                                        <InputGroup.Text id="inputGroup-sizing-default">
                                            Node Group Name
                                        </InputGroup.Text>
                                        <Form.Control
                                            aria-label="Node Group Name"
                                            aria-describedby="inputGroup-sizing-default"
                                            onChange={(event) => props.updateEditorState({ type: "setNodeGroupName", value: event.target.value })}
                                        />
                                    </InputGroup>
                                </Modal.Body>
                                <Modal.Footer>
                                    <Button variant="secondary" onClick={handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" onClick={save}>
                                        Save Node Group
                                    </Button>
                                </Modal.Footer>
                            </Modal>

                            {getNodeGroupSelect()}

                            {showDelteNodeGroupButton()}
                        </Stack>
                        {getImage()}
                        <Stack direction="horizontal" gap={5}>
                            {/* <Button>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-left" viewBox="0 0 16 16">
                                    <path fillRule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z" />
                                </svg>
                            </Button> */}
                            <Download editorState={props.editorState} updateEditorState={props.updateEditorState} updateDiagram={props.updateDiagram}></Download>
                            {/* <Button>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chevron-right" viewBox="0 0 16 16">
                                    <path fillRule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z" />
                                </svg>
                            </Button> */}
                        </Stack>
                    </Stack>
                </div>
            </Row>
        </Container>
    );
}

export default Editor;

