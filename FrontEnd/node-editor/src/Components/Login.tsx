import { useReducer } from "react";
import { Button, Container, Form, InputGroup, Row, Tab, Tabs } from "react-bootstrap";
import { UserAction, UserState, userStateReducer } from "../Models/UserState";
import { login, register } from "../Hooks/UserService";
import { useNavigate } from "react-router-dom";
import { type } from "os";
import { loadNodeGroups } from "../Hooks/NodeGroupService";
import { UserProps } from "../Models/UserProps";



function Login(props:UserProps) {

    const navigate = useNavigate();

    async function OnLogInClick(){
        if(props.userState.email&&props.userState.password){
            const user = await login(props.userState.email,props.userState.password)
            if(user){
                user.password = props.userState.password;
                props.updateUserState({type:"setIfLoggedIn",value:true})
                navigate("/")
                const userE = window.btoa(JSON.stringify(user))
                sessionStorage.setItem("user",userE)
                const nodeGroups =  await loadNodeGroups(props.userState.email,props.userState.password)
                props.updateEditorState({type:"setNodeGroups",value:nodeGroups})
            }
            else{
                props.updateUserState({type:"setIfLoggedIn",value:false})
            }
        }
    }
    async function OnRegisterClick(){
        if(props.userState.email&&props.userState.password&&props.userState.confirmPassword === props.userState.password){
            const user = await register(props.userState.email,props.userState.password,props.userState.confirmPassword)
            if(user){
                props.updateUserState({type:"setIfLoggedIn",value:true})
                navigate("/")
            }
            else{
                props.updateUserState({type:"setIfLoggedIn",value:false})
            }
        }
    }

    return (
        <Tabs
            defaultActiveKey="LogIn"
            id="uncontrolled-tab-example"
            className="mb-3"
        >
            <Tab eventKey="LogIn" title="LogIn">
                <Form className="mx-auto" style={{ width: '25%' }}>
                    <Form.Group className="mb-3" controlId="formGroupEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control onChange={(event)=>props.updateUserState({type:"setEmail",value:event.target.value})} type="email" placeholder="Enter email" /> 
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formGroupPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control onChange={(event)=>props.updateUserState({type:"setPassword",value:event.target.value})} type="password" placeholder="Password" />
                    </Form.Group>
                    <Button onClick={OnLogInClick} variant="dark">Login</Button>
                </Form>
            </Tab>
            <Tab eventKey="Register" title="Register">
                <Form className="mx-auto" style={{ width: '25%' }}>
                    <Form.Group className="mb-3" controlId="formGroupEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control onChange={(event)=>props.updateUserState({type:"setEmail",value:event.target.value})} type="email" placeholder="Enter email" />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formGroupPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control onChange={(event)=>props.updateUserState({type:"setPassword",value:event.target.value})} type="password" placeholder="Password" />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formGroupPassword">
                        <Form.Label>Confirm Password</Form.Label>
                        <Form.Control onChange={(event)=>props.updateUserState({type:"setConfirmPassword",value:event.target.value})} type="password" placeholder="Confirm Password" />
                    </Form.Group>
                    <Button onClick={OnRegisterClick} variant="dark">Register</Button>
                </Form>
            </Tab>
        </Tabs>


    );
}

export default Login;