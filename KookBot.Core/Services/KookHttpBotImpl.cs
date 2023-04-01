using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TouchSocket.Core;

namespace KookBot.Core;

public class KookHttpBotImpl : IKookHttpBot {
        private const string ApiBaseUrl = "https://www.kookapp.cn";

        public string Token { private get; set; } = string.Empty;

        private readonly HttpClient _httpClient = new();

        private async Task<RestResponse<TResponse>> HandleRequestAsync<TResponse>(
                HttpMethod method, string url, Action<RestResponse<TResponse>>? then = null
        ) {
                return await HandleRequestAsync<object, TResponse>(method, url, null, then);
        }

        private async Task<RestResponse<TResponse>> HandleRequestAsync<TOptions, TResponse>(
                HttpMethod method, string url, TOptions? options, Action<RestResponse<TResponse>>? then = null
        ) {
                var msg = new HttpRequestMessage(method, $"{ApiBaseUrl}{url}");

                msg.Headers.Accept.Clear();
                msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                msg.Headers.Add("Authorization", $"Bot {Token}");

                msg.Content = new StringContent(options.ToJson(), Encoding.UTF8, "application/json");

                // return (await httpClient.GetStringAsync($"{apiBaseUrl}{url}")).FromJson<TResult>();
                var response = await _httpClient.SendAsync(msg);

                var result = await response.Content.ReadFromJsonAsync<RestResponse<TResponse>>();

                then?.Invoke(result!);

                return result!;
        }

        public async Task<RestResponse<GatewayIndexResponse>> GetWebSocketUrl() {
                return await HandleRequestAsync<GatewayIndexResponse>(HttpMethod.Get, "/api/v3/gateway/index");
        }

        public async Task<RestResponse<object>> OfflineBot() {
                return await HandleRequestAsync<object>(HttpMethod.Post, "/api/v3/user/offline");
        }

        public async Task<RestResponse<CreateMessageResponse>> SendMessage(
                MessageType type, string targetId, string content, string quoteMsgId
        ) {
                return await HandleRequestAsync<CreateMessageOptions, CreateMessageResponse>(
                        HttpMethod.Post,
                        "/api/v3/message/create",
                        new(type, targetId, content, quoteMsgId, string.Empty, string.Empty),
                        result => {
                                if (result.Code == 0) {
                                        IKookWsBot.Instance.Info($"Message sent. Id: {result.Data.MessageId}");
                                } else {
                                        IKookWsBot.Instance.Info($"Message failed to send. Reason: {result.Message}");
                                }
                        }
                );
        }
}
