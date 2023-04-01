using System.IO.Compression;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;

namespace KookBot.Core;

public class WebsocketMessageReceiver : WebSocketPluginBase<WebSocketClient> {
        protected override Task OnHandleWSDataFrameAsync(WebSocketClient client, WSDataFrameEventArgs e) {
                return base.OnHandleWSDataFrameAsync(client, e);
        }

        protected override void OnHandleWSDataFrame(WebSocketClient client, WSDataFrameEventArgs e) {
                var data = string.Empty;

                // inflate compressed data
                if (e.DataFrame.Opcode == WSDataType.Binary) {
                        data = HandleBinaryData(client, e.DataFrame.PayloadData);
                }

                if (string.IsNullOrEmpty(data)) {
                        return;
                }

                // TODO: we should fix those exceptions on parsing json
                IKookWsBot.Instance.Info($"Websocket Event Received! {data}");

                var json = data.FromJson<WebsocketMessage<WebsocketEvent>>();

                if (json.Signal == WebsocketSignal.Event) {
                        var content = json.Data.Content;

                        if (
                                json.Data.Type is MessageType.Text or MessageType.KMarkdown &&
                                IsCallingCommand(content)
                        ) {
                                ICommandHandler.Instance.TryInvokeCommand(CommandType.Chat, content, json);
                        }

                        IKookWsBot.Instance.Info(
                                $"""
                                {json.Data.Extra.Author.Nickname} ({json.Data.Extra.Author.Username}#{json.Data.Extra.Author.IdentifyNum}): {content}
                                """
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
