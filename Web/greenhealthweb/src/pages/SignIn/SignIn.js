import React, {Component} from "react";
import "./SignIn.css";
import {Link, useHistory} from "react-router-dom";
import { apiUrl, webUrl } from "../../conf";

class SignIn extends Component {
    constructor(props) {
        super(props);

    }

    render() {
        return <div className="signin-container">
            <div className="signin">
                <form className="signin-form" method="POST" onSubmit={this.handleSubmit}>
                    <h1>Вход</h1>

                    <label htmlFor="phone"><b>Телефон</b></label>
                    <input type="text" placeholder="Enter Phone" name="phone" required maxlength="15"/>

                    <label htmlFor="password"><b>Пароль</b></label>
                    <input type="password" placeholder="Enter Password" name="password" required />
                   {/* <label>
                        <input type="checkbox" checked="checked" name="remember" />
                        Remember me
                    </label>*/}
                    <div className="signin-signup-buttons">
                        <button type="submit" className="signin-button">Вход</button>
                        <div>
                           <Link to="/sign-up">Регистрация</Link>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    }

    async handleSubmit(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        let object = {};
        data.forEach(function(value, key){
            object[key] = value;
        });
        let json = JSON.stringify(object);
        const requestOptions = {
            method: 'POST',
            body: json,
            headers: {'Content-Type': 'application/json', 'Accept' : 'application/json'}
        }
        let response = await fetch(apiUrl + "api/v1/auth/token", requestOptions);
        if (response.status == 201)
        {
            let responseJson = await response.json();
            let userJson = await JSON.parse(responseJson);
            await localStorage.setItem("currentUser", responseJson);
            window.location.replace("/profile");
            //let history = useHistory();
            //there must be redirect
        }
        else
        {
            alert("Something wrong");
        }

    }
}
export default  SignIn;