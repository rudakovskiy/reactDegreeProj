import React, {Component} from "react";
import { Link, Switch } from "react-router-dom";
import "./AdmimPanel.css";
import { webUrl } from "../../../conf";

class AdminPanel extends Component {
    render() {
        let user = JSON.parse(localStorage.getItem("currentUser"));

        return <div className="profile-container wrapper">
            <div className="profile">
                <div className="profile-user">
                    <h2>Добрый день, {user.fullName}</h2>
                    <ul>
                        <li><b>Вы проживаете по адрессу:</b> {user.city} <br/> {user.address} </li>
                        <li><b>Ваш номер: </b>{user.phone}</li>
                        <li><b>Ваша почта: </b>{user.email}</li>
                    </ul>
                </div>
                <div className="profile-menu">

                    <Link  to="profile/add-medicament"><div className="profile-menu-button">Добавить товар</div></Link>
                    <Link  to="profile/hide-medicament"><div className="profile-menu-button">Скрыть товар</div></Link>

                    <Link  to="profile/confirm-orders"><div className="profile-menu-button">Неподтвержденные заказы</div></Link>
                    <Link  to="profile/orders"><div className="profile-menu-button">Подтвержденные заказы</div></Link>

                    <Link to="/" onClick={this.logOutHandler}><div className="profile-menu-button profile-exit-button">Выход                    </div>
                    </Link>
                </div>
            </div>
        </div>//дописать профиль
    }

    async logOutHandler()
    {
        await localStorage.clear();
        window.location.replace(webUrl + "/sign-in");
    }

}
export default AdminPanel;