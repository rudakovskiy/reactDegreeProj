import  React, { Component } from 'react';
import './Header.css'
import { Link } from 'react-router-dom';

class Header extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isAuthorized : false, //when user is loged in = true
        }
    }
    render() {
        let profileUrl = "/profile";

        return <div className="header-container">
            <div className="header wrapper">
                <div className="header-logo">
                    <Link to="/">GreenHealth</Link>
                </div>
                <ul className="header-nav">
                    <li className="header-nav-element"><Link to="/products">Каталог товаров</Link></li>
                    <li className="header-nav-element"><Link to="/about">О нас</Link></li>
                </ul>
                <ul className="header-nav">
                    <li className="header-nav-ico"><Link to={profileUrl}><img src={require("./account.png")} height="30"/></Link></li>
                    <li id="cartButton" className="header-nav-ico"><Link to="/shopping-cart"><img src={require("./supermarket.png")} height="30"/></Link></li>
                    <img src="" alt=""/>
                </ul>
            </div>
        </div>
    }

    componentDidMount() {
        if (JSON.parse(localStorage.getItem("currentUser"))?.role == "admin")
        {
            let a = window.document.getElementById("cartButton");
            a.style.visibility = 'hidden';
        }

    }
}
export default Header;