using RAGE.Ui;

namespace SharpRageClient
{
    internal class Browser : HtmlWindow
    {
        public Browser(string url) : base(url)
        {
            
        }

        public void CallBlazor(string eventName, params object[] args)
        {
            Call("callEvent", eventName, RAGE.Util.Json.Serialize(args));
        }

        public void CallBlazor(string eventName, object args)
        {
            Call("callEvent", eventName, RAGE.Util.Json.Serialize(args));
        }

        public void CallBlazor(string eventName, string args)
        {
            Call("callEvent", eventName, args);
        }

        public void ShowPage(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
                route = "/";

            Call("callEvent", "SetRoute", route);
        }
    }
}
