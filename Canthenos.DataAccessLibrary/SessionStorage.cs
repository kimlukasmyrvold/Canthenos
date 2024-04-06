using Microsoft.JSInterop;

namespace Canthenos.DataAccessLibrary;

public class SessionStorage : ISessionStorage
{
    private readonly IJSRuntime _jsRuntime;

    public SessionStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<string> Get(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("SessionStorage.get", key);
    }
    
    public async Task Set(string key, string value)
    {
        await _jsRuntime.InvokeAsync<object>("SessionStorage.set", key, value);
    }
    
    public async Task Remove(string key)
    {
        await _jsRuntime.InvokeAsync<object>("SessionStorage.remove", key);
    }
    
    public async Task Clear()
    {
        await _jsRuntime.InvokeAsync<object>("SessionStorage.clear");
    }
    
}