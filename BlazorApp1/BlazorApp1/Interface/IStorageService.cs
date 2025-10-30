namespace BlazorApp1.Interface
{
    /// <summary>
    /// Interface contaning the save methods
    /// </summary>
    public interface IStorageService
    {
        // Save
        Task SetItemAsync<T>(string key, T value);
        // Get
        Task<T> GetItemAsync <T>(string key);
    }
}
