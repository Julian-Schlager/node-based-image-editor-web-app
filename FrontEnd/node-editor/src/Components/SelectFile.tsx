import { Button } from "react-bootstrap";
import { CompProps } from "../Models/CompProps";

export function SelectFile(props: CompProps){
    function selectFile(event: React.ChangeEvent<HTMLInputElement>) {
        if(event.target.files){
          const file = event.target.files[0];
          props.updateEditorState({type:"setFileName",value:file.name})
          props.updateEditorState({type:"setCurrentFile",value:file});
          console.log(event);
        }
      };
    return(
        <input type="file" onChange={selectFile}></input>
    )
}