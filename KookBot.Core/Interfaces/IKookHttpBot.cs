namespace KookBot.Core;

public interface IKookHttpBot {
        public static IKookHttpBot Instance => DependencyInjection.Get<IKookHttpBot>();

        public string Token { set; }

        /// <summary>
        /// Get the websocket entrypoint url.
        /// /api/v3/gateway/index
        /// </summary>
        /// <returns></returns>
        public Task<RestResponse<GatewayIndexResponse>> GetWebSocketUrl();

        /// <summary>
        /// Send message to a channel.
        /// /api/v3/message/create
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="content"></param>
        /// <param name="quoteMsgId"></param>
        /// <returns></returns>
        public Task<RestResponse<CreateMessageResponse>> SendMessage(
                MessageType type, string targetId, string content, string quoteMsgId = ""
        );

        public Task<RestResponse<object>> OfflineBot();
}
