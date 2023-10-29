import { useState, useEffect, useContext } from "react";
import { editImage } from "../Hooks/FileUploadService";
import { Node } from "../Models/Node"
import Spinner from 'react-bootstrap/Spinner';
import { mapFlumeNodes } from "../Hooks/NodeMappingService";
import { NodeMap } from "flume";
import { NodeType } from "../Models/NodeType";
import { NodeDataState } from "../Models/NodeDataState";
import { CompProps } from "../Models/CompProps";

export function Update(props : CompProps) {
  const [loading, setLoading] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");

  async function upload() {
    const currentFile = props.editorState.currentFile;
    if (currentFile) {
      setLoading(true)
      const firstNode =  props.editorState.firstNodeState;
      if (firstNode) {
        const response = await editImage(currentFile, firstNode);
        const imageBlob = await response.blob();
        console.log(imageBlob)
        props.updateEditorState({type:"setImageState",value:imageBlob})
      }
      setLoading(false)
    }
  };

  function loadingSpinner() {
    if (loading) {
      return <Spinner animation="border" />
    }
    return
  }

  function updateDiagram() {
    if (props.editorState.nodeState) {
      console.log(props.editorState.nodeState)
      const firstNode = mapFlumeNodes(props.editorState.nodeState, props.editorState.nodeDataState.nodeTypes)
      props.updateEditorState({type:"setFirstNodeState",value:firstNode});
    }

  }

  useEffect(()=>{upload()},[props.editorState.firstNodeState]) // https://react.dev/reference/react/useEffect, https://react.dev/learn/state-as-a-snapshot 

  return (
    <div>
      <div className="row">
        {loadingSpinner()}
        <div className="col-4">
          <button
            className="btn btn-success btn-sm"
            disabled={!props.editorState.currentFile}
            onClick={updateDiagram}
          >
            Refresh
          </button>
        </div>
      </div>

      {message && (
        <div className="alert alert-secondary mt-3" role="alert">
          {message}
        </div>
      )}
    </div>
  );
};