// See https://aka.ms/new-console-template for more information
using AssassinsManager.DiscordBot.Services;
using AssassinsManager.DiscordBot.Services.Interfaces;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

serviceCollection
    .AddSingleton<IConfiguration>(_ => config)
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton<IDiscordService, DiscordService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

await serviceProvider.GetRequiredService<IDiscordService>().ConfigureAsync();

await Task.Delay(Timeout.Infinite);