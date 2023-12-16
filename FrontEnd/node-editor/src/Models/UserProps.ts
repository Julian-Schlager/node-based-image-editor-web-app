import { EditorAction, EditorState } from "./EditorState";
import { UserAction, UserState } from "./UserState";

export interface UserProps{
    userState:UserState;
    updateUserState: (userAction:UserAction) => void;

    editorState:EditorState;
    updateEditorState: (editorAction:EditorAction) => void

    updateDiagram: () => void
}