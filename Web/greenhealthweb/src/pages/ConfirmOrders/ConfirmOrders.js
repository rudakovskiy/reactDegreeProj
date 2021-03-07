import React, { Component } from "react";
import { apiUrl } from "../../conf";
import "./ConfirmOrders.css"

class ConfirmOrders extends Component {

    constructor(props) {
        super(props);

        this.state = {
            orders : []
        }
    }
    render() {
        return <div classname="confirmOrders-container wrapper">
            <div classname="confirmOrders">
                <table className="confirmOrders-table wrapper">
                    {/*<tr>
                        <th>Номер заказа</th>
                        <th>Телефон</th>
                        <th>ФИО заказчика</th>
                        <th>Адресс заказа</th>
                        <th>Дата заказа</th>
                        <th>Подтвердить/Отказать</th>
                        <th></th>
                    </tr>*/}
                    {this.state.orders.map((el, index1) =>{
                        return <tbody className="confirmOrders-table-element">
                        <tr>
                            <th>Номер заказа</th>
                            <th>Телефон</th>
                            <th>ФИО заказчика</th>
                            <th>Адресс заказа</th>
                            <th>Дата заказа</th>
                            <th>Подтвердить/Отказать</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td className = "confirmOrders-orderDetails">{el.id}</td>
                            <td className = "confirmOrders-orderDetails">{el.phone}</td>
                            <td className = "confirmOrders-orderDetails">{el.fullName}</td>
                            <td className = "confirmOrders-orderDetails">{el.city + ", " + el.address}</td>
                            <td className = "confirmOrders-orderDetails">{el.date}</td>
                            <td className="confirmOrders-buttonsBlock">
                                <button onClick={this.confirmOrderButtonHandler.bind(this, el.id)} className="confirmOrders-button">Подтвердить</button>
                                <button onClick={this.deleteOrderButtonHandler.bind(this, el.id)} className="confirmOrders-button">Отказать</button>
                            </td>
                        </tr>
                        <tr>
                            <th className="confirmOrders-subHeading"></th>
                            <th className="confirmOrders-subHeading">Название</th>
                            <th className="confirmOrders-subHeading">Код товара</th>
                            <th className="confirmOrders-subHeading">Цена</th>
                            <th className="confirmOrders-subHeading">Кол-во</th>
                            <th className="confirmOrders-subHeading">Полная цена</th>
                        </tr>
                           {
                               el.orderElements.map((elem, index2) => {
                            return <tr>
                                <td></td>
                                <td>
                                    {elem.medicamentName}
                                </td>
                                <td>
                                    {elem.id}
                                </td>
                                <td>{elem.price}</td>
                                <td><span>{elem.count}</span>
                                <form className="asdfbb" onSubmit={() =>this.formHandler(index1, index2)}>
                                <input id={"dgdfhg"+index1 +"a"+index2} classname="dgdfh"type="text" placeholder="20" name="date" required/>
                                <button type="submit" className="confirmOrders-buttonn">Изменить</button>
                                </form>
                    </td>
                                <td>{elem.count * elem.price}</td>
                            </tr>
                        })}
                    </tbody>})}
                </table>
            </div>
        </div>
    }
    async componentDidMount() {
        const url = apiUrl + 'api/v1/orders/get-unconfirmed/0';
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));

        let requestOptions = {
            method : "GET",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
                'Content-Type': 'application/json'
            }
        }
        const response = await fetch(url);
        const data = await response.json();
        this.setState({orders : data})
    }
    async confirmOrderButtonHandler(index) {
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));

        let requestOptions = {
            method : "POST",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        await fetch(apiUrl + "api/v1/orders/confirm/" + index, requestOptions);
        requestOptions = {
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        const url = apiUrl + 'api/v1/orders/get-unconfirmed';
        const response = await fetch(url, requestOptions);
        const data = await response.json();
        this.setState({orders : data})
    }
    async deleteOrderButtonHandler(index) {
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));

        let requestOptions = {
            method : "POST",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        await fetch(apiUrl + "api/v1/orders/del/" + index, requestOptions);

        const url = apiUrl + 'api/v1/orders/get-unconfirmed';
        const response = await fetch(url);
        const data = await response.json();
        this.setState({orders : data})
    }
    

    async formHandler(i1, i2) {
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));
        let order = this.state.orders[i1];
        let orEl = order.orderElements[i2];
        let input = document.getElementById("dgdfhg"+i1 +"a"+i2).value;
        let requestOptions = {
            method : "POST",
            body: "",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,'Content-Type': 'application/json', 'Accept' : 'application/json'
            }
        }
        await fetch(apiUrl + "api/v1/orders/changeel?" + "or="+ order.id +"&" + "orEl=" + orEl.id +"&" + "count=" + input, requestOptions);

        const url = apiUrl + 'api/v1/orders/get-unconfirmed/1';
        const response = await fetch(url);
        const data = await response.json();
        this.setState({orders : data})
    }
}
export default ConfirmOrders;