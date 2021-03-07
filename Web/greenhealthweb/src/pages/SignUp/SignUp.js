import React, { Component } from "react";
import "./Signup.css"
import { Link } from "react-router-dom";
import { apiUrl } from "../../conf";

class SignUp extends Component {
    render() {
        return <div className="signup-container wrapper">
            <div className="signup">
                <form className="signup-form" onSubmit={this.signUpHandler}>
                    <h1>Регистрация</h1>

                    <label htmlFor="fullName"><b>ФИО</b></label>
                    <input type="text" placeholder="Петр Владимирович Бубкин" name="fullName" maxlength="100"/>

                    <label htmlFor="phone"><b>Телефон</b></label>
                    <input type="text" placeholder="+380001234567" name="phone" required maxlength="15"/>

                    <label htmlFor="login"><b>Логин</b></label>
                    <input type="text" placeholder="Введите ваш логин" name="login" required maxlength="15"/>

                    <label htmlFor="email"><b>Email</b></label>
                    <input type="text" placeholder="петрбубкин@gmail.com" name="email" maxlength="100"/>

                    <label htmlFor="password"><b>Пароль</b></label>
                    <input type="password" placeholder="**********" name="password" required maxlength="100"/>

                   {/* <label htmlFor="rpsw"><b>Password check</b></label>
                    <input type="password" placeholder="Enter your password again" name="rpsw" required/>*/}
                    <label htmlFor="city"><b>Город</b></label>
                    <input type="text" placeholder="Петрокровск" name="city" required maxlength="50"/>

                    <label htmlFor="address"><b>Адресс</b></label>
                    <input type="text" placeholder="ул. Павлова, дом 4-Б, кв. 88 " name="address" required maxlength="100"/>

                    <button type="submit" className="signup-button">Зарегистрироваться</button>

                </form>
            </div>
        </div>
    }

    async signUpHandler(event) {
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
        let response = await fetch(apiUrl + "api/v1/customer/add", requestOptions);
        if (response.status == 200)
        {
            alert("Registered");
        }
        else
        {
            alert("Phone, email or userName is reserved");
        }

    }
}

export default SignUp;