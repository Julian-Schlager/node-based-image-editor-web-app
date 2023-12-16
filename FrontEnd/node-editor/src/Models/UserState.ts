import { FlumeComment, FlumeConfig, NodeMap } from "flume";
import { User } from "../Models/User"
import { type } from "os";

export interface UserState {
    email?: string,
    password?: string,
    confirmPassword?: string,
    isLoggedIn: boolean
}

export type UserAction =
    | { type: "setEmail"; value: UserState["email"] }
    | { type: "setPassword"; value: UserState["password"] }
    | { type: "setConfirmPassword"; value: UserState["confirmPassword"] }
    | { type: "setIfLoggedIn"; value: UserState["isLoggedIn"] }
    | { type: "setLogOut";}


export function userStateReducer(userState: UserState, userAction: UserAction): UserState {
    switch (userAction.type) {
        case "setEmail":
            return { ...userState, email: userAction.value }
        case "setPassword":
            return { ...userState, password: userAction.value }
        case "setConfirmPassword":
            return { ...userState, confirmPassword: userAction.value }
        case "setIfLoggedIn":
            return { ...userState, isLoggedIn: userAction.value }
        case "setLogOut":
            return { email:undefined,password:undefined,confirmPassword:undefined,isLoggedIn:false }
        default:
            throw new Error("Unknown action");
    }
}