namespace KookBot.Core;

public class ConsoleCommandAttribute : CommandAttribute {
        public ConsoleCommandAttribute(string command) : base(command, CommandType.Console) { }
}
