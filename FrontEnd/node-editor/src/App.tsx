import React, { useEffect, useReducer } from 'react';
import logo from './logo.svg';
import './App.css';
import Button from 'react-bootstrap/Button';

import { BrowserRouter as Router, Route, Link, RouterProvider, createBrowserRouter } from 'react-router-dom';
import NavBar from './Components/NavBar';
import Editor from './Components/Editor';
import Login from './Components/Login';
import { UserState, userStateReducer } from './Models/UserState';
import { EditorState, editorStateReducer } from './Models/EditorState';
import { login } from './Hooks/UserService';
import { User } from './Models/User';
import { loadNodeGroups } from './Hooks/NodeGroupService';
import { mapFlumeNodes } from './Hooks/NodeMappingService';

const userInitialState: UserState = { isLoggedIn: false };
const editorInitialState: EditorState = { nodeDataState: { nodeTypes: [] },flumeRerenderKey:new Date().toISOString() };

function App() {
  const [userState, userDispatch] = useReducer(userStateReducer, userInitialState);
  const [editorState, editorDispatch] = useReducer(editorStateReducer, editorInitialState);

  async function getUserFromSessionStorage() {
    const userE = sessionStorage.getItem("user");
    if (userE) {
      const userD = window.atob(userE);
      const currentUser = JSON.parse(userD) as User;

      if (currentUser.email && currentUser.password) {
        const user = await login(currentUser.email, currentUser.password)
        if (user) {
          userDispatch({ type: "setIfLoggedIn", value: true })
          userDispatch({ type: "setEmail", value: currentUser.email })
          userDispatch({ type: "setPassword", value: currentUser.password })
          const nodeGroups = await loadNodeGroups(currentUser.email, currentUser.password)
          editorDispatch({ type: "setNodeGroups", value: nodeGroups })
        }
      }
    }
  }

  function updateDiagram() {
    if (editorState.nodeState) {
      console.log(editorState.nodeState)
      const firstNode = mapFlumeNodes(editorState.nodeState, editorState.nodeDataState.nodeTypes)
      editorDispatch({type:"setFirstNodeState",value:firstNode});
    }
  }

  useEffect(() => {
    getUserFromSessionStorage()
  },
    []
  )

  const routes = [
    {
      path: "/",
      element: <Editor userState={userState} updateUserState={userDispatch} editorState={editorState} updateEditorState={editorDispatch} updateDiagram={updateDiagram} />,
    },
    {
      path: "/login",
      element: <Login userState={userState} updateUserState={userDispatch} editorState={editorState} updateEditorState={editorDispatch}  updateDiagram={updateDiagram}  />,
    }
  ];
  return (
    <div className='vh-100' >
      <NavBar userState={userState} updateUserState={userDispatch} editorState={editorState} updateEditorState={editorDispatch}  updateDiagram={updateDiagram}  />
      <div className='vh-100' style={{ margin: "20px" }}>
        <RouterProvider router={createBrowserRouter(routes)} />
      </div>
    </div>
  );
}

export default App;
