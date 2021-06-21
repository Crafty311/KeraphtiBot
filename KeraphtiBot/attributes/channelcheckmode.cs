using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace KeraphtiBot.Attributes
{
       public enum ChannelCheckMode
        {
            Any = 0,
            None = 1,
            MineOrParentAny = 2
        }
}