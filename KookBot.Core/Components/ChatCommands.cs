namespace KookBot.Core;

public class ChatCommands {
        [ChatCommand("ping")]
        public static void Ping(string[] args, WebsocketMessage<WebsocketEvent> json) {
                if (json.Data.ChannelType != "GROUP") {
                        return;
                }

                IKookHttpBot.Instance.SendMessage(MessageType.Text, json.Data.TargetId, "pong", json.Data.MessageId);
        }
}
