using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using GreenHealthApi.Endpoints.Medicaments.DTO;

namespace GreenHealthApi.Endpoints.Medicaments
{
    public interface IMedicamentWriter
    {
        Task<MedicamentEntity> AddMedicamentAsync(MedicamentAddModel medicamentModel);
        Task ChangeHideVal(bool isHide, int id);
    }

    public class MedicamentWriter : IMedicamentWriter
    {
        private readonly IConnectionStringGetter _conStr;

        public MedicamentWriter(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }

        public async Task<MedicamentEntity> AddMedicamentAsync(MedicamentAddModel medicamentModel)
        {
            await using (var connection = new SqlConnection(_conStr.Get()))
            {
                await connection.OpenAsync();
                //TODO fix sql query and try catch
                //TODO return added medicament id
                //find manufacturer, find dosageform
                var manufacturerQuery = @"USE GreenHealth; SELECT Id FROM Manufacturers WHERE  Name = @name;";
                var manufacturerId = connection.QueryFirstOrDefault<int>(manufacturerQuery, new{name = medicamentModel.ManufacturerName});
                if (manufacturerId == 0)//not shure
                {
                    var addManufacturerQuery =
                        @"USE GreenHealth; INSERT INTO Manufacturers (Name) VALUES (@name); SELECT SCOPE_IDENTITY() FROM Manufacturers;";
                    manufacturerId = connection.QueryFirstOrDefault<int>(addManufacturerQuery,
                        new {name = medicamentModel.ManufacturerName}); //добавить такого
                }
                
                var dosageFormQuery = @"USE GreenHealth; SELECT Id FROM DosageForms WHERE  Name = @name;";
                var dosageFormId = connection.QueryFirstOrDefault<int>(dosageFormQuery, new{name = medicamentModel.DosageForm});
                if (dosageFormId == 0)//not shure
                {
                    var addDosageForm =
                        @"USE GreenHealth; INSERT INTO DosageForms (Name) VALUES (@name); SELECT SCOPE_IDENTITY() FROM DosageForms";
                    dosageFormId = connection.QueryFirstOrDefault<int>(addDosageForm,
                        new {name = medicamentModel.DosageForm}); //добавить такого
                }
                
                var categoryQuery = @"USE GreenHealth; SELECT Id FROM Categories WHERE  Name = @name;";
                var categoryId = connection.QueryFirstOrDefault<int>(categoryQuery, new{name = medicamentModel.Category});
                if (categoryId == 0)//not shure
                {
                    var addCategoryQuery =
                        @"USE GreenHealth; INSERT INTO Categories (Name) VALUES (@name); SELECT SCOPE_IDENTITY() FROM Categories;";
                    categoryId = connection.QueryFirstOrDefault<int>(addCategoryQuery,
                        new {name = medicamentModel.Category}); //добавить такого
                }
                
                var addMedicamentQuery = 
                    @"USE GreenHealth; INSERT INTO Medicaments (Name, Price, PriceMultiplier, ManufacturerId, 
                                                           IsPrescriptionNeeded, DosageFormId, Specification, Amount, Unit, CategoryId) 
                          VALUES (@Name, @Price, @PriceMultiplier, @ManufacturerId, @IsPrescriptionNeeded, @DosageFormId, 
                                  @Specification, @Amount, @Unit, @CategoryId);
                          SELECT SCOPE_IDENTITY() FROM Customers;";
                var medicamentId = await connection.QueryFirstOrDefaultAsync<int>(addMedicamentQuery, new
                {
                    Name = medicamentModel.Name,
                    Price = medicamentModel.Price,
                    PriceMultiplier = medicamentModel.PriceMultiplier, 
                    ManufacturerId = manufacturerId,
                    IsPrescriptionNeeded = medicamentModel.IsPrescriptionNeeded, 
                    DosageFormId = dosageFormId, 
                    Specification = medicamentModel.Specification, 
                    Amount = medicamentModel.Amount, 
                    Unit = medicamentModel.Unit,
                    CategoryId = categoryId
                } );
                var getMedicamentQuery = @"USE GreenHealth; SELECT Id, Name, Price, PriceMultiplier, ManufacturerId, 
                                                           IsPrescriptionNeeded, DosageFormId, Specification, Amount, Unit, ImageName, CategoryId FROM Medicaments WHERE Id = @Id;";
                var medicamentEntity =
                    await connection.QueryFirstOrDefaultAsync<MedicamentEntity>(getMedicamentQuery, new {Id = medicamentId});
                return medicamentEntity;
            }
        }
        
        public async Task ChangeHideVal(bool isHide, int id)
        {
            await using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var query = @"USE GreenHealth; UPDATE Medicaments SET isHide = @isHide WHERE Id = @id";
                    connection.Query(query, new {isHide = isHide, id = id});
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}