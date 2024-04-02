using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public class SnacksData : ISnacksData
{
    private readonly ISqlDataAccess _db;

    public SnacksData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<SnacksModel>> GetSnacks(int id = 0)
    {
        var sql = id switch
        {
            0 => "select * from ViewSnacks;",
            1 => "select * from ViewSnacks order by Price;",
            2 => "select * from ViewSnacks order by Price DESC;",
            3 => "select * from ViewSnacks order by Name;",
            4 => "select * from ViewSnacks order by Name DESC;",
            _ => "select * from ViewSnacks;"
        };

        return _db.LoadData<SnacksModel, dynamic>(sql, new { });
    }
}