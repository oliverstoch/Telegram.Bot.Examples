using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System;
using System.Linq;
using System.Net;
using System.Text;


using Telegram.Bot.Examples.controllers;
namespace Telegram.Bot.Examples.Polling;

public static class Program
{
    private static TelegramBotClient? _bot;


    public static async Task Main()
    {

        var allTasks =  Controller.GetAllTasksForUser();


        _bot = new TelegramBotClient(Configuration.BotToken);
        User me = await _bot.GetMeAsync();
        Console.Title = me.Username ?? "My awesome Bot";
        using var cts = new CancellationTokenSource();
        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
        _bot.StartReceiving(Handlers.HandleUpdateAsync,
                           Handlers.HandleErrorAsync,
                           receiverOptions,
                           cts.Token);

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        // Send cancellation request to stop bot
        cts.Cancel();
    }
}
