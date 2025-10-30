namespace BlazorApp1.Interface
{
    public interface IStorageService
    {
        // Save
        Task SetItemAsync<T>(string key, T value);
        // Get
        Task<T> GetItemAsync <T>(string key);
    }
}
