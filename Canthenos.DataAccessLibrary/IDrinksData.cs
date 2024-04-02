using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public interface IDrinksData
{
    Task<List<DrinksModel>> GetDrinks(int drinkTypeId = 0, int drinkSortId = 0);
    Task<List<DrinkTypesModel>> GetDrinkTypes();
}