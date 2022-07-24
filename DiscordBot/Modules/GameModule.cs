using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class GameModule : ModuleBase<SocketCommandContext>
    {
       public static Game gameToPlayParent;
       [Group("Game")]
        public class GameModuel : ModuleBase<SocketCommandContext>
        {
            public Game gameToPlay ;
            [Command("RandomNumber")]
            [Summary("Gives a random number")]
            public async Task Random([Summary("The number to randomize to")] int max)
            {
                Random rand = new Random();
                // We can also access the channel from the Command Context.
                await Context.Channel.SendMessageAsync($"Random number is  " + rand.Next(max));
            }
            [Command("NewGame")]
            [Summary("Create a new entry by entry game ")]
            public async Task SetUpNewGame([Summary("The number to of cells in the new game")] int gridsize)
            {
                gameToPlayParent = new Game(gridsize);
                // We can also access the channel from the Command Context.
                await Context.Channel.SendMessageAsync($"New Game Set Up With Grid Size  " + gridsize);
                await Context.Channel.SendMessageAsync($"New Game Set Up As gameToPlayParent With Grid Size  " + gridsize);
            }
            [Command("New Unit")]
            [Summary("Create a new Unit On The Game Board ")]
            public async Task SetUpNewUnit([Summary("The x co-ord of the unit")] int x, [Summary("The y coord of the unit")] int y)
            {
                if(gameToPlayParent != null) {
                    gameToPlayParent.AddUnit("newUnit" + gameToPlayParent.units.Count(), x, y);
                    await Context.Channel.SendMessageAsync(String.Format(" New Unit Set up at {0},{1}",x,y));
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"No gmae set up. \n Use command \'NewGame [gridsize]\' to set up a game ");

                }
            }

            [Command("UnitXAttackUnitY")]
            [Summary("Get Unit to Attack another Unit")]
            public async Task AttackUnit([Summary("The index of the first unit")] int unit1, [Summary("The index of the second unit")] int unit2)
            {
                if (gameToPlayParent != null)
                {
                    if(gameToPlayParent.units.Count() > unit1 && gameToPlayParent.units.Count() > unit2)
                    {
                        if(gameToPlayParent.units[unit1].AttackUnit(gameToPlayParent.units[unit2].x, gameToPlayParent.units[unit2].y))
                        {
                            await Context.Channel.SendMessageAsync(String.Format("Unit {0} with name{1} has successfully attacked unit {2} with name {3} at position {4},{5}",unit1, gameToPlayParent.units[unit1].name, unit2, gameToPlayParent.units[unit2].name, gameToPlayParent.units[unit2].x, gameToPlayParent.units[unit2].y));

                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync(String.Format("Error attacking units"));

                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"Not enough units present in the game for the units listed in command. Total units is "+ gameToPlayParent.units.Count());

                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"No game set up. \n Use command \'NewGame [gridsize]\' to set up a game ");

                }
            }

            [Command("PrintGame")]
            [Summary("Print A Text Outline OF The Current Game")]
            public async Task PrintMap()
            {
                if (gameToPlayParent != null)
                {
                    string returnMessage = "";
                    for(int x =0; x < gameToPlayParent.cellLength; x++)
                    {

                        returnMessage += "[" + x + "]";
                        for (int y = 0; y < gameToPlayParent.cellLength; y++)
                        {
                            if (x == 0)
                            {

                                returnMessage += "\t["+ y+"]";
                            }
                            else
                            {

                                if (gameToPlayParent.cellGrid[x][y].unitOnCell != null)
                                {

                                    returnMessage += "\t[x]";
                                }
                                else
                                {

                                    returnMessage += "\t[ ]";
                                }
                            }
                        }
                        returnMessage += "\n";

                    }
                    await Context.Channel.SendMessageAsync(returnMessage);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"No game set up. \n Use command \'NewGame [gridsize]\' to set up a game ");

                }
                PrintMap(gameToPlayParent);
            }
            public async void PrintMap(Game gameToPrint)
            {
                if (gameToPrint != null)
                {
                    string returnMessage = "";
                    for (int x = 0; x < gameToPrint.cellLength; x++)
                    {

                        returnMessage += "[" + x + "]";
                        for (int y = 0; y < gameToPrint.cellLength; y++)
                        {
                            if (x == 0)
                            {

                                returnMessage += "\t[" + y + "]";
                            }
                            else
                            {

                                if (gameToPrint.cellGrid[x][y].unitOnCell != null)
                                {

                                    returnMessage += "\t[x]";
                                }
                                else
                                {

                                    returnMessage += "\t[ ]";
                                }
                            }
                        }
                        returnMessage += "\n";

                    }
                    await Context.Channel.SendMessageAsync(returnMessage);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"No game set up for fucntion game. \n Use command \'NewGame [gridsize]\' to set up a game ");

                }
            }
        }
    }
}

