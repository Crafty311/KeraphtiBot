using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading;
using DSharpPlus.Entities;

namespace KeraphtiBot.Commands
{
    public class Dadmin : BaseCommandModule
    {
        [Command("ban")]
        public async Task Ban(CommandContext ctx,
            [Description("User banned")] DiscordMember member,
            [Description("How many days will ban take?")] int days,
            [RemainingText, Description("Reason")] string reason)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild guild = member.Guild;

            try
            {
                await guild.BanMemberAsync(member, days, reason);
                await ctx.RespondAsync($"User @{member.Username}#{member.Discriminator} was bonked by the banhammer holding {ctx.User.Username}");
            }
            catch (Exception)
            {
                await ctx.RespondAsync($"User {member.Username} cannot be blocked");
            }
        }
    }
}
