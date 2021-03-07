import React, { Component } from "react";
import { apiUrl } from "../../../../conf";

class EditCategories extends Component {
    constructor() {
        super();
        this.state = {
            categories: []
        }
    }
    render() {
        return <div className={"wrapper"}>
            <span>При удалении категори, все товары находящиеся в ней будут удалены!</span>
            <table className="confirmOrders-table wrapper">
                <tr>
                    <th>Номер</th>
                    <th>Категория</th>
                    <th>Удалить</th>
                </tr>
                {this.state.categories?.map((el, index) =>{
                    return <tbody className="confirmOrders-table-element">
                    <tr>
                        <td className = "confirmOrders-orderDetails">{el.id}</td>
                        <td className = "confirmOrders-orderDetails">{el.name}</td>
                        <td>
                            <button className={"confirmOrders-button"} onClick={this.delCategoryHandler.bind(this, el.id)}>Удалить</button>
                        </td>
                    </tr>
                    </tbody>})}
            </table>
        </div>
    }

    async componentDidMount() {
        let curUser = await JSON.parse(localStorage.getItem("currentUser"));
        const url = apiUrl + 'api/v1/orders/get-all-categories';
        const options = {
            method: "GET",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        let cat = await fetch(url, options);
        cat = await cat.json();
        this.setState({categories : cat})
    }

    delCategoryHandler(index) {
        let curUser = JSON.parse(localStorage.getItem("currentUser"));
        const url = apiUrl + 'api/v1/orders/del-category/' + index;
        const options = {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + curUser.access_token,
            }
        }
        let cat = fetch(url, options);
    }

}

export default EditCategories