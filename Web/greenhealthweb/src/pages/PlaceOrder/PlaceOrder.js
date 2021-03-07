import React, {Component} from "react";
import { apiUrl, webUrl } from "../../conf";
import "./PlaceOrder.css"

class PlaceOrder extends Component {
    constructor(props) {
        super(props);
        this.state = {
            products : []
        }
        this.PlaceOrderHandler = this.PlaceOrderHandler.bind(this);
    }
    render() {
        let curUser = localStorage.getItem("currentUser")
        curUser = JSON.parse(curUser);

        if (this.state.products == null)
            return <div><h2>Добавте в корзину товары, что хотите купить</h2></div>
        else if (curUser == null)
            return <div className="placeOrder-container wrapper">
                <div className="placeOrder">
                <form className="placeOrder-form" onSubmit={this.PlaceOrderHandler}>
                    <h1>Оформить заказ</h1>

                    <label htmlFor="fullName"><b>ФИО</b></label>
                    <input type="text" placeholder="Петр Владимирович Бубкин" name="fullName" required/>

                    <label htmlFor="phone"><b>Телефон</b></label>
                    <input type="text" placeholder="+380001234567" name="phone" required />


                    <label htmlFor="city"><b>Город</b></label>
                    <input type="text" placeholder="Петрокровск" name="city" required/>

                    <label htmlFor="address"><b>Адресс</b></label>
                    <input type="text" placeholder="ул. Павлова, дом 4-Б, кв. 88 " name="address" required/>

                    <button type="submit" className="placeOrder-button">Оформить заказ</button>

                </form>
                </div>
            </div>
            else {


            return <div className="placeOrder-container wrapper">
                <div className="placeOrder">
                    <form className="placeOrder-form" onSubmit={this.PlaceOrderHandler}>
                        <h1>Оформить заказ</h1>

                        <label htmlFor="fullName"><b>ФИО</b></label>
                        <input type="text" placeholder="Петр Владимирович Бубкин" value={curUser.fullName} name="fullName" readOnly/>

                        <label htmlFor="phone"><b>Телефон</b></label>
                        <input type="text" placeholder="+380001234567" pattern="[\+]\d{1}\s[\(]\d{3}[\)]\s\d{3}[\-]\d{2}[\-]\d{2}" value={curUser.phone} name="phone" readOnly/>


                        <label htmlFor="city"><b>Город</b></label>
                        <input id="placeOrder-form-city" type="text" placeholder="Петрокровск" name="city" required/>

                        <label htmlFor="address"><b>Адресс</b></label>
                        <input id="placeOrder-form-address" type="text" placeholder="ул. Павлова, дом 4-Б, кв. 88 "  name="address" required/>

                        <button type="submit" className="placeOrder-button">Оформить заказ</button>

                    </form>
                </div>
            </div>}

    }

    componentDidMount() {
        let shoppingCartItemsString = localStorage.getItem("shoppingCartItems")
        let shoppingCartItems = JSON.parse(shoppingCartItemsString);
        this.setState({products: shoppingCartItems});

        let curUser = localStorage.getItem("currentUser")
        curUser = JSON.parse(curUser);


        let a = document.getElementById("placeOrder-form-address");
        if (a!=null)
            a.defaultValue = curUser.address;
        a = document.getElementById("placeOrder-form-city");
        if(a!=null)
            a.defaultValue = curUser.city;
    }

    async PlaceOrderHandler(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        let object = {};
        data.forEach(function(value, key){
            object[key] = value;
        });
        object["orderElements"] = this.state.products;
        object["isConfirmed"] = false;
        let json = JSON.stringify(object);
        const requestOptions = {
            method: 'POST',
            body: json,
            headers: {'Content-Type': 'application/json', 'Accept' : 'application/json'}
        }
        let response = await fetch(apiUrl + "api/v1/orders/place-order", requestOptions);
        localStorage.setItem("shoppingCartItems", "[]");
        window.location.replace(webUrl + "profile/my-orders");
    }
}


export default PlaceOrder;