import { Button, Modal } from 'react-bootstrap/'
import { useState, useEffect } from 'react'
import React from 'react'
import usersList from '../usersDB';
import './AddFriend.css'

//using logging.user.nickname
//logginguser.friends
//using setFriends
function AddFriend(props) {
    
    //const [AllUsernames,setAllUsernames]  = useState("");
    
    const fetchusernames = async(friendServer) => {
        console.log(friendServer)
        const response = await fetch(`http://${friendServer}/api/Users/GetAllUsers`,{
            method:'get',
            headers: {
                'Content-Type' : 'application/json'},
        })
        if (response.status != 404){
        var usernames = await response.json();
        //setAllUsernames(usernames)
        console.log(usernames)
        return usernames;
        }
    }
    useEffect(() =>{
        // fetchusernames();
        //console.log(AllUsernames);
    },[]);
    
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    
    
    const handleAdd = async () => {
        let friendID = document.getElementById("friendID").value
        let friendNick = document.getElementById("friendNick").value
        let friendServer = document.getElementById("friendServer").value
        let AllUsernames = await fetchusernames(friendServer);

        let friendUser = AllUsernames.find(x => x == friendID)
        console.log(friendUser)
        console.log(props.contactsData);
        
        if (friendUser) {
            //User can't add himself as a contact
            if (props.loggedPersonUsername==friendID) {
                alert("User can't add himself as a contact");
                return;
            }
            //User can't add a friend that already in his contact list
            if (props.contactsData.find(contact => contact.id == friendID)) {
                alert("Friend already in contacts list");
                return;
            }

            ///works now its risky
            props.setContactsData((currentContactsData) => {
                ///////////////////////////////////////////
                //now adding friend to database
            const addFriend = async(e) => {    
                var valFetch = await fetch('http://localhost:5094/api/Contacts', {
                    method: 'POST',
                    headers: {
                    'Content-Type' : 'application/json'},
                    body: JSON.stringify({Id: friendID, Name: friendNick ,server: friendServer, user: props.loggedPersonUsername})
                })
                console.log(valFetch.status);
                }
                
                addFriend();
                ///////////////////////////////////////////
                
            const invitation = async(e) => {    
                var valFetch = await fetch(`http://${friendServer}/api/Invitations`, {
                    method: 'POST',
                    headers: {
                    'Content-Type' : 'application/json'},
                    body: JSON.stringify({from: props.loggedPersonUsername, to: friendID ,server: "localhost:5094"})
                })
                console.log(valFetch.status);
                }
                invitation();
                ///////////////////////////////////////////
                
                
                
                
                
                let newFriends = [...currentContactsData];
                let newContact = {id: friendID, name: friendNick ,server: friendServer, last: null, lastdate:null} 
                newFriends.push(newContact)
                handleClose();
                console.log(props.contactsData);
                return newFriends;
            })
        }
        //No such friend in database
        else{
            alert("Friend doesn't exist")
        }
    }

    return (
        <div>
            <Button id="addFriendButton" onClick={handleShow}>Add friend</Button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add friend to chat</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>
                    <input placeholder="Enter friend's username" id="friendID"></input>
                    </div>
                    <div>
                    <input placeholder="Enter friend's nickname" id="friendNick"></input>
                    </div>
                    <div>
                    <input placeholder="Enter friend's server" id="friendServer"></input>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleAdd}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}
export default AddFriend;