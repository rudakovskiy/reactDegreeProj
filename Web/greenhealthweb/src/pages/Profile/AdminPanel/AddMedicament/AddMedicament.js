import React, { Component } from "react";
import "./AddMedicament.css";
import { apiUrl } from "../../../../conf";
class AddMedicament extends Component {
    render() {
        return <div className="addMedicament-container wrapper">
            <div className="addMedicament">
            <form className="addMedicament-form" onSubmit={this.addMedicamentHandler}>
                <h1>Добавление нового медикамента</h1>
                <label htmlFor="image"><b>Картинка</b></label>
                <input type="file" placeholder="Вставте картинку товара" name="image" required/>

                <label htmlFor="name"><b>Название</b></label>
                <input type="text" placeholder="Введите название продукта" name="name" required maxlength="50"/>

                <label htmlFor="price"><b>Цена закупки</b></label>
                <input type="text" placeholder="Введите цену закупки" name="price" required />

                <label htmlFor="priceMultiplier"><b>Коофициент наценки</b></label>
                <input type="text" placeholder="Введите ко-оф наценки" name="priceMultiplier" required/>

                <label htmlFor="manufacturerName"><b>Производитель</b></label>
                <input type="text" placeholder="Введите производителя" name="manufacturerName" required maxlength="100"/>

                <label htmlFor="dosageForm"><b>Медикаментозная форма</b></label>
                <input type="text" placeholder="Введите медикаментозную форму" name="dosageForm" maxlength="100" required/>

                <label htmlFor="category"><b>Категория</b></label>
                <input type="text" placeholder="Введите категорию товара" name="category" maxlength="100" required/>

                <label htmlFor="specification"><b>Описание</b></label>
                <input type="text" placeholder="Введите описание" name="specification"  required/>

                <label htmlFor="amount"><b>Количество</b></label>
                <input type="text" placeholder="Введите количество" name="amount" required/>

                <label htmlFor="unit"><b>Мера измерения</b></label>
                <input type="text" placeholder="Введите меру измерения" name="unit" maxlength="20"required/>

                <div>
                    <label className="addMedicament-form-label"htmlFor="isPrescriptionNeeded"><b>Выдача по рецепту</b></label>
                    <input className="addMedicament-form-cb" type="checkbox" name="isPrescriptionNeeded"/>
                </div>

                <button type="submit" className="addMedicament-button">Создать</button>

            </form>
        </div>
        </div>
    }
    async addMedicamentHandler(event)
    {
        //сперва зарегать медикамент
        event.preventDefault();
        const data = new FormData(event.target);
        let object = {};
        data.forEach(function(value, key){
            object[key] = value;
        });
        let image = object.image;
        delete object.image;
        object.price = Number(object.price);
        object.priceMultiplier = Number(object.priceMultiplier);
        object.amount = Number(object.amount);

        console.log(object);
        let curUser = JSON.parse(localStorage.getItem("currentUser"));

        let json = JSON.stringify(object);
        console.log(json);
        const medicamentRequestOptions = {
            method: 'POST',
            body: json,
            headers: {'Authorization': 'Bearer ' + curUser.access_token, 'Content-Type': 'application/json', 'Accept' : 'application/json'}
        }
        let medicamentResponse = await fetch(apiUrl + "api/v1/medicaments/add/", medicamentRequestOptions);
        console.log(medicamentResponse)
        let addedMedicament = await medicamentResponse.json();
        let imageFormData = new FormData();
        await imageFormData.append("image", image)
        const imageRequestOptions = {
            method: 'PUT',
            body: imageFormData,
        }
        let imageResponse = await fetch(`${apiUrl}api/v1/images/updateMedicamentImage/${addedMedicament.name}`, imageRequestOptions);
        if (medicamentResponse.status != 200 && imageResponse.status != 200)
            alert("Something wrong");
        else {
            //document.location.replace("/");
            alert("Medicament added");
        }
    }
}
export default AddMedicament;