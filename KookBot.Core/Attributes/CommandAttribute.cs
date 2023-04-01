namespace KookBot.Core;

[AttributeUsage(AttributeTargets.Method)]
public abstract class CommandAttribute : Attribute {
        public string Command { get; private set; }
        public CommandType Flag { get; }

        public CommandAttribute(string command, CommandType flag) {
                Command = command;
                Flag = flag;
        }
}
