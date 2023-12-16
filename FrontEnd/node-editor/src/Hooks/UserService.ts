import { User } from "../Models/User"
import configData from "../config.json"

export async function login(email: string, password: string) {
    const response = await fetch(configData.SERVER_URL + "/User/Login",{method: "POST",headers:{"Content-Type": "application/json"},body:JSON.stringify({email,password})});
    const user: User = await response.json();
    console.log(user);
    return user;
}

export async function register(email: string, password: string, confirmPassword: string) {
    const response = await fetch(configData.SERVER_URL + "/User/Register",{method: "POST",headers:{"Content-Type": "application/json"},body:JSON.stringify({email,password,confirmPassword})});
    const user: User = await response.json();
    console.log(JSON.stringify({email,password,confirmPassword}))
    console.log(user);
    return user;
}

