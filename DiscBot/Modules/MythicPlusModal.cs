using Discord.Interactions;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscBot.Modules
{
    public class MythicPlusModal : IModal
    {
        // Modal Title
        public string Title => "M+ Rating";
        // Text box title
        [InputLabel("Request a players .io score")]
        // Strings with the ModalTextInput attribute will automatically become components.
        [ModalTextInput("greeting_input", TextInputStyle.Paragraph, placeholder: "Hydrude", maxLength: 200)]
        // string to hold the user input text
        public string? Message { get; set; }
    }
}
