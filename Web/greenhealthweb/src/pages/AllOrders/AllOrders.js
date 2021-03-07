import React, { Component } from "react";
import { apiUrl } from "../../conf";
import "./ConfirmOrders.css"

class AllOrders extends Component {

    constructor(props) {
        super(props);
        this.state = {
            orders : [],
            sortedOrders: [],
            sortString: ""
        }
    }
    render() {
        console.log(this.state.sortedOrders.length);
        
        if(this.state.sortedOrders.length < 1)
        {
            return <div classname="wrapper">
                <div className="formfind">
                    <form  onSubmit={this.FindOrders.bind(this)}>                    
                    <input id="myFindInput"type="text" placeholder="2020-12-31" name="date"/>
                    <button type="submit" className="shoppingCartItem-deleteButton">Поиск</button>
                    </form>

                </div>
                Нету  заказов</div>
        }
        else
            return <div classname="confirmOrders-container wrapper">
            <div classname="confirmOrders">
                <div className="formfind">
                    <form  onSubmit={this.FindOrders.bind(this)}>                    
                    <input id="myFindInput"type="text" placeholder="2020-12-31" name="date" required/>
                    <button type="submit" className="shoppingCartItem-deleteButton">Поиск</button>
                    </form>

                </div>
                <table className="confirmOrders-table wrapper">
                
                    
                    {
                        this.state.sortedOrders.map((el, index) =>{
                        return <tbody className="confirmOrders-table-element">
                        <tr>
                            <th>Номер заказа</th>
                            <th>Телефон</th>
                            <th>ФИО заказчика</th>
                            <th>Адресс заказа</th>
                            <th>Дата заказа</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td className = "confirmOrders-orderDetails">{el.id}</td>
                            <td className = "confirmOrders-orderDetails">{el.phone}</td>
                            <td className = "confirmOrders-orderDetails">{el.fullName}</td>
                            <td className = "confirmOrders-orderDetails">{el.city + ", " + el.address}</td>
                            <td className = "confirmOrders-orderDetails">{el.date}</td>
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
                                <td>{elem.price}</td>
                                <td>{elem.count}</td>
                                <td>{elem.count * elem.price}</td>
                            </tr>
                        })}
                    </tbody>})}
                </table>
            </div>
        </div>
    }
    async componentDidMount() {
        const url = apiUrl + 'api/v1/orders/get-unconfirmed/1';
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
        this.setState({orders : data, sortedOrders: data, sortString: ""    })
    }
    FindOrders(e) {
        e.preventDefault();
        console.log(document.getElementById("myFindInput").value);
        console.log(this.state.orders);
        console.log(this.state.sortedOrders);
        let sort = this.state.orders.filter(el => (el.date.includes(document.getElementById("myFindInput").value.toString())));
        console.log(sort);
        let sorted = [];
        sort.forEach(element => {
            sorted.push(element)
        });
        this.setState({sortedOrders: sorted, orders:this.state.orders})
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
        this.setState({orders : data, sortedOrders: data})
    }
}
export default AllOrders;