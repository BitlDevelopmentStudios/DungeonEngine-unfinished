using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonbladeHK_Remake
{
    public class TaggedLanguage : Language
    {
        public TaggedLanguage(string fileName) : base(fileName) { }

        public string ParseTags(string str)
        {
            //ADD Replace FOR TAGS
            return str.Replace("%playerName%", GameVars.playerFullName);
        }

        public override string TranslateString(string propertyName)
        {
            return ParseTags(base.TranslateString(propertyName));
        }
    }
}
