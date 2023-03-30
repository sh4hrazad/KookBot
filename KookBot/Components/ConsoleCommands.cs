using KookBot.Attributes;
using KookBot.Enums;
using KookBot.Interfaces;

namespace KookBot.Components; 

public class ConsoleCommands {
        [ConsoleCommand("ping")]
        public static void Ping(string[] args) {
                Console.WriteLine("pong");
        }

        [ConsoleCommand("say")]
        public static void Say(string[] args) {
                if (args.Length < 3) {
                        Console.WriteLine("Usage: say <target_id> <message>.");
                        return;
                }
                
                var channel = args[1];

                var message = string.Join(" ", args[2..]);
                
                IKookHttpBot.Instance.SendMessage(MessageType.Text, channel, message).Wait();
        }
}
