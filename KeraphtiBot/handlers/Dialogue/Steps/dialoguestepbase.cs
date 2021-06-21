using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace KeraphtiBot.Handlers.Dialogue.Steps
{
    public abstract class DialogueStepBase : IDialogueStep

    {
        protected readonly string _content;
        public DialogueStepBase(string content)
        {
            _content = content;
        }
        
            public Action<DiscordMessage> OnMessageAdded { get; set;} = delegate { };

            public abstract IDialogueStep NextStep { get; }

            public abstract Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser User);

            protected async Task TryAgain(DiscordChannel channel, string problem)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Please Try Again",
                Color = DiscordColor.Red,
            };

            embedBuilder.AddField("There was a problem with your previous input", problem);

            var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

            OnMessageAdded(embed);
        }
    }
}