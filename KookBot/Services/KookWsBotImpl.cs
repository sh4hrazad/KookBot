using System.Reflection;
using KookBot.Attributes;
using KookBot.Components;
using KookBot.Interfaces;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace KookBot.Services; 

public class KookWsBotImpl : IKookWsBot {
        private readonly WebSocketClient _client = new();

        public IKookWsBot Setup(string url) {
                _client.Setup(
                        new TouchSocketConfig()
                                .SetRemoteIPHost(new IPHost(url))
                                .UsePlugin()
                                .ConfigureContainer(
                                        a => {
                                                a.AddConsoleLogger();
                                        }
                                )
                                .ConfigurePlugins(
                                        a => {
                                                a.Add<WsMessageReceiver>();
                                                a.UseWebSocketHeartbeat().Tick(TimeSpan.FromSeconds(5));
                                        }
                                )
                );

                return this;
        }

        public Result TryConnect() {
                return _client.TryConnect();
        }

        public void Info(string message) {
                _client.Logger.Info($"{message}");
        }

        public void Close() {
                _client.SafeClose("close");
        }
}
