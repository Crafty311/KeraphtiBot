using System.Reflection.Emit;
using System.Security.Cryptography;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace KeraphtiBot.Handlers.Dialogue.Steps
{
    public class IntStep : DialogueStepBase
    {
        private IDialogueStep _nextStep;
        private readonly int? _minValue;
        private readonly int? _maxValue;

        public IntStep(
            string content,
            IDialogueStep nextStep,
            int? minValue = null,
            int? maxValue = null) : base(content)
        {
            _nextStep = nextStep;
            _minValue = minValue;
            _maxValue = maxValue;
        }

       public Action<int> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public void SetNextStep(IDialogueStep nextStep)
        {
            _nextStep = nextStep;
        }

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = $"Please Respond Below",
                Description = $"{user.Mention}, {_content}",
                Color = DiscordColor.Blue
            };

            embedBuilder.AddField("To Stop The Dialogue", "Use the ?cancel command");

            if (_minValue.HasValue)
            {
                embedBuilder.AddField("Min Value:", $"{_minValue.Value} characters");
            }
            if (_maxValue.HasValue)
            {
                embedBuilder.AddField("Max Value:", $"{_maxValue.Value} characters");
            }

            var interactivity = client.GetInteractivity();

            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                } 
                if(!int.TryParse (messageResult.Result.Content, out int inputValue))
                {
                        await TryAgain(channel, $"Your input is not an integer dum dum").ConfigureAwait(false);
                        continue;
                    }
                if (inputValue < _minValue.Value)
                {
                    if (messageResult.Result.Content.Length < _minValue.Value)
                    {
                        await TryAgain(channel, $"Your input is {inputValue} is too smol").ConfigureAwait(false);
                        continue;
                    }
                }
                if (inputValue > _maxValue.Value)
                {
                    if (messageResult.Result.Content.Length > _maxValue.Value)
                    {
                        await TryAgain(channel, $"Your input is {inputValue} is too big (Max Value = {_maxValue})").ConfigureAwait(false);
                        continue;
                    }
                }
                OnValidResult(inputValue);

                return true;
            }
        }
    }
}