import { Dispatch } from "react";
import { EditorAction, EditorState } from "./EditorState";

export interface CompProps{
    editorState:EditorState;
    updateEditorState: (editorAction:EditorAction) => void
}