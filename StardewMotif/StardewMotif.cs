using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace StardewMotif
{
    public class ModEntry : Mod
    {
        public string CurrentLocale => throw new NotImplementedException();

        public LocalizedContentManager.LanguageCode CurrentLocaleConstant => throw new NotImplementedException();

        public string ModID => throw new NotImplementedException();

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;

            helper.Events.Content.AssetRequested += this.OnAssetRequested;
        }
        public void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            ReplaceAsset<Texture2D>(e, "Portraits/Abigail", "Portraits/luny.png"); 
            ReplaceAsset<Texture2D>(e, "Characters/Abigail", "Characters/luny.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Abigail_Beach", "Portraits/luny_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Abigail_Beach", "Characters/luny_beach.png");

            ReplaceAsset<Texture2D>(e, "Portraits/Leah", "Portraits/mona.png");
            ReplaceAsset<Texture2D>(e, "Characters/Leah", "Characters/mona.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Leah_Beach", "Portraits/mona_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Leah_Beach", "Characters/mona_beach.png");

            ReplaceAsset<Texture2D>(e, "Portraits/Sam", "Portraits/fin.png"); 
            ReplaceAsset<Texture2D>(e, "Characters/Sam", "Characters/fin.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Sam_Beach", "Portraits/fin_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Sam_Beach", "Characters/fin_beach.png");

            ReplaceAsset<Dictionary<string, string>>(e, "Data/NPCDispositions", "Data/NPCDispositions.json");
        }

        private void ReplaceAsset<T>(AssetRequestedEventArgs e, string replacePath, string assetPath)       
        {
            if (e.NameWithoutLocale.IsEquivalentTo(replacePath))
            {
                e.LoadFromModFile<T>("assets/"+assetPath, AssetLoadPriority.Medium);
            }
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            //this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
    }
}
