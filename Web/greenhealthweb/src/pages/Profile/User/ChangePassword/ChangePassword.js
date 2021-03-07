import React, { Component } from "react";
import { apiUrl, webUrl } from "../../../../conf";

class ChangePassword extends Component {
    render() {
        return <div className="placeOrder-container wrapper">
            <div className="placeOrder">
                <form className="placeOrder-form" onSubmit={this.ChangePasswordHandler}>
                    <h1>Изменить пароль</h1>

                    <label htmlFor="OldPass"><b>Старый пароль</b></label>
                    <input type="password" placeholder="Пароль" name="OldPass" required maxlength="100"/>

                    <label htmlFor="NewPass"><b>Новый пароль</b></label>
                    <input type="password" placeholder="Пароль" name="NewPass" required maxlength="100"/>

                    <button type="submit" className="placeOrder-button">Изменить пароль</button>

                </form>
            </div>
        </div>
    }

    async ChangePasswordHandler(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        let object = {};
        data.forEach(function (value, key) {
            object[key] = value;
        });
        let json = JSON.stringify(object);
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));

        const requestOptions = {
            method: 'PUT',
            body: json,
            headers: {Authorization: 'Bearer ' + curUser.access_token, 'Content-Type': 'application/json', 'Accept': 'application/json'}
        }
        let response = await fetch(apiUrl + "api/v1/users/change-password", requestOptions);
        if (response.status != 200)
            alert("Something wrong");
        else {
            //document.location.replace("/");
            alert("Password changed");
        }
    }
}
export default ChangePassword;