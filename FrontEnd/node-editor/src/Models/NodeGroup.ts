import { Node } from "./Node";
import { User } from "./User";
import { EntityBase } from "./EntityBase";
import { NodeMap } from "flume";

export interface NodeGroup extends EntityBase {
    nodes: Node[];
    user?: User;
    userId?: string;
    name?:string;
    flumeNodeMap:string;
}