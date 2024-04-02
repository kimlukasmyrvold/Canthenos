using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public interface ISnacksData
{
    Task<List<SnacksModel>> GetSnacks(int id = 0);
}