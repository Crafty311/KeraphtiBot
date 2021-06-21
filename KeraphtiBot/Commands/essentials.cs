using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Exceptions;
using DSharpPlus.CommandsNext.Entities;
using System.Linq;

namespace KeraphtiBot.Commands
{
        public class Essentials : BaseCommandModule
{
        [Command("poll")]
        [Description("I. Love. Democracy")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emoption)
        {
            var interactivity =  ctx.Client.GetInteractivity();
            var options = emoption.Select(x=> x.ToString());
            
            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "I. Love. Democracy",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);
            foreach(var option in emoption)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }
            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);

        }






}

}
