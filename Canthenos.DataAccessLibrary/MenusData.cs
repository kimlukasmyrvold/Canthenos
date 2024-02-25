using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public class MenusData : IMenusData
{
    private readonly ISqlDataAccess _db;

    public MenusData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<MenusModel>> GetMenus()
    {
        const string sql = "select * from ViewMenusAdvanced order by DayId;";

        return _db.LoadData<MenusModel, dynamic>(sql, new { });
    }
}