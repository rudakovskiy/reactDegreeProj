import React, {Component} from "react";
import "./Footer.css"
class Footer extends Component {
    render() {
        return <div className="footer-container">
            <div className="footer wrapper">
                <div className="footer-copiright">
                    © 2019 - 2020 GreenHealth
                </div>
                <div>Разработал Рудаковский Д.Р.</div>
            </div>
        </div>
    }
}
export default Footer;