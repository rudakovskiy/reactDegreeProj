import React, { Component } from "react";
import "./ShoppingCartItem.css";
export class ShoppingCartItem extends Component {
    constructor(props) {
        super(props);
        this.state = {
            products : [],
        }
        this.deleteShoppingCartItemHandler = this.deleteShoppingCartItemHandler.bind(this);
    }
    render() {
        let imageLink  = this.props.product.imageUrl;
        let product = this.props.product;
        if(imageLink == null)
            imageLink = "./noImage.png"
        return <div className="shoppingCartItem-container">
            <div className="shoppingCartItem">
                <img src={imageLink} alt="" height="70" width="70"/>
                <span>{product.name}</span>
                <span>Код товара: {product.id}</span>
                <span>Кол:во: {this.props.count}</span>
                <span>{product.price} ₴</span>
                <button className="shoppingCartItem-deleteButton" onClick={this.deleteShoppingCartItemHandler}>Удалить</button>
            </div>
        </div>
    }

    deleteShoppingCartItemHandler() {
        let shoppingCartItemsString = localStorage.getItem("shoppingCartItems");
        let shoppingCartItems = JSON.parse(shoppingCartItemsString)
        let a = this.props.product;
        const index = shoppingCartItems.indexOf(this.props.product);
        console.log(index);
        shoppingCartItems.splice(index);
        shoppingCartItemsString = JSON.stringify(shoppingCartItems);
        localStorage.setItem("shoppingCartItems", shoppingCartItemsString);
        window.location.reload();
    }
}
export default ShoppingCartItem;