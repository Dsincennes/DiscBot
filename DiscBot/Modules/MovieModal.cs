using Discord;
using Discord.Interactions;

namespace DiscBot
{
    // Defines the modal that will be sent.
    public class MovieModal : IModal
    {
        // Modal form label
        public string Title => "Movie Request";
        // Text box title
        [InputLabel("Request Movie, ie: The Dark Knight!")]
        // Strings with the ModalTextInput attribute will automatically become components.
        [ModalTextInput("greeting_input", TextInputStyle.Paragraph, placeholder: "The Dark Knight", maxLength: 200)]
        // string to hold the user input text
        public string? Greeting { get; set; }
    }
}