import React, { Component } from "react";
import { apiUrl } from "../../conf";


class UserOrders extends Component{
    constructor() {
        super();
        this.state = {
            orders: [],
        }
    }
    render() {
        if(this.state.orders.length == 0)
            return <div>У вас еще нету заказов</div>//проверка на нул
        return <div className="userOrders-container wrapper">
            <div>
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
                    {this.state.orders?.map((el, index) =>{
                        return <tbody className="confirmOrders-table-element">
                        <tr>
                            <th>Номер заказа</th>
                            <th>Телефон</th>
                            <th>ФИО заказчика</th>
                            <th>Адресс заказа</th>
                            <th>Дата заказа</th>
                            <th>Статус</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td className = "confirmOrders-orderDetails">{el.id}</td>
                            <td className = "confirmOrders-orderDetails">{el.phone}</td>
                            <td className = "confirmOrders-orderDetails">{el.fullName}</td>
                            <td className = "confirmOrders-orderDetails">{el.city + ", " + el.address}</td>
                            <td className = "confirmOrders-orderDetails">{el.date}</td>
                            <td className="confirmOrders-buttonsBlock">
                                <UserOrderButton isConfirmed={el.isConfirmed} id={el.id}/>

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
                            el.orderElements.map(elem => {
                                return <tr>
                                    <td></td>
                                    <td>
                                        {elem.medicamentName}
                                    </td>
                                    <td>
                                        {elem.id}
                                    </td>
                                    <td>{elem.price.toFixed(2)}</td>
                                    <td>{elem.count}</td>
                                    <td>{(elem.count * elem.price).toFixed(2)}</td>
                                </tr>
                            })}
                        </tbody>})}
                </table>
            </div>
        </div>
    }

    async componentDidMount() {
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));
        const url = apiUrl + 'api/v1/orders/get-my';
        const options = {
            method: "GET",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        const response = await fetch(url, options);
        const data = await response.json();
        this.setState({orders : data})
    }
}
const UserOrderButton = (props) => {
    if (props.isConfirmed == false)
        return <button onClick={cancelUserOrderHandler.bind(this, props.id)} className="confirmOrders-button">Отменить</button>
    else return <span className={"confirmOrders-orderDetails"}>Подтвержден</span>
}

async function cancelUserOrderHandler(id) {
    let curUser = await JSON.parse(localStorage.getItem("currentUser"));

    let requestOptions = {
        method : "POST",
        headers: {
            'Authorization': 'Bearer ' + curUser.access_token,
        }
    }
    await fetch(apiUrl + "api/v1/orders/del/" + id, requestOptions);
    window.location.reload();
}

export default UserOrders;