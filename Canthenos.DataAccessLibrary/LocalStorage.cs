using Microsoft.JSInterop;

namespace Canthenos.DataAccessLibrary;

public class LocalStorage : ILocalStorage
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> Get(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("LocalStorage.get", key);
    }

    public async Task Set(string key, string value)
    {
        await _jsRuntime.InvokeAsync<object>("LocalStorage.set", key, value);
    }

    public async Task Remove(string key)
    {
        await _jsRuntime.InvokeAsync<object>("LocalStorage.remove", key);
    }

    public async Task Clear()
    {
        await _jsRuntime.InvokeAsync<object>("LocalStorage.clear");
    }
}