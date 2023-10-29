import { useState } from "react";
import configData from "../config.json"
import {Node} from "../Models/Node"
import { Stream } from "stream";

export async function editImage(file:File,firstNode:Node) {
    const formData = new FormData();
    formData.append("formFile",file)
    formData.append("fileName",file.name)
    formData.append("firstNodeJson",JSON.stringify(firstNode))
    // const headers = new Headers();
    // headers.append("Content-Type", "multipart/form-data");
    const response = await fetch(configData.SERVER_URL + "/Image/Edit",{method: "POST",body: formData});
    console.log(response);
    return response;
}