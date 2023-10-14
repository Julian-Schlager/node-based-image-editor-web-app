import { User } from "./User";
import { EntityBase } from "./EntityBase";

export interface UserSettings extends EntityBase {
    user: User;
    userId: string;
}