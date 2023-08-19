using Discord;
using Discord.Interactions;
using DiscBot.Log;
using DiscBot.Modules;

namespace DiscBot
{
    public class RequestsModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService? Commands { get; set; }
        private readonly Logger _logger;

        public RequestsModule(ConsoleLogger logger)
        {
            // Constructor Injection of the ConsoleLogger, to be used to log the message sent by the user modal input
            _logger = logger;
        }

        // Movie Request Command
        [SlashCommand("movie", "Request a movie")]
        public async Task ModalInput()
        {
            await Context.Interaction.RespondWithModalAsync<MovieModal>("MovieRequestModal");
        }

        [ModalInteraction("MovieRequestModal")]
        public async Task ModalResponse(MovieModal modal)
        {
            // Build the message to send.
            string message = $"{modal.Greeting}";

            // Console Logging
            await _logger.Log(new LogMessage(LogSeverity.Info, "Modal : MovieRequestModal", $"Username: {Context.User.Username}, Movie Requested: {message}"));

            // Responding to user
            await Context.User.SendMessageAsync($"{Context.User.Username}, Your request of [{message}] has been received.");

            // writing to file for logging
            var movieRequestFile = @"C:\Users\Donal\Desktop\MovieRequests.txt";
            using (StreamWriter sw = File.AppendText(movieRequestFile))
            {
                sw.WriteLine($"Time: {string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now)} Username: {Context.User.Username}, Movie Requested: {message}");
            }

            var apiKey = "b67551695b04415d9576036e625a07d1";
            if (apiKey != null)
            {
                var movieMade = new MovieApiCalls();
                movieMade.MovieCreate(apiKey, message);
            }

            await RespondAsync(message, ephemeral: true);
        }

        // Mythic Plus Request Command
        [SlashCommand("mplus", "Request a io score")]
        public async Task MythicModalInput()
        {
            await Context.Interaction.RespondWithModalAsync<MythicPlusModal>("MythicRequestModal");
        }

        [ModalInteraction("MythicRequestModal")]
        public async Task MythicModalResponse(MythicPlusModal modal)
        {
            // Build the message to send.
            string message = $"{modal.Message}";

            // Console Logging
            await _logger.Log(new LogMessage(LogSeverity.Info, "Modal : MythicRequestModal", $"Username: {Context.User.Username}, Raider.io Requested: {message}"));

            // Responding to user
            await Context.User.SendMessageAsync($"{Context.User.Username}, Your request of [{message}] has been received.");

            // writing to file for logging
            var mythicRequestFile = @"C:\Users\D\Desktop\MythicRequests.txt";
            using (StreamWriter sw = File.AppendText(mythicRequestFile))
            {
                sw.WriteLine($"Time: {string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now)} Username: {Context.User.Username}, MPlus Requested: {message}");
            }

            var apiKey = "b67551695b04415d9576036e625a07d1";
            if (apiKey != null)
            {
                var mplus = new MPlusApiCalls();
                var score = mplus.MplusRequest(apiKey, message);
                double scoreRounded = Math.Round(double.Parse(score), 2);
                if(scoreRounded > 0)
                {
                    await Context.Channel.SendMessageAsync($"{message} ioScore:  {scoreRounded}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Check spelling of character");
                }
            }

            await RespondAsync(message, ephemeral: true);
        }

    }
}