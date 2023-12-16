import { FlumeConfig, Controls, Colors, NodeType as FlumeNodeType } from "flume";
import { getConfigFileParsingDiagnostics } from "typescript";
import { ModificationType, NodeType } from "../Models/NodeType";
import { DataInput, DataInputType } from "../Models/DataInput";
import { Update } from "../Components/Update";
import { SelectFile } from "../Components/SelectFile";
import { CompProps } from "../Models/CompProps";
import { Download } from "../Components/Download";

// export const flumeConfig = new FlumeConfig()

export function addNodeTypes(nodeTypes:NodeType[],props:CompProps){
    const newConfig = new FlumeConfig();

    initNodePortConfig(newConfig,nodeTypes);

    nodeTypes.forEach(nodeType => {
        nodeType.dataInputs.forEach(dataInput => {
            newConfig
                .addPortType({
                    type: mapToFlumeType(dataInput.dataInputType),
                    name: dataInput.name,
                    label: dataInput.label,
                    color: Colors.green,
                    controls:[
                        mapToFlumeControl(dataInput)
                    ]
                })
        });
        
        if(nodeType.modificationType != ModificationType.Upload && nodeType.modificationType != ModificationType.Download){
            newConfig
            .addNodeType({
                type: nodeType.id,
                label: nodeType.description,
                inputs: ports => [
                    ports.image(),
                    ...nodeType.dataInputs.map(x => callMethodByName(mapToFlumeType(x.dataInputType),ports))
                ],
                  outputs: ports => [
                    ports.image()
                  ]
            })
        }
        
        
        if(nodeType.modificationType === ModificationType.Download){
            newConfig.addPortType({
                type: "downloadImage",
                name: "downloadImage",
                label: "DownloadImage",
                controls: [
                    Controls.custom({
                        name: "downloadImage",
                        label: "Download Image",
                        render: () =>(
                            <Download editorState={props.editorState} updateEditorState={props.updateEditorState}  updateDiagram={props.updateDiagram}/>
                        )
                    })
                ]
            })
            newConfig
                .addRootNodeType({
                    type: nodeType.id,
                    label: nodeType.description,
                    initialWidth: 170,
                    inputs: ports => [
                        ports.image(),
                        ports.downloadImage()
                    ],
            })
        }

        if(nodeType.modificationType === ModificationType.Upload){
            newConfig.addPortType({
                type: "uploadImage",
                name: "uploadImage",
                label: "UploadImage",
                controls: [
                    Controls.custom({
                        name: "uploadImage",
                        label: "Upload Image",
                        render: () =>(
                            <SelectFile editorState={props.editorState} updateEditorState={props.updateEditorState} updateDiagram={props.updateDiagram}/>
                        )
                    })
                ]
            })
            newConfig
                .addRootNodeType({
                    type: nodeType.id,
                    label: nodeType.description,
                    initialWidth: 250,
                    inputs: ports => [
                        ports.uploadImage()
                    ],
                    outputs: ports => [
                        ports.image()
                    ],
                
            })
        }
    });
    // newConfig
    //   .addPortType({
    //     type: "number",
    //     name: "number",
    //     label: "Number",
    //     color: Colors.red,
    //     controls: [
    //       Controls.number({
    //         label: "Number",
    //         name: "number"
    //       })
    //     ]
    //   })
    //   .addNodeType({
    //     type: "number",
    //     label: "Number",
    //     inputs: (ports) => [ports.number()],
    //     outputs: (ports) => [ports.number()]
    //   });
    return newConfig;
}


function initNodePortConfig(newConfig: FlumeConfig,nodeTypes:NodeType[]) {
    newConfig
        .addPortType({
            type: "image",
            name: "image",
            label: "Image",
            color: Colors.orange,
        });
}

function mapToFlumeType(dataInput:DataInputType){
    switch (dataInput) {
        case DataInputType.Number:
            return "number";
        case DataInputType.Text:
            return "string";  
        case DataInputType.Boolean:
            return "boolean";
        default:
            throw Error("DataInputType is undifind!");
    }
}

function mapToFlumeControl(dataInput:DataInput){
    switch (dataInput.dataInputType) {
        case DataInputType.Number:
            return Controls.number({
                name: dataInput.name,
                label: dataInput.label
            });
        case DataInputType.Text:
            return Controls.text({
                name: dataInput.name,
                label: dataInput.label
            }); 
        case DataInputType.Boolean:
            return Controls.checkbox({
                name: dataInput.name,
                label: dataInput.label
            });
        default:
            throw Error("DataInputType is undifind!");
    }
}

function callMethodByName(name:string, object:any){
    const method = object[name]
    if(method){
        return method();
    } else {
        throw Error("Method not found: "+ name);
    }

}