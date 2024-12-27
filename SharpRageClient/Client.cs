using RAGE;
using RAGE.Ui;
using System;
using System.Linq;

namespace SharpRageClient
{
    public class Client : RAGE.Events.Script
    {
        private readonly Browser _window;

        public Client()
        {
            _window = new Browser("http://package/browser/wwwroot/index.html");
            _window.Active = false;
            _window.MarkAsChat();

            RAGE.Events.OnPlayerCommand += OnPlayerCommand;

            RAGE.Input.Bind(VirtualKeys.Escape, true, () =>
            {
                _window.Active = false;
            });

            RAGE.Input.Bind(VirtualKeys.F5, true, () =>
            {
                _window.Reload(true);
            });
        }

        private void OnPlayerCommand(string cmd, Events.CancelEventArgs cancel)
        {
            if (cmd.StartsWith("open_window"))
            {
                _window.Active = true;
                Cursor.ShowCursor(true, true);
            }

            if (cmd.StartsWith("close_window"))
            {
                _window.Active = false;
                Cursor.ShowCursor(false, false);
            }

            if (cmd.StartsWith("call_event"))
            {
                string paramStr = cmd.Substring("call_event".Length).Trim();
                string[] args = paramStr.Split(" ");
                string eventName = args.Length > 0 ? args[0] : string.Empty;
                if (string.IsNullOrWhiteSpace(eventName))
                {
                    RAGE.Ui.Console.LogLine(ConsoleVerbosity.Error, "Error eventname");
                    return;
                }

                _window.CallBlazor(eventName, args.Skip(1));
            }
        }
    }
}
