using System.Reflection;
using KookBot.Attributes;
using KookBot.Enums;
using KookBot.Interfaces;
using KookBot.Structs;

namespace KookBot.Services;

public class CommandHandlerImpl : ICommandHandler {
        private IDictionary<string, MethodInfo> _chatCommands = new Dictionary<string, MethodInfo>();
        private IDictionary<string, MethodInfo> _consoleCommands = new Dictionary<string, MethodInfo>();

        public event EventHandler? OnKilled;

        public ICommandHandler RegisterChatCommands<TClass>() {
                return RegisterCommands<TClass>(this, _chatCommands);
        }

        public ICommandHandler RegisterConsoleCommands<TClass>() {
                return RegisterCommands<TClass>(this, _consoleCommands);
        }

        private static ICommandHandler RegisterCommands<TClass>(
                ICommandHandler thisClass, IDictionary<string, MethodInfo> dictionary
        ) {
                var classType = typeof(TClass);
                var cmdNames = new List<string>();

                // classType.GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
                var methods = classType.GetMethods(BindingFlags.Public | BindingFlags.Static);

                foreach (var m in methods) {
                        var att = (CommandAttribute?)m.GetCustomAttribute(
                                typeof(CommandAttribute),
                                true
                        );

                        if (att == null) {
                                continue;
                        }

                        dictionary.Add(att.Command, m);

                        Console.WriteLine($"Found registered command [{att.Command}] -> {classType.Name}::{m.Name}()");
                }

                return thisClass;
        }

        public InvokeResult TryInvokeCommand(CommandType type, string command, object? json = null) {
                var split = command.Split(" ");

                var methods = type switch {
                        CommandType.Chat => _chatCommands,
                        CommandType.Console => _consoleCommands,
                        _ => null,
                };

                if (type == CommandType.Chat) {
                        // remove "!", "/", "."
                        split[0] = split[0].Remove(0, 1);
                }

                if (methods != null && methods.TryGetValue(split[0], out var method)) {
                        return new(true, method?.Invoke(null, new object?[] { split, json }));
                }

                return new();
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

                        var invokeResult = TryInvokeCommand(CommandType.Console, command);

                        if (!invokeResult.Invoked) {
                                Console.WriteLine($"Command not found: {command}");
                        }
                }
        }
}
