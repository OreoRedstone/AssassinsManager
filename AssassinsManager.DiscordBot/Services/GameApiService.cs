using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssassinsManager.DiscordBot.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AssassinsManager.DiscordBot.Services;

public class GameApiService(IConfiguration config): IGameApiService
{
    private readonly Uri apiUrl = new(config.GetConnectionString("GameApi"));
}
