namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface ITitleData
    {
        Task<List<TitleModel>> GetTitlesAsync();
    }
}