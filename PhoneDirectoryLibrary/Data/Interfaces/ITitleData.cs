namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface ITitleData
    {
        void ClearCache();
        Task<List<TitleModel>> GetTitlesAsync();
    }
}