using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using KookBot.Enums;
using KookBot.Structs;
using TouchSocket.Core;

namespace KookBot.Singletons;

public class KookHttpBotImpl : IKookHttpBot {
        private const string ApiBaseUrl = "https://www.kookapp.cn";
        
        public string Token { private get; set; } = string.Empty;

        private readonly HttpClient _httpClient = new();

        private async Task<TResult?> HandleRequestAsync<TResult>(
                HttpMethod method, string url, Action<TResult?>? then = null
        ) {
                return await HandleRequestAsync<object, TResult?>(method, url, null, then);
        }

        private async Task<TResult?> HandleRequestAsync<TOptions, TResult>(
                HttpMethod method, string url, TOptions? options, Action<TResult?>? then = null
        ) {
                var msg = new HttpRequestMessage(method, $"{ApiBaseUrl}{url}");

                msg.Headers.Accept.Clear();
                msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                msg.Headers.Add("Authorization", $"Bot {Token}");

                msg.Content = new StringContent(options.ToJson(), Encoding.UTF8, "application/json");

                // return (await httpClient.GetStringAsync($"{apiBaseUrl}{url}")).FromJson<TResult>();
                var response = await _httpClient.SendAsync(msg);

                var result = await response.Content.ReadFromJsonAsync<TResult>();

                then?.Invoke(result);

                return result;
        }

        public async Task<RestResult<GatewayData>?> GetWebSocketUrl() {
                return await HandleRequestAsync<RestResult<GatewayData>>(HttpMethod.Get, "/api/v3/gateway/index");
        }

        public async Task<RestResult<object>?> OfflineBot() {
                return await HandleRequestAsync<RestResult<object>>(HttpMethod.Post, "/api/v3/user/offline");
        }

        public async Task<RestResult<CreateMessageData>?> SendMessage(
                MessageType type, string targetId, string content, string quoteMsgId
        ) {
                var options = new CreateMessageOptions(type, targetId, content, quoteMsgId, string.Empty, string.Empty);

                return await HandleRequestAsync<CreateMessageOptions, RestResult<CreateMessageData>>(
                        HttpMethod.Post,
                        "/api/v3/message/create",
                        options,
                        result => {
                                if (result!.Code == 0) {
                                        IKookWsBot.Instance.Info($"Message sent. Id: {result.Data.MessageId}");
                                } else {
                                        IKookWsBot.Instance.Info($"Message failed to send. Reason: {result.Message}");
                                }
                        }
                );
        }
}
