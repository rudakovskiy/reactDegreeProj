import React, { Component } from "react";
import { apiUrl } from "../../conf";
import "./HideMedicament.css";

class HideMedicaments extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items : [],
        }
    }
    render() {
        return <div className="hideMedicaments-container wrapper">
            <div className="hideMedicaments">
                <table>
                    <tr>
                        <th>Изображение</th>
                        <th>Название</th>
                        <th>Код товара</th>
                        <th>Изменить</th>
                        <th>Скрыт</th>
                        <th>Цена</th>
                    </tr>
                    {this.state.items.map((product, index) => {
                        let imageLink  = product.imageUrl;
                        if(imageLink == null)
                            imageLink = "./noImage.png"
                        return <tr className="shoppingCartItem-container">
                            <th><img src={imageLink} alt="" height="70" width="70"/></th>
                            <th><span>{product.name}</span></th>
                            <th><span>{product.id}</span></th>
                            <th><span>
                                <button className="shoppingCart-button-clear" onClick={this.dItemHandler.bind(this, index)}>Скрыть</button>
                                <button className="shoppingCart-button-clear" onClick={this.uItemHandler.bind(this, index)}>Показать</button>
                            </span>
                            </th>
                            <th><span>{product.isHide == false ? "-" : "+"}</span></th>
                            <th><span>{product.price} ₴</span></th>
                        </tr>
                    })}
                </table>
            </div>
        </div>
    }

    async componentDidMount() {
            const url = apiUrl + 'api/v1/medicaments/all';
            const response = await fetch(url);
            const data = await response.json();

            this.setState({items : data});
        }

    dItemHandler(index) {
        let shoppingCartItems = this.state.items;
        let idElToDel = shoppingCartItems[index].id;
        let curUser = JSON.parse(localStorage.getItem("currentUser"));
        let url = apiUrl + 'api/v1/medicaments/hide' + '/' + idElToDel + "?isHide=" + true;
        const object = {isHided: true};
        let json = JSON.stringify(object);

        const medicamentRequestOptions = {
            method: 'PUT',
            body: json,
            headers: {'Content-Type': 'application/json', 'Authorization': 'Bearer ' + curUser.access_token, 'Accept' : 'application/json'}
        }
        let medicamentResponse =  fetch(url , medicamentRequestOptions);

         url = apiUrl + 'api/v1/medicaments/all';
        const response = fetch(url).then(r => r.json()).then(data => {this.setState({items : data});
        });
        fetch(url).then(r => r.json()).then(data => {this.setState({items : data});
        });
    }

    uItemHandler(index) {
        let shoppingCartItems = this.state.items;
        let curUser = JSON.parse(localStorage.getItem("currentUser"));
        let idElToDel = shoppingCartItems[index].id;
        let url = apiUrl + 'api/v1/medicaments/hide' + '/' + idElToDel + "?isHide=" + false;
        const object = {isHided: false};
        let json = JSON.stringify(object);

        const medicamentRequestOptions = {
            method: 'PUT',
            body: json,
            headers: {'Content-Type': 'application/json', 'Authorization': 'Bearer ' + curUser.access_token, 'Accept' : 'application/json'}
        }
        let medicamentResponse =  fetch(url , medicamentRequestOptions);

         url = apiUrl + 'api/v1/medicaments/all';
        const response = fetch(url).then(r => r.json()).then(data => {this.setState({items : data});
        });

        fetch(url).then(r => r.json()).then(data => {this.setState({items : data});
        });
    }
}
export default HideMedicaments;