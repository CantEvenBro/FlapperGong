using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot;
using System.Reflection;
using System.Resources;

public class Program
{

    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    public async Task MainAsync()
    {
        DiscordBot.Game game1 = new DiscordBot.Game(5);
        Console.WriteLine("game1's gridsize is " + game1.cellLength);
        Console.WriteLine("Game1 has " + game1.units.Count() + " units");
        string cockmunch = "This is the value of cockmunch";
        game1.AddUnit(cockmunch,1,3);

        Console.WriteLine("Game1 NOW  has " + game1.units.Count() + " units");


        Console.WriteLine("The first unit in game1's 'unit' List has the color " + game1.units[0].colour);
        // Everything above here can be edited back to 20
        var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(_config);
        _client.Log += Log;
        var token = DiscordBot.Properties.Resources.TokenString;
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        //Commands
        CommandService commandService = new CommandService();
        CommandHandler commandHandler = new CommandHandler(_client, commandService);
        _client.MessageUpdated += MessageUpdated;
        _client.MessageReceived += MessageReceived;
        _client.Ready += () =>
        {
            Console.WriteLine("Bot is connected!");
            return Task.CompletedTask;
        };


        await Task.Delay(-1);
    }

    private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {

        Console.WriteLine("Reading message: ");
        // If the message was not in the cache, downloading it will result in getting a copy of `after`.
        var message = await before.GetOrDownloadAsync();
        Console.WriteLine($"{message} -> {after}");
    }
    private async Task MessageReceived(SocketMessage messageParams)
    {
        Console.WriteLine("Got message in Program.cs" + messageParams.Content);
        if (!
                (
                    messageParams.Author.IsBot
                )
            )
        {
            await messageParams.Channel.SendMessageAsync($"Got that! " + messageParams.Author.Username);
        }
    }
    }
