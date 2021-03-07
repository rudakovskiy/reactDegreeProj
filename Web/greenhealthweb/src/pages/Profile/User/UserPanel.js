import React, {Component} from "react";
import { Link } from "react-router-dom";
import "./UserPanel.css"
import { webUrl } from "../../../conf";

class UserPanel extends Component {
    render() {
        let user = JSON.parse(localStorage.getItem("currentUser"));

        return <div className="profile-container wrapper">
            <div className="profile">
                <div className="profile-user">

            <h2>Добрый день, {user.fullName}</h2>
            <ul>
                <li><b>Вы проживаете по адрессу:</b> {user.city} <br/> {user.address} </li>
                <li><b>Ваш логин: </b>{user.login}</li>
                <li><b>Ваш номер: </b>{user.phone}</li>
                <li><b>Ваша почта: </b>{user.email}</li>
            </ul>
                </div >
                <div className="profile-menu">
                    <Link to="/profile/my-orders"><div className="profile-menu-button">Мои заказы</div></Link>
                    <Link to="/profile/change-password" ><div className="profile-menu-button">Изменить пароль</div></Link>
                    <Link to="/" onClick={this.logOutHandler}><div className="profile-menu-button">Выход</div></Link>

                </div>
                </div>
            </div>//дописать профиль
    }

    async logOutHandler()
    {
        await localStorage.clear();
        window.location.replace(webUrl + "sign-in");
    }

}
export default UserPanel;