import { FlumeComment, FlumeConfig, NodeMap } from "flume";
import { NodeDataState } from "./NodeDataState";
import { Node } from "../Models/Node"
import { type } from "os";

export interface EditorState {
    nodeDataState: NodeDataState;
    configState?: FlumeConfig;
    nodeState?: NodeMap;
    firstNodeState?: Node;
    imageState?: Blob;
    currentFile?: File;
    fileName?:string;
}

export type EditorAction =
    | { type: "setNodeDataState"; value: EditorState["nodeDataState"] }
    | { type: "setConfigState"; value: EditorState["configState"] }
    | { type: "setNodeState"; value: EditorState["nodeState"] }
    | { type: "setFirstNodeState"; value: EditorState["firstNodeState"] }
    | { type: "setImageState"; value: EditorState["imageState"] }
    | { type: "setCurrentFile"; value: EditorState["currentFile"] }
    | { type: "setFileName"; value: EditorState["fileName"] }

export function editorStateReducer(editorState: EditorState, editorAction: EditorAction): EditorState {
    switch (editorAction.type) {
        case "setNodeDataState":
            return {...editorState, nodeDataState:editorAction.value}
        case "setConfigState":
            return {...editorState, configState:editorAction.value}
        case "setNodeState":
            editorState.nodeState = editorAction.value;
            return editorState;
        case "setFirstNodeState":
            return {...editorState, firstNodeState:editorAction.value}
        case "setImageState":
            return {...editorState, imageState:editorAction.value}
        case "setCurrentFile":
            return {...editorState, currentFile:editorAction.value}
        case "setFileName":
            return {...editorState, fileName:editorAction.value}
        default:
            throw new Error("Unknown action");
    }
}