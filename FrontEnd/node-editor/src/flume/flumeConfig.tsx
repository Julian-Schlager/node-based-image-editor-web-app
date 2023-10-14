import { FlumeConfig, Controls, Colors, NodeType as FlumeNodeType } from "flume";
import { getConfigFileParsingDiagnostics } from "typescript";
import { NodeType } from "../Models/NodeType";
import { DataInput, DataInputType } from "../Models/DataInput";

export const flumeConfig = new FlumeConfig()

export function addNodeTypes(nodeTypes:NodeType[]){
    const newConfig = new FlumeConfig(flumeConfig);
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
        newConfig.
            addPortType({
                type: "image",
                name: "image",
                label: "Image",
                color: Colors.orange,
            })
        newConfig
            .addNodeType({
                type: "string",
                label: nodeType.description,
                inputs: ports => [
                    ports.image(),
                    ...nodeType.dataInputs.map(x => callMethodByName(mapToFlumeType(x.dataInputType),ports))
                ],
                  outputs: ports => [
                    ports.image()
                  ]
            })
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