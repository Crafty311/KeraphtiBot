using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;

namespace KeraphtiBot.Commands
{
    public class Misc : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }
        
        [Command("serverinfo")]
        [Description("Get to know about da server uwu")]
        public async Task ServerInfo(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("This is just a test server wdym?").ConfigureAwait(false);
        }

        [Command("rules")]
        [Description("Rules of da server uwu")]
        public async Task Rules(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Check out #Rules You dum dum").ConfigureAwait(false);
        }
    }
}
