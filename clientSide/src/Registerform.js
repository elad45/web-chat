import Message from "./Message";
import usersList from './usersDB'
import {Link ,useNavigate} from 'react-router-dom'
import './Registerform.css'
import { useState } from 'react'
import React from 'react';


 function Registerform() {
    const navigate = useNavigate();
    const RegisterClick = async(e) => {
        e.preventDefault();
        // inserting the user's input into variables
        var userID = document.getElementById("loginID").value;
        var userNick = document.getElementById("nickname").value;
        //var img = document.getElementById("Avatar").files[0];
        //if(img){
        //   var userAvatar=URL.createObjectURL(img)
        //var reader = new FileReader();
        //reader.onload = function () {
        //        userAvatar = reader.result;
        //    }
        //    reader.readAsDataURL(userAvatar);
        //}
        //else{
            var userAvatar = "https://www.gravatar.com/avatar/00000000000000000000000000000000?d=mp&f=y"
        //}
        var userPassword = document.getElementById("loginPassword").value;
        var passwordVerification = document.getElementById("verifyPassword").value;
        var paswd=  /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{5,}$/;
             //no blank password
        if (!userPassword){
                alert("Enter a password")
        }     
        else if (userPassword.length < 5) {
            alert("Password length should be at least 5 characters long")
        }
        
        else if(!userPassword.match(paswd)) {
            alert("Password must contain at least one numeric digit and a special character(!@#$%^&*)")
        }
        //checks if passwords are the same
        else if (passwordVerification !== userPassword) {
                    alert("Password doesn't match");
                }
        //create new User
        else {

            var valFetch = await fetch('http://localhost:5094/api/Register', {
                method: 'POST',
                headers: {
                    'Content-Type' : 'application/json'},
                body: JSON.stringify({Id: userID, Name: userNick ,Password: userPassword})
            })
            console.log(valFetch.status);
            //wrong username
            if(valFetch.status == 404)
                alert("Username already exists");
            //nickname already exists - should be removed.
            else if (valFetch.status == 405)
                alert("Nickname already exists");    
            //logged in
            else {
                localStorage.setItem('currentUser', userID);
                navigate("/chat");
            }
        }

    }
    return (
        <div className="registerbox">
            <span className="d-flex justify-content-center">
                <div>
                    <div className="d-flex justify-content-center">
                        <h1>Register page</h1>
                    </div>
                    <br />

                    <div className="row mb-3">
                        <label htmlFor="loginID" className="col-sm-3 col-form-label">Username*</label>
                        <div className="col-sm-7">
                            <input type="text" className="form-control" id="loginID" placeholder="Enter your userName" required />
                        </div>
                    </div>

                    <div className="row mb-3">
                        <label htmlFor="nickname" className="col-sm-3 col-form-label">Nickname*</label>
                        <div className="col-sm-7">
                            <input type="text" className="form-control" id="nickname" placeholder="Enter your nickname" required />
                        </div>
                    </div>


                    <div className="row mb-3" hidden>
                        <label htmlFor="Avatar" className="col-sm-3 col-form-label">Avatar</label>
                        <div className="col-sm-7">
                            <input type="file" className="form-control form-control-sm" id="Avatar" placeholder="Enter Avatar url" />
                        </div>
                    </div>

                    <div className="row mb-3">
                        <label htmlFor="loginPassword" className="col-sm-3 col-form-label">Password*</label>
                        <div className="col-sm-7">
                            <input type="password" className="form-control" id="loginPassword" placeholder="Enter password" required />
                        </div>
                    </div>

                    <div className="row mb-3">
                        <label htmlFor="verifyPassword" className="col-sm-3 col-form-label">Password verification*</label>
                        <div className="col-sm-7">
                            <input type="password" className="form-control" id="verifyPassword" placeholder="Enter password again" required />
                        </div>
                    </div>

                    <div className="row-sm">
                    <button type="button" onClick={RegisterClick} className="btn btn-primary">Register</button>
                        <label className="m-1">Already registered? click <Link to="/">here</Link> to login</label>
                    </div>
                </div>
            </span>
            </div>
    );
}

export default Registerform