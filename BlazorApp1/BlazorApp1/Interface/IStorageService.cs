namespace BlazorApp1.Interface
{
    public interface IStorageService
    {
        //spara
        Task SetItemAsync<T>(string key, T value);
        //hämta
        Task<T> GetItemAsync <T>(string key);
    }
}
