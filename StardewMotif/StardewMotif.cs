using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace StardewMotif
{
    public class ModEntry : Mod
    {
        public string CurrentLocale { get; set; }
        public string LocalHeader { get; set; }

        public LocalizedContentManager.LanguageCode CurrentLocaleConstant => throw new NotImplementedException();

        public string ModID => throw new NotImplementedException();

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;

            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            helper.Events.Content.LocaleChanged += this.OnLocaleChange;
        }

        public void OnLocaleChange(object sender, LocaleChangedEventArgs e)
        {
            CurrentLocale = e.NewLocale;

            LocalHeader = CurrentLocale == "" ? "" : "." + CurrentLocale;
            //this.Monitor.Log($"Current Locale Header :" + LocalHeader, LogLevel.Debug);
        }

        //Json Names
        readonly string[] stringStringData = { "CookingRecipes", "EngagementDialogue", "ExtraDialogue",
            "FarmAnimals", "mail", "NPCDispositions", "NPCGiftTastes" };

        readonly string[] stringObjectData = { "ConcessionTastes"};

        readonly string[] intStringData = { "Furniture", "SecretNotes", "weapons", "Quests", "ObjectInformation" };

        readonly string[] events = { "Beach", "BusStop", "Farm", "FarmHouse", "Forest", "HaleyHouse", "SamHouse", "SienceHouse", "SebastianRoom", "Town" };

        readonly string[] festivals = { "spring13" };

        readonly string[] strings = { "Characters", "animationDescriptions", "Locations", "SpecialOrderStrings", "StringsFromCSFiles", "StringsFromMaps" };
        readonly string[] stringSchedules = { "Maru", "Sebastian" };

        readonly string[] dialogue = {"Leah","Abigail","Caroline","Demetrius","Jas","Jodi","Kent","LeoMainland","Lewis","MarriageDialogue",
            "MarriageDialogueAbigail","MarriageDialogueMaru","MarriageDialogueSam","MarriageDialogueSebastian","Pam","Penny","Pierre", "rainy","Robin","Sam","Sebastian","Vincent" };

        //Sprite Names
        readonly string[] looseSpritesPaths = { "logo", "emojis", "Concessions" };
        readonly string[] looseSpritesPathsLocalized = { "Billboard", "Cursors" };

        readonly string[] animals = { "BabySheep", "ShearedSheep", "Sheep" };


        public void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            ////Characters
            //Luny
            ReplaceCharacter(e, "Abigail", "Luny");

            //Mona
            ReplaceCharacter(e, "Leah", "Mona");

            //Fin
            ReplaceCharacter(e, "Sam", "Fin");

            //Jinja
            ReplaceCharacter(e, "Robin", "Jinja");

            //Laphi
            ReplaceCharacter(e, "Penny", "Laphi");

            //Misc Chars 
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Sebastian", "Characters/Dialogue/Sebastian", "json");
            ReplaceAsset<Dictionary<string, string>>(e, "Characters/Dialogue/Pierre", "Characters/Dialogue/Pierre", "json");

            ////General
            //Data
            ReplaceAsset<List<object>>(e, "Data/ConcessionTastes", "Data/ConcessionTastes", "json", false);

            ReplaceAssets<Dictionary<string, string>>(e, "Data/", stringStringData, "json");

            ReplaceAsset<List<StardewValley.GameData.Movies.MovieCharacterReaction>>(e, "Data/MoviesReactions", "Data/MoviesReactions", "json");

            ReplaceAssets<Dictionary<int, string>>(e, "Data/", intStringData, "json");

            //Festivals
            ReplaceAssets<Dictionary<string, string>>(e, "Data/Festivals/", festivals, "json");

            //Events
            ReplaceAssets<Dictionary<string, string>>(e, "Data/Events/", events, "json");

            //Character Dialogue
            ReplaceAssets<Dictionary<string, string>>(e, "Characters/Dialogue/", dialogue, "json");

            //Strings 
            ReplaceAssets<Dictionary<string, string>>(e, "Strings/", strings, "json");

            //String Schedules
            ReplaceAssets<Dictionary<string, string>>(e, "Strings/schedules/", stringSchedules, "json");

            //Loose Sprites
            ReplaceAssets<Texture2D>(e, "LooseSprites/", looseSpritesPaths, "png", false);
            ReplaceAssets<Texture2D>(e, "LooseSprites/", looseSpritesPathsLocalized, "png");

            //MiniGames
            ReplaceAsset<Texture2D>(e, "MiniGames/TitleButtons", "MiniGames/TitleButtons", "png");

            //Animals
            ReplaceAssets<Texture2D>(e, "Animals/", animals, "png", false);

            //Maps
            ReplaceAsset<Texture2D>(e, "Maps/springobjects", "Maps/SpringObjects", "png", false);

            ReplaceAsset<Texture2D>(e, "TileSheets/furniture", "TileSheets/furniture", "png", false);
        }

        private void ReplaceAssets<T>(AssetRequestedEventArgs e, string commonPath, string[] names, string type, bool localize = true)
        {
            foreach (string path in names)
            {
                ReplaceAsset<T>(e, commonPath + path, commonPath + path, type, localize);
            }
        }

        private void ReplaceAsset<T>(AssetRequestedEventArgs e, string replacePath, string assetPath, string type, bool localize = true)
        {
            //Check if it is the Asset we are looking for
            if (e.NameWithoutLocale.IsEquivalentTo(replacePath))
            {
                //Localized Asset
                //Monitor.Log($"Without: " + e.NameWithoutLocale + " With: " + e.Name, LogLevel.Debug);
                if (localize)
                {
                    e.LoadFromModFile<T>("assets/" + assetPath + LocalHeader + "." + type, AssetLoadPriority.Medium);
                }
                //Unlocalized Asset
                else
                {
                    e.LoadFromModFile<T>("assets/" + assetPath + "." + type, AssetLoadPriority.Medium);
                }
            }
        }

        private void ReplaceCharacter(AssetRequestedEventArgs e, string replaceChar, string newChar)
        {
            ReplaceAsset<Texture2D>(e, "Portraits/" + replaceChar, "Portraits/" + newChar, "png", false);
            ReplaceAsset<Texture2D>(e, "Characters/" + replaceChar, "Characters/" + newChar, "png", false);
            ReplaceAsset<Texture2D>(e, "Portraits/" + replaceChar + "_Beach", "Portraits/" + newChar + "_Beach", "png", false);
            ReplaceAsset<Texture2D>(e, "Characters/" + replaceChar + "_Beach", "Characters/" + newChar + "_Beach", "png", false);
        }

        string[] debugDialogueNPCs = {"Abigail", "Sam","Leah","Robin","Penny" };

        Task debugTask;
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            /*
            if (e.Button.ToString()[0] == 'K')
            {
                if((debugTask != null && debugTask.IsCompleted) || debugTask == null)
                {
                    debugTask = DebugDialogue("Robin");
                    this.Monitor.Log("Activated Debug", LogLevel.Debug);
                }
            }
            */
        }
        bool queueNextDialogue = false;
        public async Task DebugDialogue(string npcName)
        {
            this.Monitor.Log("Called", LogLevel.Debug);

            string fullPath = @"\Steam\steamapps\common\Stardew Valley\Mods\StardewMotif\assets\Characters\Dialogue\";
            string dialoguePath = @"\Characters\Dialogue\";
            dialoguePath += npcName;

            this.Monitor.Log("Dialogue Path: " + dialoguePath, LogLevel.Debug);
            NPC npc = Utility.fuzzyCharacterSearch(npcName, true);
            string json;
            try
            {
                json = File.ReadAllText(fullPath+ npcName + ".json");
                Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                foreach (string key in data.Keys)
                {
                    string dialogue = Game1.content.LoadStringReturnNullIfNotFound(dialoguePath + ":" + key);
                   
                    if(dialogue != null)
                    {
                        string[] split = dialogue.Split(new[] { "#$e#" }, StringSplitOptions.RemoveEmptyEntries);
                        
                        foreach (string s in split)
                        {
                            this.Monitor.Log("Showing dialogue: " + s, LogLevel.Debug);
                            Game1.drawDialogue(npc, s);
                            while (Game1.currentSpeaker != null)
                            {
                                await Task.Yield();
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                this.Monitor.Log("Except: " + e, LogLevel.Debug);
            }

            this.Monitor.Log("Dialogue Path: " + dialoguePath, LogLevel.Debug);
            
        }
    }
}
