using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public class DishesData : IDishesData
{
    private readonly ISqlDataAccess _db;

    public DishesData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<DishesModel>> GetDishes(int id = 0)
    {
        var sql = id switch
        {
            0 => "select * from ViewDishes;",
            1 => "select * from ViewDishes order by Price;",
            2 => "select * from ViewDishes order by Price DESC;",
            3 => "select * from ViewDishes order by Name;",
            4 => "select * from ViewDishes order by Name DESC;",
            _ => "select * from ViewDishes;"
        };

        return _db.LoadData<DishesModel, dynamic>(sql, new { });
    }
}