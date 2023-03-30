namespace KookBot.Attributes; 

[AttributeUsage(AttributeTargets.Method)]
public abstract class CommandAttribute : Attribute {
        public string Command { get; private set; }

        public CommandAttribute(string command) {
                Command = command;
        }
}
