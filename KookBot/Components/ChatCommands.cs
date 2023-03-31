using KookBot.Attributes;
using KookBot.Enums;
using KookBot.Interfaces;
using KookBot.Structs;

namespace KookBot.Components;

public class ChatCommands {
        [ChatCommand("ping")]
        public static void Ping(string[] args, WebSocketResult<WebsocketEventData> json) {
                if (json.Data.ChannelType != "GROUP") {
                        return;
                }
                
                IKookHttpBot.Instance.SendMessage(MessageType.Text, json.Data.TargetId, "pong", json.Data.MessageId);
        }
}
