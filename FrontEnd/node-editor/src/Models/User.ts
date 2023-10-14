import { NodeGroup } from "./NodeGroup";
import { UserSettings } from "./UserSettings";
import { EntityBase } from "./EntityBase";

export interface User extends EntityBase {
    nodeGroups: NodeGroup[];
    userSettings: UserSettings;
}