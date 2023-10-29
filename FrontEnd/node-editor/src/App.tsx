import React from 'react';
import logo from './logo.svg';
import './App.css';
import Button from 'react-bootstrap/Button';

import { BrowserRouter as Router, Route, Link, RouterProvider, createBrowserRouter} from 'react-router-dom';
import NavBar from './Components/NavBar';
import Editor from './Components/Editor';
import Login from './Components/Login';
import Registration from './Components/Registration';


function App() {

  const routes = [
    {
      path:"/",
      element: <Editor />,
    },
    {
      path:"/login",
      element: <Login />,
    },
    {
      path:"/registration",
      element: <Registration />,
    }
  ];
  return (
    <div className='vh-100'>
      <NavBar/>
      <RouterProvider router={createBrowserRouter(routes)} />
    </div>
    );
}

export default App;
