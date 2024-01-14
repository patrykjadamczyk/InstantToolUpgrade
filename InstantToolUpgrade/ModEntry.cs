namespace InstantToolUpgrade;

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        // Any time the player submits a tool to be upgraded...
        if (Game1.player.daysLeftForToolUpgrade.Value > 0)
        {
            // Check to see if it's a genericTool...
            if (Game1.player.toolBeingUpgraded.Value is StardewValley.Tools.GenericTool genericTool)
            {
                // ...and upgrade it.
                genericTool.actionWhenClaimed();
            }
            else
            {
                // Otherwise give the player their new tool.
                Game1.player.addItemToInventory(Game1.player.toolBeingUpgraded.Value);                    
            }
                
            // Finally, cancel the queued upgrade.
            Game1.player.toolBeingUpgraded.Value = null;
            Game1.player.daysLeftForToolUpgrade.Value = 0;
        }
    }
}

