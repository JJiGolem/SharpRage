using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Reflection;
using System.Text.Json;

namespace SharpRageUI.API
{
    public class EventsContainer
    {
        private readonly ILogger<EventsContainer> _logger;
        private readonly Dictionary<string, List<EventHandler>> _clientEvents;
        private readonly object _locker;

        public EventsContainer(ILogger<EventsContainer> logger)
        {
            _logger = logger;
            _clientEvents = new Dictionary<string, List<EventHandler>>();
            _locker = new object();
        }

        [JSInvokable]
        public void Call(string eventName, JsonElement[]? args = null)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                _logger.LogWarning("CallClientEvent: eventName is null or empty");
                return;
            }

            _logger.LogInformation($"Dotnet Called: {eventName}");

            var @params = args is null ? Array.Empty<string>() : args.Select(a => a.ToString());
            _logger.LogInformation(string.Join("\n", @params));
            CallClientEvent(eventName, @params.ToArray());
        }

        public void Add(string eventName, Action callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    callback?.Invoke();
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        #region Переопределения

        public void Add<T1>(string eventName, Action<T1> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    _logger.LogInformation(args[0].GetType().ToString());

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2>(string eventName, Action<T1, T2> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3>(string eventName, Action<T1, T2, T3> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6>(string eventName, Action<T1, T2, T3, T4, T5, T6> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)),
                        (T11)Convert.ChangeType(args[10], typeof(T11)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)),
                        (T11)Convert.ChangeType(args[10], typeof(T11)),
                        (T12)Convert.ChangeType(args[11], typeof(T12)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)),
                        (T11)Convert.ChangeType(args[10], typeof(T11)),
                        (T12)Convert.ChangeType(args[11], typeof(T12)),
                        (T13)Convert.ChangeType(args[12], typeof(T13)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)),
                        (T11)Convert.ChangeType(args[10], typeof(T11)),
                        (T12)Convert.ChangeType(args[11], typeof(T12)),
                        (T13)Convert.ChangeType(args[12], typeof(T13)),
                        (T14)Convert.ChangeType(args[13], typeof(T14)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
        {
            AddInternal(eventName, callback.Method, args =>
            {
                try
                {
                    var methodInfo = callback.Method;
                    if (!OnVerify(eventName, methodInfo, args))
                        return;

                    callback.Invoke((T1)Convert.ChangeType(args[0], typeof(T1)),
                        (T2)Convert.ChangeType(args[1], typeof(T2)),
                        (T3)Convert.ChangeType(args[2], typeof(T3)),
                        (T4)Convert.ChangeType(args[3], typeof(T4)),
                        (T5)Convert.ChangeType(args[4], typeof(T5)),
                        (T6)Convert.ChangeType(args[5], typeof(T6)),
                        (T7)Convert.ChangeType(args[6], typeof(T7)),
                        (T8)Convert.ChangeType(args[7], typeof(T8)),
                        (T9)Convert.ChangeType(args[8], typeof(T9)),
                        (T10)Convert.ChangeType(args[9], typeof(T10)),
                        (T11)Convert.ChangeType(args[10], typeof(T11)),
                        (T12)Convert.ChangeType(args[11], typeof(T12)),
                        (T13)Convert.ChangeType(args[12], typeof(T13)),
                        (T14)Convert.ChangeType(args[13], typeof(T14)),
                        (T15)Convert.ChangeType(args[14], typeof(T15)));
                }
                catch (Exception e)
                {
                    HandleEventException(eventName, callback.Method, e);
                }
            });
        }

        public void Remove(string eventName)
        {
            lock (_locker)
            {
                _clientEvents?.Remove(eventName);
            }
        }

        public void Remove(string eventName, Action callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1>(string eventName, Action<T1> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2>(string eventName, Action<T1, T2> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3>(string eventName, Action<T1, T2, T3> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6>(string eventName, Action<T1, T2, T3, T4, T5, T6> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }

        public void Remove<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
        {
            RemoveInternal(eventName, callback.Method);
        }
        #endregion

        private void CallClientEvent(string eventName, object[] args)
        {
            lock (_locker)
            {
                if (!_clientEvents.ContainsKey(eventName))
                {
                    _logger.LogWarning($"CallClientEvent: Not contains key by eventName: {eventName}");
                    return;
                }

                _clientEvents[eventName].ForEach(handler => handler?.Callback?.Invoke(args));

                _logger.LogInformation(
                    $"CallClientEvent: Called event: {eventName}, " +
                    $"args: {JsonSerializer.Serialize(args)}");
            }
        }

        private void AddInternal(string eventName, MethodInfo method, Action<object[]> callback)
        {
            lock (_locker)
            {
                if (!_clientEvents.ContainsKey(eventName))
                    _clientEvents.Add(eventName, new List<EventHandler>());

                if (_clientEvents[eventName].Count > 0 && _clientEvents[eventName].Exists(e => e.Method == method))
                    throw new InvalidOperationException($"Duplicate {nameof(method)} ({method.Name}) on event {eventName}");

                _clientEvents[eventName].Add(new EventHandler()
                {
                    Method = method,
                    Callback = callback
                });
            }
        }

        private void RemoveInternal(string eventName, MethodInfo method)
        {
            lock (_locker)
            {
                if (!_clientEvents.ContainsKey(eventName) || method == null)
                    return;

                EventHandler? handler = _clientEvents[eventName].FirstOrDefault(e => e.Method == method);
                if (handler is null)
                    return;

                _clientEvents[eventName].Remove(handler);
            }
        }

        /// <summary>
        /// Проверка обработчика
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="methodInfo"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool OnVerify(string eventName, MethodInfo methodInfo, object[] args)
        {
            var parameters = methodInfo.GetParameters();

            if (parameters.Length != args.Length)
            {
                _logger.LogError(
                    $"eventName: {eventName}; callback: {methodInfo.DeclaringType}, {methodInfo.Name};" +
                    $"Exception: Wroung count of args");
                return false;
            }

            return true;
        }

        private void HandleEventException(string eventName, MethodInfo method, Exception e)
        {
            _logger.LogError(
                $"eventName: {eventName}; callback: {method.DeclaringType}, {method.Name}; Exception: {e.Message}\n" +
                $"{e.StackTrace}"
            );
        }

        private class EventHandler
        {
            public MethodInfo? Method { get; set; }
            public Action<object[]>? Callback { get; set; }
        }
    }
}