using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public interface IDishesData
{
    Task<List<DishesModel>> GetDishes(int id = 0);
}