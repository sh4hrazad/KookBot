﻿namespace KookBot.Core;

public interface ICommandHandler {
        public static ICommandHandler Instance => DependencyInjection.Get<ICommandHandler>();

        public event EventHandler OnKilled;

        public InvokeResult TryInvokeCommand(CommandType type, string command, object? json);

        public ICommandHandler RegisterConsoleCommands<TClass>();

        public ICommandHandler RegisterChatCommands<TClass>();

        public void StartCommandListener();
}
