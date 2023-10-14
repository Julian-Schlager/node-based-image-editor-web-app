import { Node } from "./Node";
import { User } from "./User";
import { EntityBase } from "./EntityBase";

export interface NodeGroup extends EntityBase {
    nodes: Node[];
    user: User;
    userId: string;
}