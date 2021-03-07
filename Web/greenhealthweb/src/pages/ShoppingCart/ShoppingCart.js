import React, {Component} from "react";
import "./ShoppingCart.css";
import { apiUrl } from "../../conf";
import {Link} from "react-router-dom";
class ShoppingCart extends Component {
    constructor(props) {
        super(props);
        this.state = {
            shoppingCartItems : [],
            orderItems : []
        }
        /*this.deleteShoppingCartItemHandler = this.deleteShoppingCartItemHandler.bind(this);*/

    }
    render() {
        if (this.state.shoppingCartItems.length == 0)
            return <div className="shoppingCart-container wrapper">
            <h2>Ваша корзина пуста</h2>
        </div>
        else
        return <div className="shoppingCart-container wrapper">
            <div className="shoppingCart">
                <table>
                    <tr>
                        <th>Изображение</th>
                        <th>Название</th>
                        <th>Код товара</th>
                        <th>Количество</th>
                        <th>Цена</th>
                        <th align="left">
                            <button className="shoppingCart-button-clear" onClick={this.clearCart.bind(this)}>
                                <div className="">Очистить</div>
                            </button>
                        </th>
                    </tr>
                {this.state.shoppingCartItems.map((product, index) => {
                    let imageLink  = product.imageUrl;
                    if(imageLink == null)
                        imageLink = "./noImage.png"
                    return <tr className="shoppingCartItem-container">
                        <th><img src={imageLink} alt="" height="70" width="70"/></th>
                        <th><span>{product.name}</span></th>
                        <th><span>{product.id}</span></th>
                        <th><span>
                            Кол-во: {this.state.orderItems[index].count}
                            <button className="shoppingCartItem-DUbutton" onClick={this.dShoppingCartItemHandler.bind(this, index)}>-</button>
                            <button className="shoppingCartItem-DUbutton" onClick={this.uShoppingCartItemHandler.bind(this, index)}>+</button>
                            </span>
                        </th>
                        <th><span>{(product.price * this.state.orderItems[index].count).toFixed(2)} ₴</span></th>
                        <th><button className="shoppingCartItem-deleteButton" onClick={this.deleteShoppingCartItemHandler.bind(this, index)}>Удалить</button></th>
                    </tr>
                })}
                </table>
                <div className="shoppingCart-buttons">
                    <Link  to={"place-order"}><div className="shoppingCart-button">Оформить заказ</div></Link>

                </div>
            </div>
        </div>
    }

    async componentDidMount() {
        let shoppingCartItemsString = localStorage.getItem("shoppingCartItems")
        let shoppingCartItems = JSON.parse(shoppingCartItemsString);
        if (shoppingCartItems == null)
        {
            localStorage.setItem("shoppingCartItems", "[]")
        }
        else
        {
            let shoppingCartItemsId = JSON.parse(shoppingCartItemsString);

            const url = apiUrl + 'api/v1/medicaments';
            const response = await fetch(url);
            const data = await response.json();
            let shoppingCartItems = [];
            shoppingCartItemsId.forEach((val, index) => {
                let a = data.find((el)=> (el.id == val.id));
                if (a != null)
                    shoppingCartItems.push(a);
            });

            this.setState({shoppingCartItems : shoppingCartItems, orderItems: shoppingCartItemsId});
        }
    }

    deleteShoppingCartItemHandler(index) {
        let shoppingCartItems = this.state.shoppingCartItems;
        let orderItems = this.state.orderItems;
        let idElToDel = shoppingCartItems[index].id;
        shoppingCartItems.splice(index, 1)
        let a = orderItems.findIndex((el) => el.id == idElToDel)
        orderItems.splice(a, 1);
        let shoppingCartItemsString = JSON.stringify(orderItems);
        localStorage.setItem("shoppingCartItems", shoppingCartItemsString);
        this.setState({shoppingCartItems: shoppingCartItems, orderItems: orderItems})
    }
    dShoppingCartItemHandler(index) {
        let shoppingCartItems = this.state.shoppingCartItems;
        let orderItems = this.state.orderItems;
        let idElToDel = shoppingCartItems[index].id;
        let a = orderItems.findIndex((el) => el.id == idElToDel);
        if (orderItems[a].count > 1) {
            orderItems[a].count--;
            localStorage.setItem("shoppingCartItems", JSON.stringify(orderItems));
            this.setState({shoppingCartItems: shoppingCartItems, orderItems: orderItems})
        }
    }
    uShoppingCartItemHandler(index) {
        let shoppingCartItems = this.state.shoppingCartItems;
        let orderItems = this.state.orderItems;
        let idElToDel = shoppingCartItems[index].id;
        let a = orderItems.findIndex((el) => el.id == idElToDel);
        if (orderItems[a].count > 0) {
            orderItems[a].count++;
            localStorage.setItem("shoppingCartItems", JSON.stringify(orderItems));
            this.setState({shoppingCartItems: shoppingCartItems, orderItems: orderItems})
        }
    }
    clearCart() {
        localStorage.setItem("shoppingCartItems", "[]");
        this.setState({shoppingCartItems: [], orderItems: []})

    }
}
export default ShoppingCart;