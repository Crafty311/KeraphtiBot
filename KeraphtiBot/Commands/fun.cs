using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using KeraphtiBot.Attributes;
using KeraphtiBot.Handlers.Dialogue;
using KeraphtiBot.Handlers.Dialogue.Steps;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using KeraphtiBot.Handlers;
using System.Collections.Generic;
using System.Linq;
using KeraphtiBot.handlers.Dialogue.DialogueHandler;




namespace KeraphtiBot.Commands
{
    public class Fun : BaseCommandModule
    {

        [Command("hallo")]
        [Description("Says Hallo to YOU")]
        [RequireRoles(RoleCheckMode.None)]
        [RequireCategories(ChannelCheckMode.Any, "Text Channels")]
        public async Task Hallo(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("HALLLOOOO").ConfigureAwait(false);

        }

        [Command("add")]
        [Description("Get gud at math")]
        public async Task Add(CommandContext ctx, [Description("First Number")] int numberOne, [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }
        [Command("random")]
        [Description("Get gud at math")]
        public async Task Randomize(CommandContext ctx, [Description("First Number")] int numberOne, [Description("Second Number")] int numberTwo)
        
        {
            Random r = new Random();
            int newRandom = r.Next(numberOne, numberTwo);
            {
                await ctx.Channel
                    .SendMessageAsync((newRandom).ToString())
                    .ConfigureAwait(false);
            }
        }
        [Command("say")]
        [Description("says what you say, yay")]
        public async Task Say(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("react")]
        [Description("reacts to what you say, yay")]
        public async Task React(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Message.GetReactionsAsync(message.Result.Emoji);
            await ctx.Message.CreateReactionAsync(message.Result.Emoji);
        }
        [Command("dialogue")]
        public async Task Dialogue(CommandContext ctx)
        {
            var inputStep = new TextStep("Enter something interesting!", null);
            var funnyStep = new IntStep("Gib A Number", null);

            string input = string.Empty;
            int input2 = 0;

            inputStep.OnValidResult += (result) =>
            {
                input = result;

                if (result == "interesting")
                {
                    inputStep.SetNextStep(funnyStep);
                }
            };
            
            funnyStep.OnValidResult += (result) => input2 = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);
           
            
            var inputDialogueHandler = new DHandler(ctx.Client, userChannel, ctx.User, inputStep);
            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }


            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(input2.ToString()).ConfigureAwait(false);
        }


    }
}