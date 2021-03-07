import React, { Component } from 'react';
import ProductCard from "../../components/ProductCard/ProductCard";
import "./ProductList.css";
import {apiUrl} from "../../conf";

class ProductList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            products : [],
            sortBy : "alphabetU",
            categories : [],
            sortByCategory: -1,
        }
    }

    render() {
        if(this.state.products.length) {
            let products = this.state.products;
            let sortByCat = this.state.sortByCategory;
            if(sortByCat != -1)
                products = products.filter(el => el.categoryId == sortByCat);
            if(this.state.sortBy == "alphabetU")
                products.sort((a, b) => {
                    if (a.name > b.name)
                        return 1
                    else return -1
                })
            else if(this.state.sortBy == "alphabetD")
                products.sort((a, b) => {
                    if (a.name < b.name)
                        return 1
                    else return -1
                })
            else if(this.state.sortBy == "priceU")
                products.sort((a, b) => {
                    if (a.price > b.price)
                        return 1
                    else return -1
                })
            else if(this.state.sortBy == "priceD")
                products.sort((a, b) => {
                    if (a.price < b.price)
                        return 1
                    else return -1
                })
            return <div className="wrapper">
                    <div className="catalog-menu-container">
                        <div className="catalog-menu">
                            <select className="catalog-menu-drpdwn" id="catalog-sortBy" name="" onChange={this.sortByHandler.bind(this)}>
                                <option value="alphabetU">Название по возрастанию</option>
                                <option value="alphabetD">Название по убыванию</option>
                                <option value="priceU">Цена по возрастанию</option>
                                <option value="priceD">Цена по убыванию</option>
                            </select>
                            <select className="catalog-menu-drpdwn" id="catalog-category" name="" onChange={this.categoryHandler.bind(this)}>
                                <option value={-1}>Все категории</option>
                                {this.state.categories.map(el => {
                                    return <option value={el.id}>{el.name}</option>
                                } )}
                            </select>
                        </div>
                    </div>
                <div className="catalog">
                    {products.map(p => {
                        let imageUrl = p.imageUrl;

                        return <ProductCard product={p} imageLink={imageUrl} name={p.name} price={p.price} productId={p.id}/>
                    })}
                </div>
            </div>
        }
        else return <span className="wrapper">No Products(</span>
    }

    async componentDidMount() {
        let url = apiUrl + 'api/v1/medicaments';
        const response = await fetch(url);
        const data = await response.json();
        url = apiUrl + 'api/v1/orders/get-all-categories';
        let dataCateagoties = await fetch(url);
        dataCateagoties = await dataCateagoties.json();
        this.setState({products : data, categories: dataCateagoties});
    }

    sortByHandler() {
        let sort = document.getElementById("catalog-sortBy");
        let sortValue = sort.options[sort.selectedIndex].value;
        this.setState({sortBy: sortValue})
    }
    categoryHandler() {
        let sort = document.getElementById("catalog-category");
        let sortValue = sort.options[sort.selectedIndex].value;
        console.log(sortValue);
        this.setState({sortByCategory: sortValue})
    }
}


export default ProductList;