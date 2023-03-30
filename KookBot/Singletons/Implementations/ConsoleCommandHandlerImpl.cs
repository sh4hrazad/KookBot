using System.Reflection;
using KookBot.Components;

namespace KookBot.Singletons;

public class ConsoleCommandHandlerImpl : IConsoleCommandHandler {
        private readonly IList<Type> _classTypes = new List<Type>();
        private readonly IDictionary<string, MethodInfo?> _methods = new Dictionary<string, MethodInfo?>();

        public event EventHandler? OnKilled;

        public ConsoleCommandHandlerImpl() { }

        public IConsoleCommandHandler RegisterCommandsClass<TClass>() {
                var classType = typeof(TClass);
                var cmdNames = new List<string>();

                // classType.GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
                var methods = classType.GetMethods();

                foreach (var m in methods) {
                        var att = (ConsoleCommandAttribute?)m.GetCustomAttribute(
                                typeof(ConsoleCommandAttribute),
                                true
                        );

                        if (att == null) {
                                continue;
                        }

                        _methods.Add(att.Command, m);

                        Console.WriteLine($"Found registered command [{att.Command}] -> {classType.Name}::{m.Name}()");
                }

                return this;
        }

        public void StartCommandListener() {
                while (true) {
                        Console.Write("> ");

                        var command = Console.ReadLine()!;

                        if (string.IsNullOrEmpty(command)) {
                                continue;
                        }

                        if (command == "kill") {
                                OnKilled?.Invoke(this, EventArgs.Empty);

                                break;
                        }

                        var splited = command.Split(" ");

                        if (_methods.TryGetValue(splited[0], out var value)) {
                                value?.Invoke(null, new object?[] { splited });
                        } else {
                                Console.WriteLine($"Command not found: {command}");
                        }
                }
        }
}
