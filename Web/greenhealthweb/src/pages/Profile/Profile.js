import React, {Component} from "react";
import { Link } from "react-router-dom";
import { apiUrl, webUrl } from "../../conf";
import UserPanel  from "./User/UserPanel";
import AdminPanel from "./AdminPanel/AdminPanel";

class Profile extends Component {
    render() {
        let user = JSON.parse(localStorage.getItem("currentUser"));
        if (user == undefined)
            window.location.replace(webUrl + "sign-in");//dont work. redirect to signin
        if (user.role == "customer")
            return <UserPanel />;
        else return <AdminPanel />;
    }


}
export default Profile;