import  React, { Component } from 'react';
import "./Home.css";

class Home extends Component {
    render() {
        return <div className="home-container wrapper">
            <div className="home">
            <img src={require("./_73523-3040.jpg")} height={300}/>
            <div className="home-aboutUs" >
                <h2>Только у нас</h2>
                <p>Только у нас вы можете преобрести натураьные лекарственные препараты по самым низким ценам и с самой быстрой доставкой.</p>
            </div>
            </div>
        </div>
    }
}

export default Home;