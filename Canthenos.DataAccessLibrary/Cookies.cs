using Microsoft.JSInterop;

namespace Canthenos.DataAccessLibrary;

public class Cookies : ICookies
{
    private readonly IJSRuntime _jsRuntime;

    public Cookies(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task WriteCookie(string name, string value, DateTime expires)
    {
        await _jsRuntime.InvokeAsync<object>("WriteCookie.WriteCookie", name, value, expires);
    }

    public async Task<string> ReadCookie(string name)
    {
        return await _jsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", name);
    }
}