namespace KookBot.Core;

public class ChatCommandAttribute : CommandAttribute {
        public ChatCommandAttribute(string command) : base(command, CommandType.Chat) { }
}
