import { NodeType } from "./NodeType";
import { DataInputValue } from "./DataInputValue";
import { EntityBase } from "./EntityBase";

export interface DataInput extends EntityBase {
    dataInputType: DataInputType;
    label: string;
    name: string;
    nodeTypeId: string;
    nodeType?: NodeType;
    dataInputValues: DataInputValue[] | null;
}

export enum DataInputType {
    Number = 0,
    Text = 1,
    Boolean = 2
}