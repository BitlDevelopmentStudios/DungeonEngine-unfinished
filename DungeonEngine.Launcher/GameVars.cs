using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonbladeHK_Remake
{
    public class GameVars
    {
        public static readonly string EnemyPath = FileManagement.ActorPath + @"enemies\";
        public static readonly string ItemsPath = FileManagement.ActorPath + @"items\";
        public static readonly string LocationsPath = FileManagement.ActorPath + @"locations\";

        public static bool proceed = false;
        public static Player? gamePlayer = null;
    }
}
