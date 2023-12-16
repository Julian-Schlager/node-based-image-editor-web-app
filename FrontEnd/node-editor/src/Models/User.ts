import { NodeGroup } from "./NodeGroup";
import { UserSettings } from "./UserSettings";
import { EntityBase } from "./EntityBase";

export interface User extends EntityBase {
    email:string;
    password:string;
    nodeGroups: NodeGroup[];
    userSettings: UserSettings;
}