﻿using System.Configuration;
using KookBot;
using KookBot.Components;
using KookBot.Enums;
using KookBot.Interfaces;
using KookBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

DependencyInjection.ConfigureServices(
        () => {
                IServiceCollection services = new ServiceCollection();

                services.AddScoped<IKookHttpBot, KookHttpBotImpl>()
                        .AddScoped<IKookWsBot, KookWsBotImpl>()
                        .AddScoped<ICommandHandler, CommandHandlerImpl>();

                return services;
        }
);

IKookHttpBot.Instance.Token = config["token"] ?? string.Empty;

var wss = await IKookHttpBot.Instance.GetWebSocketUrl();

if (wss!.Code != 0) {
        throw new Exception($"Failed to get Websocket URL! Reason: {wss.Message}");
}

var connectResult = IKookWsBot.Instance.Setup(wss!.Data.Url).TryConnect();

IKookWsBot.Instance.Info($"Kook Bot login status: {connectResult.ResultCode}");

ICommandHandler.Instance.OnKilled += (sender, e) => {
        IKookHttpBot.Instance.OfflineBot();
        IKookWsBot.Instance.Close();
        IKookWsBot.Instance.Info("Kook Bot is now OFFLINE!");
};

ICommandHandler.Instance
        .RegisterConsoleCommands<ConsoleCommands>()
        .RegisterChatCommands<ChatCommands>()
        .StartCommandListener();
