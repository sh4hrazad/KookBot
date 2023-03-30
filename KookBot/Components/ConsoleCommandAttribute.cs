namespace KookBot.Components; 

[AttributeUsage(AttributeTargets.Method)]
public class ConsoleCommandAttribute : Attribute {
        public string Command { get; set; }

        public ConsoleCommandAttribute(string command) {
                Command = command;
        }
}
