
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

                _cache.Set(cacheName, titles, TimeSpan.FromHours(4));
                return titles;
            }

            return cacheOutput;
        }

        public void ClearCache()
        {
            _cache.Remove(cacheName);
        }
    }
}
