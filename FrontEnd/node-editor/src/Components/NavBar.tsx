import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { UserProps } from '../Models/UserProps';
import { useNavigate } from 'react-router-dom';

function NavBar(props: UserProps) {

  function showSignedIn() {
    if (props.userState.isLoggedIn) {
      return (
        <Navbar.Collapse className="justify-content-end">
          <Navbar.Text>
            Signed in as: {props.userState.email}
          </Navbar.Text>
        </Navbar.Collapse>
      );
    }
  }

  function logOut(){
    sessionStorage.clear()
    props.updateUserState({type:"setLogOut"})
  }

  function showLogin(){
    if(props.userState.isLoggedIn){
      return(
        <Nav.Link href='/login' onClick={_=>logOut()}>LogOut</Nav.Link>
      )
    }
    else{
      return(
        <Nav.Link href="login">Login</Nav.Link>
      )
    }
  }

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand href="/">Node Image Editor</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/">Home</Nav.Link>
            {showLogin()}
          </Nav>
        </Navbar.Collapse>
        {showSignedIn()}
      </Container>
    </Navbar>
  );
}

export default NavBar;