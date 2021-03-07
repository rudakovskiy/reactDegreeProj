import React, {Component} from "react";
import {withRouter} from 'react-router'
import "./Product.css"
import {apiUrl} from "../../conf";
import {Link} from "react-router-dom";

class Product extends Component{
    constructor(props) {
        super(props);
        this.state = {
            product : {},
        }
        this.buyOneHandler = this.buyOneHandler.bind(this);
    }
    render() {
        let imageUrl = this.state.product.imageUrl;
        if (imageUrl == null)
            imageUrl = "https://via.placeholder.com/500";

        return <div className="product-page wrapper">
            <div className="product-page-head">
                <img  src={imageUrl} height="500" width="500" alt=""/>
                <div className="product-page-info">
                <h2 className="product-page-info-name">{this.state.product.name}</h2>
                <span className="product-page-info-price">{(this.state.product.price * 1).toFixed(2)} ₴</span>
                    <div className="product-button-container">
                        <Link className="product-button-container" to={"/shopping-cart"} onClick={this.buyOneHandler}><div className="product-button">Купить</div></Link>
                    </div>
                </div>
            </div>
            <div className="product-page-desc">
                <h2>Описание</h2>
                <p>{this.state.product.specification}</p>
            </div>
        </div>

    }

    buyOneHandler()
    {
        let product = this.state.product;
        localStorage.setItem("shoppingCartItems", JSON.stringify([{id:product.id, count: 1}]))

    }
    async componentDidMount() {
        const url = apiUrl + 'api/v1/medicaments/' + this.props.match.params.productId;
        const response = await fetch(url);
        const data = await response.json();
        this.setState({product : data});
    }

}
export default withRouter(Product);
