
using System;
using Modding;

namespace Moonside
{
    public class Moonside : Mod
    {
        new public string GetName() => "Moonside";
        public override string GetVersion() => "1.0.0.0";
        public override void Initialize()
        {
            ModHooks.LanguageGetHook += NoMeansYes;
        }

        private string NoMeansYes(string key, string sheetTitle, string orig)
        {
            if (key == "ELDERBUG_INTRO_NORMAL" && sheetTitle == "Elderbug")
            {
                return "\"Yes\" is \"No\" and \"No\" is \"Yes.\" It makes perfect sense in Moonside.";
            }
            if (sheetTitle == "Stag")
            {
                return "Hello! And... good-bye... Shall I...?";
            }
            if (key == "BIGCAT_INTRO")
            {
                return "Do you know whose bones are on display here? The answer is... your bones. My bones. Bone's bones. Bone bone bone.";
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
            if (sheetTitle == "Sly")
            {
                return "No, that's right. I am the host here.";
            }
            if (sheetTitle == "Iselda")
            {
                return "Yes, that's wrong. I am the hostess here.";
            }
            if (sheetTitle == "Charm Slug")
            {
                return "Welcome to Moonside. Wel Come to moo nsi ns dem oons ide.";
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

            orig = orig.Replace("Radiance", "Mani Mani");
            return orig;
        }
    }   
}