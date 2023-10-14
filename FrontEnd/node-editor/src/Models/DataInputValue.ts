import { DataInput } from "./DataInput";
import { Node } from "./Node";
import { EntityBase } from "./EntityBase";

export interface DataInputValue extends EntityBase {
    value: string;
    dataInputId: string;
    dataInput?: DataInput;
    nodeId: string;
    node?: Node;
}