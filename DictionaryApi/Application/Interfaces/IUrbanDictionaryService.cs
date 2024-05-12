namespace Application.Interfaces
{
    public interface IUrbanDictionaryService
    {
        Task<string> GetDefinitionAsync(string term);
    }
}
