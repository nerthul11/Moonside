
using System;
using System.Collections.Generic;
using System.Linq;
using Modding;

namespace Moonside
{
    public class Moonside : Mod, IMenuMod
    {
        public bool ToggleButtonInsideMenu => false;
        public bool Dialogues;
        public bool Extra;
        new public string GetName() => "Moonside";
        public override string GetVersion() => "1.1.0.0";
        public override void Initialize()
        {
            ModHooks.LanguageGetHook += NoMeansYes;
        }

        public override int LoadPriority() => 4444;
        private string NoMeansYes(string key, string sheetTitle, string orig)
        {
            if (orig.All(char.IsDigit))
                return orig;

            if (Dialogues)
            {            
                if (key == "ELDERBUG_INTRO_NORMAL" && sheetTitle == "Elderbug")
                {
                    return "\"Yes\" is \"No\" and \"No\" is \"Yes.\" It makes perfect sense in Moonside.";
                }
                if (sheetTitle == "Stag" && key == "STAG_END_SPEAK")
                {
                    return "Hello! And... good-bye... Shall I...?";
                }
                if (key == "BIGCAT_INTRO")
                {
                    return "Do you know whose bones are on display here? The answer is... <page>Your bones. My bones. Bone's bones. <page>Bone bone bone.";
                }
                if (key.Contains("MOSS_CULTIST") && sheetTitle == "Minor NPC")
                {
                    return "Before the soup gets cold, we must care for Mani Mani. Before the knife gets rusty, we must care for Mani Mani.";
                }
                if (key.Contains("MINER_EARLY") && sheetTitle == "Minor NPC")
                {
                    return "Good morn... Uhhh... not morning. Here in Moonside, it's always the middle of the night. This is a headline from tonight's Moonside Press... \"Mani Mani is  always Mani Mani  at Mani Mani with all Mani Mani  Mani\"";
                }
                if (key == "MINER_INFECTED" && sheetTitle == "Minor NPC")
                {
                    return "How about I sharpen you? I just love sharpening. You don't want me to sharpen? Sidem oonsi demoon. Welc welc omewelc omeome.";
                }
                if (sheetTitle == "Charm Slug" && key == "CHARMSLUG_REPEAT")
                {
                    return "Welcome to Moonside. Wel Come to moo nsi ns dem oons ide.";
                }

                orig = orig.Replace("Radiance", "Mani Mani");
                orig = orig.Replace("RADIANCE", "MANI MANI");
            }

            string placeholder = "MOONSIDE";
            string placeholder2 = "SOONMIDE";
            orig = orig.Replace("yes", placeholder);
            orig = orig.Replace("no", placeholder2);
            orig = orig.Replace(placeholder, "no");
            orig = orig.Replace(placeholder2, "yes");            

            orig = orig.Replace("No", placeholder);
            orig = orig.Replace("Yes", placeholder2);
            orig = orig.Replace(placeholder, "Yes");
            orig = orig.Replace(placeholder2, "No");

            orig = orig.Replace("YES", placeholder);
            orig = orig.Replace("NO", placeholder2);
            orig = orig.Replace(placeholder, "NO");
            orig = orig.Replace(placeholder2, "YES");   

            

            if (Extra)
            {
                Random randomizer = new();
                int scramble = randomizer.Next(4, 44);
                if (scramble == 4)
                {
                    orig = Shuffle(orig);
                }
                else if (scramble == 40)
                {
                    orig = Reverse(orig);
                }
            }
            return orig;
        }

        public static string Shuffle(string orig)
        {
            List<string> chars = Tokenize(orig);
            List<string> shuffled = chars.OrderBy(c => Guid.NewGuid()).ToList();
            return string.Concat(shuffled);
        }

        public static string Reverse(string orig)
        {
            List<string> tokens = Tokenize(orig);
            tokens.Reverse();
            return string.Concat(tokens);
        }

        // Helper function to parse into regular characters and "<...>" chunks
        private static List<string> Tokenize(string input)
        {
            var tokens = new List<string>();
            int i = 0;

            while (i < input.Length)
            {
                // Check if a '<' starts a chunk
                if (input[i] == '<')
                {
                    int endIndex = input.IndexOf('>', i);
                    if (endIndex != -1)
                    {
                        // Extract the chunk including '<' and '>'
                        tokens.Add(input.Substring(i, endIndex - i + 1));
                        i = endIndex + 1; // Move past the '>'
                        continue;
                    }
                }

                // Otherwise, treat it as a normal character
                tokens.Add(input[i].ToString());
                i++;
            }

            return tokens;
        }

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            return
            [
                new IMenuMod.MenuEntry("Custom Dialogues", ["Disabled", "Enabled"], "Use a few NPC custom dialogues.", opt => Dialogues = opt == 1, () => Dialogues ? 1 : 0),
                new IMenuMod.MenuEntry("Easter Egg", ["Disabled", "Enabled"], "Further mess up with text.", opt => Extra = opt == 1, () => Extra ? 1 : 0)
            ];
        }
    }   
}