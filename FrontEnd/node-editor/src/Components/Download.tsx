import { Button, ListGroup, ListGroupItem } from "react-bootstrap";
import {saveAs} from "file-saver";
import { CompProps } from "../Models/CompProps";

export function Download(props: CompProps){
    
    function downloadImage(){
        props.updateEditorState({type:"setflumeRerenderKey",value:new Date().toISOString()});
        console.log(props.editorState.imageState)
        if(props.editorState.imageState){
            props.updateDiagram()
            saveAs(props.editorState.imageState, props.editorState.fileName);
        }
    }

    return(
        <Button variant="primary" onClick={downloadImage}>Download</Button>
    )
}