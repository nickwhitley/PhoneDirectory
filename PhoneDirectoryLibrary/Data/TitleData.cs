using PhoneDirectoryLibrary.Data.Interfaces;
using PhoneDirectoryLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectoryLibrary.Data
{
    public class TitleData : ITitleData
    {
        private readonly ISQLDataAccess _database;

        public TitleData(ISQLDataAccess database)
        {
            _database = database;
        }

        public Task<List<TitleModel>> GetTitlesAsync()
        {
            string storedProc = "dbo.spTitle_GetAll";

            return _database.LoadDataAsync<TitleModel, dynamic>
                (
                storedProc, new { }
                );
        }
    }
}
