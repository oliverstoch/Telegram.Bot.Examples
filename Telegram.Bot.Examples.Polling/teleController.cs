using TL;
using WTelegram;

namespace Telegram.Bot.Examples.Polling;


internal static class teleController {
    private static readonly Client Client = new WTelegram.Client(Config);

    //remove config to sepreate folder?
    private static string? Config(string what)
    {
        switch (what)
        {
            case "api_id": return "11035583";
            case "api_hash": return "eac6102b7b5227f54b207de61f4a12e3";
            case "phone_number": return "+972552239822";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine();
            case "first_name": return "Oliver";      // if sign-up is required
            case "last_name": return "stoch";        // if sign-up is required
            case "password": return "MatanMizrahi1";     // if user has enabled 2FA
            default: return null;                  // let WTelegramClient decide the default config
        }
    }
    public static async void SendMessageInAChat()
    {
        var my = await Client.LoginUserIfNeeded();

        Console.WriteLine($"We are logged-in as {my.username ?? my.first_name + " " + my.last_name} (id {my.id})");



        var chats = await Client.Messages_GetAllChats();
        Console.WriteLine(chats.chats.Count);
        Console.WriteLine( "PRINTING ALL CHATS!");
        Console.WriteLine("user has entered the foll");
        foreach (var (id, chat) in chats.chats)
            switch (chat) // example of downcasting to their real classes:
            {
                case Chat smallgroup when smallgroup.IsActive:
                    Console.WriteLine($"{id}:  Small group: {smallgroup.title} with {smallgroup.participants_count} members");
                    break;
                case Channel group when group.IsGroup:
                    Console.WriteLine($"{id}: Group {group.username}: {group.title}");
                    break;
                case Channel channel:
                    Console.WriteLine($"{id}: Channel {channel.username}: {channel.title}");
                    break;
            }


        Console.Write("Type a chat ID to send a message: ");
        long chatId = long.Parse(Console.ReadLine()!);
        var target = chats.chats[chatId];
        Console.WriteLine($"Sending a message in chat {chatId}: {target.Title}");
        await Client.SendMessageAsync(target, "Hello, World war3?");
    }
    public static async void CreateGroup()
    {
        Console.WriteLine("attempting To create a group");

        var my = await Client.LoginUserIfNeeded();
        Console.WriteLine($"We are logged-in as {my.username ?? my.first_name + " " + my.last_name} (id {my.id})");
        await Client.Channels_CreateChannel("CommieHippyLong", "birds ", false, false, false, null, null);
        await Client.AddChatUser(null, null, 0);


    }
}
