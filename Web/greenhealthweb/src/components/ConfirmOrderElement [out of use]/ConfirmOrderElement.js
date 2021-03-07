import React, { Component } from "react";

class ConfirmOrderElement extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        let el = this.props.confirmOrderElement;
        return <div className="confirmOrder-container">
            <div className="confirmOrder">
                <table>
                    <tr>
                        <th>Код</th>
                        <th>ФИО заказчика</th>
                        <th>Адресс заказа</th>
                        <th>Дата заказа</th>
                    </tr>
                    <tr>
                        <td>{el.id}</td>
                        <td>{el.fullName}</td>
                        <td>{el.city + ", " + el.address}</td>
                        <td>{el.date}</td>
                    </tr>
                    {el.orderElements.map(e=>{
                        return <tr>
                            <th></th>
                            <th>{e.medicamenntName}</th>
                            <th></th>
                            <th></th>

                        </tr>
                    })}
                </table>
            </div>
        </div>
    }
}
export default ConfirmOrderElement;