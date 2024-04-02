using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public class DrinksData : IDrinksData
{
    private readonly ISqlDataAccess _db;

    public DrinksData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<DrinksModel>> GetDrinks(int drinkTypeId = 0, int drinkSortId = 0)
    {
        var sql = drinkSortId switch
        {
            0 => drinkTypeId == 0
                ? "select * from ViewDrinks;"
                : "select * from ViewDrinks where DrinkTypeId = @DrinkTypeId;",
            1 => drinkTypeId == 0
                ? "select * from ViewDrinks order by Price;"
                : "select * from ViewDrinks where DrinkTypeId = @DrinkTypeId order by Price;",
            2 => drinkTypeId == 0
                ? "select * from ViewDrinks order by Price DESC;"
                : "select * from ViewDrinks where DrinkTypeId = @DrinkTypeId order by Price DESC;",
            3 => drinkTypeId == 0
                ? "select * from ViewDrinks order by Name, Flavor;"
                : "select * from ViewDrinks where DrinkTypeId = @DrinkTypeId order by Name, Flavor;",
            4 => drinkTypeId == 0
                ? "select * from ViewDrinks order by Name DESC, Flavor DESC;"
                : "select * from ViewDrinks where DrinkTypeId = @DrinkTypeId order by Name DESC, Flavor DESC;",
            _ => "select * from ViewDrinks;"
        };

        var parameters = new
        {
            DrinkTypeId = drinkTypeId
        };

        return _db.LoadData<DrinksModel, dynamic>(sql, parameters);
    }


    public Task<List<DrinkTypesModel>> GetDrinkTypes()
    {
        const string sql = "select * from DrinkTypes order by DrinkTypeId;";

        return _db.LoadData<DrinkTypesModel, dynamic>(sql, new { });
    }
}