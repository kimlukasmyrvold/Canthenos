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
        const int menuId = 1;
        const string sql = "select * from ViewMenus where MenuId=@MenuId order by DayId;";

        var parameters = new { MenuId = menuId };

        return _db.LoadData<MenusModel, dynamic>(sql, parameters);
    }
}