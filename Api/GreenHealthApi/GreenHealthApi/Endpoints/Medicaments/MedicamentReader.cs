using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using GreenHealthApi.Endpoints.Medicaments.DTO;
using GreenHealthApi.Exceptions;

namespace GreenHealthApi.Endpoints.Medicaments
{
    public interface IMedicamentReader
    {
        Task<MedicamentFullResponseModel> GetMedicamentAsync(int medicamentId);
        Task<IEnumerable<MedicamentFullResponseModel>> GetAllMedicamentsAsync();
        Task<IEnumerable<MedicamentFullResponseModel>> AllMedicamentsAsync();
    }

    public class MedicamentReader : IMedicamentReader
    {
        private readonly IConnectionStringGetter _conStr;
        private readonly IStaticContentRootUrlGetter _contentRoot;


        public MedicamentReader(IConnectionStringGetter conStr, IStaticContentRootUrlGetter staticContentRootUrlGetter)
        {
            _conStr = conStr;
            _contentRoot = staticContentRootUrlGetter;

        }

        public async Task<MedicamentFullResponseModel> GetMedicamentAsync(int medicamentId)
        {

            await using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var getMedicamentQuery =
                        @"USE GreenHealth;
                          SELECT Medicaments.Id, Medicaments.Name, Medicaments.Price * 
                                 Medicaments.PriceMultiplier AS Price, Medicaments.ManufacturerId, 
                                 Medicaments.IsPrescriptionNeeded, Medicaments.DosageFormId, Medicaments.CategoryId, c.Name AS Category,
                                 Medicaments.Specification, Medicaments.Amount, Medicaments.Unit,
                                 Manufacturers.Name AS ManufacturerName, Manufacturers.Phone AS ManufacturerPhone,
                                 DosageForms.Name AS DosageForm, Medicaments.ImageName as ImageUrl
                          FROM Medicaments LEFT JOIN Manufacturers 
                          ON Medicaments.ManufacturerId = Manufacturers.Id LEFT JOIN DosageForms ON Medicaments.DosageFormId = DosageForms.Id JOIN Categories c ON c.Id = Medicaments.CategoryId WHERE Medicaments.Id = @Id;";
                    var medicamentEntity =
                        await connection.QueryFirstOrDefaultAsync<MedicamentFullResponseModel>(getMedicamentQuery,
                            new {Id = medicamentId});

                    var imageN = medicamentEntity.ImageUrl ?? "noImage.png";
                    medicamentEntity.ImageUrl = _contentRoot.Get() + @"/" + imageN;

                    return medicamentEntity;
                }
                catch (Exception e)
                {
                    throw new SqlQueryException("Something wrong in medicament reader GETMET method");
                }
            }
        }


    
        public async Task<IEnumerable<MedicamentFullResponseModel>> GetAllMedicamentsAsync()
        {    
            try
            {
                await using (var connection = new SqlConnection(_conStr.Get()))
                {
                    await connection.OpenAsync();
                    var getAllMedicamentQuery =
                        @"USE GreenHealth;
                          SELECT Medicaments.Id, Medicaments.Name, Medicaments.Price * 
                                 Medicaments.PriceMultiplier AS Price, Medicaments.ManufacturerId, 
                                 Medicaments.IsPrescriptionNeeded, Medicaments.DosageFormId, Medicaments.CategoryId, c.Name AS Category,
                                 Medicaments.Specification, Medicaments.Amount, Medicaments.Unit,
                                 Manufacturers.Name AS ManufacturerName, Manufacturers.Phone AS ManufacturerPhone,
                                 DosageForms.Name AS DosageForm, Medicaments.ImageName as ImageUrl
                          FROM Medicaments LEFT JOIN Manufacturers 
                          ON Medicaments.ManufacturerId = Manufacturers.Id LEFT JOIN DosageForms ON Medicaments.DosageFormId = DosageForms.Id JOIN Categories c ON c.Id = Medicaments.CategoryId WHERE Medicaments.isHide = 0;";
                    var medicamentEntities =
                        await connection.QueryAsync<MedicamentFullResponseModel>(getAllMedicamentQuery);
                    foreach (var medicament in medicamentEntities)
                    {
                        var imageN = medicament.ImageUrl ?? "noImage.png";
                        medicament.ImageUrl = _contentRoot.Get()  + @"/" + imageN;
                    }
                    return medicamentEntities;
                    
                }
            }
            catch (Exception e)
            {
                throw new SqlQueryException("Something wrong in medicament reader GETALLMET method");
            }
        }
        
        public async Task<IEnumerable<MedicamentFullResponseModel>> AllMedicamentsAsync()
        {    
            try
            {
                await using (var connection = new SqlConnection(_conStr.Get()))
                {
                    await connection.OpenAsync();
                    var getAllMedicamentQuery =
                        @"USE GreenHealth;
                          SELECT Medicaments.Id, Medicaments.Name, Medicaments.Price * 
                                 Medicaments.PriceMultiplier AS Price, Medicaments.ManufacturerId, 
                                 Medicaments.IsPrescriptionNeeded, Medicaments.DosageFormId, Medicaments.CategoryId, c.Name AS Category,
                                 Medicaments.Specification, Medicaments.Amount, Medicaments.Unit,
                                 Manufacturers.Name AS ManufacturerName, Manufacturers.Phone AS ManufacturerPhone,
                                 DosageForms.Name AS DosageForm, Medicaments.ImageName as ImageUrl,
                                 Medicaments.isHide
                          FROM Medicaments LEFT JOIN Manufacturers 
                          ON Medicaments.ManufacturerId = Manufacturers.Id LEFT JOIN DosageForms ON Medicaments.DosageFormId = DosageForms.Id JOIN Categories c ON c.Id = Medicaments.CategoryId;";
                    var medicamentEntities =
                        await connection.QueryAsync<MedicamentFullResponseModel>(getAllMedicamentQuery);
                    foreach (var medicament in medicamentEntities)
                    {
                        var imageN = medicament.ImageUrl ?? "noImage.png";
                        medicament.ImageUrl = _contentRoot.Get()  + @"/" + imageN;
                    }
                    return medicamentEntities;
                    
                }
            }
            catch (Exception e)
            {
                throw new SqlQueryException("Something wrong in medicament reader GETALLMET method");
            }
        }
    }
}