import React, { Component } from 'react';
import { Router, Route, Switch } from 'react-router';
import ProductList from './ProductList/ProductList';
import Product from "./Product/Product";
import SignIn from "./SignIn/SignIn";
import SignUp from "./SignUp/SignUp";
import Profile from "./Profile/Profile";
import ShoppingCart from "./ShoppingCart/ShoppingCart";
import "./Main.css"
import AddMedicament from "./Profile/AdminPanel/AddMedicament/AddMedicament";
import { Redirect } from "react-router-dom";
import PlaceOrder from "./PlaceOrder/PlaceOrder";
import ConfirmOrders from "./ConfirmOrders/ConfirmOrders";
import UserOrders from "./UserOrders/UserOrders";
import EditCategories from "./Profile/AdminPanel/EditCategories/EditCategories"
import ChangePassword from "./Profile/User/ChangePassword/ChangePassword";
import Home from "./Home/Home";
import AboutUs from "./AboutUs/AboutUs";
import HideMedicaments from "./HideMedicaments/HideMedicaments";
import { isElementOfType } from 'react-dom/test-utils';
import AllOrders from './AllOrders/AllOrders';

class Main extends Component {
    render() {
        return <div className="main-container">
            <Switch>
                <Route exact path="/" component={Home}/>
                <Route exact path="/products" component={ProductList}/>
                <Route path="/products/:productId" component={Product}/>
                <Route path="/sign-in" component={SignIn}/>
                <Route path="/sign-up" component={SignUp}/>
                <Route exact path="/profile" component={Profile}/>
                <Route exact path="/profile/add-medicament"
                       render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "admin" ?
                           <Redirect to="/"/> : <AddMedicament/>)}/>
                <Route exact path="/profile/confirm-orders"
                       render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "admin" ?
                           <Redirect to="/"/> : <ConfirmOrders />)}/>
                <Route exact path="/profile/orders"
                       render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "admin" ?
                           <Redirect to="/"/> : <AllOrders />)}/>
                <Route exact path="/shopping-cart" component={ShoppingCart} />
                <Route exact path="/place-order"
                render={() => (JSON.parse(localStorage.getItem("shoppingCartItems")) == [] ?
                <Redirect to="/"/> : <PlaceOrder />)}/>
                <Route exact path="/profile/my-orders"
                       render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "customer"  ?
                           <Redirect to="/"/> : <UserOrders/>)}/>
                <Route exact path="/profile/hide-medicament"
                       render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "admin"  ?
                           <Redirect to="/"/> : <HideMedicaments />)}/>

                <Route exact path="/profile/change-password" render={() => (JSON.parse(localStorage.getItem("currentUser")).role != "customer"  ?
                               <Redirect to="/"/> : <ChangePassword/>)}/>
                               <Route exact path="/about" component={AboutUs} />

            </Switch>
        </div>
    }
}

export default Main;