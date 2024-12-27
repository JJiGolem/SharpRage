using Microsoft.JSInterop;

namespace SharpRageUI.API
{
    public class MPService
    {
        private static IJSObjectReference _mp;

        public void SetMp(IJSObjectReference mp)
        {
            _mp = mp;
        }

        public ValueTask CallClient(string eventName, params object?[]? args)
        {
            return _mp.InvokeVoidAsync("RageAPI.callClient", [eventName, .. args]);
        }

        public ValueTask Invoke(string eventName, params object?[]? args)
        {
            return _mp.InvokeVoidAsync("RageAPI.invoke", [eventName, .. args]);
        }
    }
}
