using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public interface IMenusData
{
    Task<List<MenusModel>> GetMenus();
}