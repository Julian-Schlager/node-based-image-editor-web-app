import { NodeType } from "../Models/NodeType"
import configData from "../config.json"

export const getNodeTypes = async () => {
  const response = await fetch(configData.SERVER_URL + "/NodeType")
  const data: NodeType[] = await response.json()
  return data
}
