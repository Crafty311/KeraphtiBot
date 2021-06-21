using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KeraphtiBot
{
    class Program
    {
        private static void Main(string[] args) 
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();

        }
    }
}