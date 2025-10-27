using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp1.Services
{
    public class StorageService : IStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        public StorageService(IJSRuntime jSRuntime) => _jsRuntime = jSRuntime;
        public async Task SetItemAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
        }
    }
}
