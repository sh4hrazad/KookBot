using System.IO.Compression;
using KookBot.Enums;
using KookBot.Singletons;
using KookBot.Structs;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;

namespace KookBot.Components; 

public class WsMessageReceiver : WebSocketPluginBase<WebSocketClient> {
        protected override Task OnHandleWSDataFrameAsync(WebSocketClient client, WSDataFrameEventArgs e) {
                return base.OnHandleWSDataFrameAsync(client, e);
        }

        protected override void OnHandleWSDataFrame(WebSocketClient client, WSDataFrameEventArgs e) {
                // inflate compressed data
                if (e.DataFrame.Opcode == WSDataType.Binary) {
                        HandleBinaryData(client, e.DataFrame.PayloadData);
                }
        }

        private static void HandleBinaryData(WebSocketClient client, ByteBlock data) {
                var bytes = data.Buffer;

                using var ms = new MemoryStream(bytes, 2, bytes.Length - 2);
                using var inflater = new DeflateStream(ms, CompressionMode.Decompress);
                using var sr = new StreamReader(inflater);

                var str = sr.ReadToEnd();
                
                Console.WriteLine(str);

                var json = str.FromJson<WebSocketResult<WebsocketEventData>>();
                
                if (json.Signal == WebsocketSignal.Event) {
                        var content = json?.Data.Content;
                
                        IKookWsBot.Instance.Info($"{json?.Data.Extra.Author.Nickname}: {content}");
                }
        }
}
