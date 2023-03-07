import {Link, useNavigate} from "react-router-dom";
import usersList from './usersDB'
import './Loginform.css'
import React from 'react';


function LoginForm() {
    const navigate = useNavigate();

    const loginClick = async () => {
        let loggingPassword,loggingID;
        loggingPassword = document.getElementById("loginPassword").value;
        loggingID = document.getElementById("loginID").value;

        //valFetch is the returned value from login page
        
        var valFetch = await fetch('http://localhost:5094/api/Login', {
            method: 'POST',
            headers: {
                'Content-Type' : 'application/json'},
            body: JSON.stringify({Id: loggingID, Password: loggingPassword})
        })
        console.log(valFetch.status);
        //wrong username
        if(valFetch.status == 404)
            alert("No such user");
        //wrong password
        else if (valFetch.status == 400)
            alert("Wrong password");    
        //logged in
        else {
            localStorage.setItem('currentUser', loggingID);
            navigate("/chat");
        }
    }

    return (
        <div className="loginbox">
        <form action="">
        <span className="d-flex justify-content-center">
            <div>
                <div className="d-flex justify-content-center">
                    <h1>Login page</h1>
                </div>
                <br />
                <div className="row mb-3">
                    <label htmlFor="loginID" className="col-sm-3 col-form-label col-form-label-sm">Username</label>
                    <div className="col-sm-7">
                        <input type="username" className="form-control form-control-sm" id="loginID" placeholder="Enter Username" required/>
                    </div>
                </div>
                
                <div className="row mb-3">
                    <label htmlFor="loginPassword" className="col-sm-3 col-form-label-sm">Password</label>
                    <div className="col-sm-7">
                        <input type="password" className="form-control form-control-sm" id="loginPassword" placeholder="Enter password" required/>
                    </div>
                </div>
                <div className="row-sm">
                    <button type="button" className="btn btn-secondary" onClick = {loginClick}>Login</button>
                    <label className="m-1">Not registered? click <Link to="/register">here</Link> to register</label>
                </div>
            </div>            
        </span>
        </form>
        </div>
    );
}

export default LoginForm