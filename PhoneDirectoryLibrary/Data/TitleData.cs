using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        private const string cacheName = "TitleCache";

        public TitleData(ISQLDataAccess database, IMemoryCache cache)
        {
            _database = database;
            _cache = cache;
        }

        public async Task<List<TitleModel>> GetTitlesAsync()
        {
            var cacheOutput = _cache.Get<List<TitleModel>>(cacheName);

            if(cacheOutput is null)
            {
                string storedProc = "dbo.spTitle_GetAll";

                var titles = await _database.LoadDataAsync<TitleModel, dynamic>
                    (
                    storedProc, new { }
                    );

                _cache.Set(cacheName, titles, TimeSpan.FromMinutes(5));
                return titles;
            }

            return cacheOutput;
        }
    }
}
