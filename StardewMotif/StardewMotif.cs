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
            ////Characters
            //Luny
            ReplaceAsset<Texture2D>(e, "Portraits/Abigail", "Portraits/luny.png"); 
            ReplaceAsset<Texture2D>(e, "Characters/Abigail", "Characters/luny.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Abigail_Beach", "Portraits/luny_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Abigail_Beach", "Characters/luny_beach.png");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Abigail", "Characters/Dialogue/Luny.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/MarriageDialogueAbigail", "Characters/Dialogue/MarriageDialogueLuny.json");

            //Mona
            ReplaceAsset<Texture2D>(e, "Portraits/Leah", "Portraits/mona.png");
            ReplaceAsset<Texture2D>(e, "Characters/Leah", "Characters/mona.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Leah_Beach", "Portraits/mona_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Leah_Beach", "Characters/mona_beach.png");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Leah", "Characters/Dialogue/Mona.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/MarriageDialogueLeah", "Characters/Dialogue/MarriageDialogueMona.json");

            //Fin
            ReplaceAsset<Texture2D>(e, "Portraits/Sam", "Portraits/fin.png"); 
            ReplaceAsset<Texture2D>(e, "Characters/Sam", "Characters/fin.png");
            ReplaceAsset<Texture2D>(e, "Portraits/Sam_Beach", "Portraits/fin_beach.png");
            ReplaceAsset<Texture2D>(e, "Characters/Sam_Beach", "Characters/fin_beach.png");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Sam", "Characters/Dialogue/Fin.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/MarriageDialogueSam", "Characters/Dialogue/MarriageDialogueFin.json");

            //Misc Chars 
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Sebastian", "Characters/Dialogue/Sebastian.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Pierre", "Characters/Dialogue/Pierre.json");


            ////General
            //Data
            ReplaceAsset<Dictionary<string, string>>(e, "Data/NPCDispositions", "Data/NPCDispositions.json");
            ReplaceAsset<Dictionary<int, string>>(e, "Data/SecretNotes", "Data/SecretNotes.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/EngagementDialogue", "Data/EngagementDialogue.json");
            ReplaceAsset<Dictionary<int, string>>(e, "Data/weapons", "Data/weapons.json");
            ReplaceAsset<Dictionary<int, string>>(e, "Data/Quests", "Data/Quests.json");
            ReplaceAsset<Dictionary<int, string>>(e, "Data/ObjectInformation", "Data/ObjectInformation.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/mail", "Data/mail.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/FarmAnimals", "Data/FarmAnimals.json");

            //Events
            ReplaceAsset<Dictionary<string, string>>(e, "Data/Events/Beach", "Data/Events/Beach.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/Events/Town", "Data/Events/Town.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/Events/Forest", "Data/Events/Forest.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/Events/SamHouse", "Data/Events/SamHouse.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Data/Events/SebastianRoom", "Data/Events/SebastianRoom.json");

            //Character Dialogue
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/rainy", "Characters/Dialogue/rainy.json");

            //Strings 
            ReplaceAsset<Dictionary<string, string>>(e, "Strings/Characters", "Strings/Characters.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Strings/animationDescriptions", "Strings/animationDescriptions.json");
            ReplaceAsset<Dictionary<string, string>>(e, "Strings/Locations", "Strings/Locations.json");

            //Loose Sprites
            ReplaceAsset<Texture2D>(e, "LooseSprites/logo", "LooseSprites/logo.png");
            ReplaceAsset<Texture2D>(e, "LooseSprites/emojis", "LooseSprites/emojis.png"); 
            ReplaceAsset<Texture2D>(e, "LooseSprites/Concessions", "LooseSprites/Concessions.png");
            ReplaceAsset<Texture2D>(e, "LooseSprites/Billboard", "LooseSprites/Billboard.png");
            ReplaceAsset<Texture2D>(e, "LooseSprites/Cursors", "LooseSprites/Cursors.png");

            //MiniGames
            ReplaceAsset<Texture2D>(e, "MiniGames/TitleButtons", "MiniGames/TitleButtons.png");

            //Animals
            ReplaceAsset<Texture2D>(e, "Animals/BabySheep", "Animals/BabyWoo.png");
            ReplaceAsset<Texture2D>(e, "Animals/ShearedSheep", "Animals/ShearedWoo.png");
            ReplaceAsset<Texture2D>(e, "Animals/Sheep", "Animals/Woo.png");

            //Maps
            ReplaceAsset<Texture2D>(e, "Maps/springobjects", "Maps/SpringObjects.png");

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
