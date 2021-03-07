import React, {Component} from "react";
import "./ProductCard.css";
import {Link} from "react-router-dom";

class ProductCard extends Component {
    constructor(props) {
        super(props);
        this.addToCartHandler = this.addToCartHandler.bind(this);
    }
    render() {
        //TODO use only 1 prop (obj)
        const linkToBuy = "/products/" + this.props.productId;
        let imageLink  = this.props.imageLink;
        if(imageLink == null)
            imageLink = "./noImage.png"

        return <div className="product-card-container">
            <div className="product-card">
                <Link to={linkToBuy}><img className="product-card-image" src={imageLink} height="200" width="200" alt="image"/></Link>
                <div className="product-card-info">
                    <div className="product-card-name">{this.props.name}</div>
                    <div ><span className="product-card-price">{this.props.price.toFixed(2)}</span> ₴</div>
                </div>
                <div className="product-button-container">
                    <button className="product-button-container" onClick={this.addToCartHandler}>
                        <div className="product-button">Добавить в корзину</div>
                    </button>
                </div>
            </div>
        </div>
    }

    addToCartHandler(event)
    {
        let shoppingCartItemsString = localStorage.getItem("shoppingCartItems");
        if (shoppingCartItemsString == null)
        {
            localStorage.setItem("shoppingCartItems", "[]")
        }
        shoppingCartItemsString = localStorage.getItem("shoppingCartItems");

        let shoppingCartItems = JSON.parse(shoppingCartItemsString);
        let a = this.props.product;
        let ind = shoppingCartItems.findIndex((el) => (el.id == a.id));
        console.log(ind);
        if (ind!=-1)
        {
            shoppingCartItems[ind].count ++;
        }
        else
            shoppingCartItems.push({id: a.id, count: 1});

        shoppingCartItemsString = JSON.stringify(shoppingCartItems);
        localStorage.setItem("shoppingCartItems", shoppingCartItemsString);
    }
}

export default ProductCard;