import { NodeType } from "../Models/NodeType"
import configData from "../config.json"

export async function getNodeTypes() {
  const response = await fetch(configData.SERVER_URL + "/NodeType")
  const data: NodeType[] = await response.json()
  return data
}
