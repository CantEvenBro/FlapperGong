using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    // Create a module with the 'sample' prefix
    //[Group("Hello")]
    public class HelloModule : ModuleBase<SocketCommandContext>
    {
        [Command("From")]
        [Summary("Echoes a reply to a given name.")]
        public async Task Hello([Summary("The name to say hello to")] string name)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"Hello "+name);
        }
    }
    
}
