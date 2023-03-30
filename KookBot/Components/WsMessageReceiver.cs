using System.IO.Compression;
using System.Reflection;
using KookBot.Attributes;
using KookBot.Enums;
using KookBot.Interfaces;
using KookBot.Structs;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;

namespace KookBot.Components;

public class WsMessageReceiver : WebSocketPluginBase<WebSocketClient> {
        public IDictionary<string, MethodInfo> Methods { get; } = new Dictionary<string, MethodInfo>();

        protected override Task OnHandleWSDataFrameAsync(WebSocketClient client, WSDataFrameEventArgs e) {
                return base.OnHandleWSDataFrameAsync(client, e);
        }

        protected override void OnHandleWSDataFrame(WebSocketClient client, WSDataFrameEventArgs e) {
                string data = string.Empty;

                // inflate compressed data
                if (e.DataFrame.Opcode == WSDataType.Binary) {
                        data = HandleBinaryData(client, e.DataFrame.PayloadData);
                }

                if (string.IsNullOrEmpty(data)) {
                        return;
                }

                var json = data.FromJson<WebSocketResult<WebsocketEventData>>();

                if (json.Signal == WebsocketSignal.Event) {
                        var content = json?.Data.Content ?? string.Empty;

                        if (
                                json!.Data.Type == MessageType.Text && IsCallingCommand(content)
                        ) {
                                ICommandHandler.Instance.TryInvokeCommand(CommandType.Chat, content);
                        }

                        IKookWsBot.Instance.Info(
                                $"{json.Data.Extra.Author.Nickname} ({json.Data.Extra.Author.Username}#{json.Data.Extra.Author.IdentifyNum}): {content}"
                        );
                }
        }

        private static bool IsCallingCommand(string content) {
                return content[0] == '/' || content[0] == '!' || content[0] == '.';
        }

        private static string HandleBinaryData(WebSocketClient client, ByteBlock data) {
                var bytes = data.Buffer;

                using var ms = new MemoryStream(bytes, 2, bytes.Length - 2);
                using var inflater = new DeflateStream(ms, CompressionMode.Decompress);
                using var sr = new StreamReader(inflater);

                return sr.ReadToEnd();
        }
}
