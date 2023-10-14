import { NodeType } from "./NodeType";
import { DataInputValue } from "./DataInputValue";
import { NodeGroup } from "./NodeGroup";
import { EntityBase } from "./EntityBase";

export interface Node extends EntityBase {
    nodeType: NodeType;
    nodeTypeId: string;
    dataInputValues: DataInputValue[];
    nodeGroup: NodeGroup;
    nodeGroupId: string;
}