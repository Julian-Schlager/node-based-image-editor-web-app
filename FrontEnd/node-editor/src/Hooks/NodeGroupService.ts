import { useState } from "react";
import configData from "../config.json"
import {Node} from "../Models/Node"
import { Stream } from "stream";
import { User } from "../Models/User";
import { getAuthHeader } from "./AuthHelper";
import { NodeGroup } from "../Models/NodeGroup";
import { createArrayOfNodes } from "./NodeMappingService";
import { NodeMap } from "flume";

export async function saveNodeGroup(email: string, password: string, firstNode:Node,nodeGroupName:string,flumeNodeMap:NodeMap):Promise<NodeGroup> {
    const nodeGroup:NodeGroup = {name:nodeGroupName,nodes:createArrayOfNodes(firstNode,[]),id:crypto.randomUUID(),flumeNodeMap:JSON.stringify(flumeNodeMap)} 
    const response = await fetch(configData.SERVER_URL + "/NodeGroup/Save",{method: "POST",headers:{"Content-Type": "application/json","Authorization":getAuthHeader(email,password)},body:JSON.stringify(nodeGroup)});
    const savedNodeGroup= await response.json();
    return savedNodeGroup;
}

export async function loadNodeGroups(email:string,password:string) {
    const response = await fetch(configData.SERVER_URL + "/NodeGroup/Load",{method: "GET",headers:{"Content-Type": "application/json","Authorization":getAuthHeader(email,password)}});
    const data: NodeGroup[] = await response.json();
    return data;
}

export async function deltecurrentNodeGroup(currentNodeGroupId:string,email:string,password:string) {
    const response = await fetch(configData.SERVER_URL + `/NodeGroup/Delete/${currentNodeGroupId}`,{method: "DELETE",headers:{"Content-Type": "application/json","Authorization":getAuthHeader(email,password)}});
    const data: boolean = await response.json();
    return data;
}