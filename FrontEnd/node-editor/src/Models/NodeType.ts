import { DataInput } from "./DataInput";
import {Node} from "./Node";

export interface NodeType{

    id: string;
    createdAt: Date;
    lastModifiedAt:	Date;
    description: string;
    modificationType: ModificationType;
    dataInputs:DataInput[];
    nodes:Node[];

}

export enum ModificationType
{
    Brightness=0,
    Saturation=1,
    Contrast=2,
    Text=3,
    Rotate=4,
    Resize=5,
    Crop=6
}