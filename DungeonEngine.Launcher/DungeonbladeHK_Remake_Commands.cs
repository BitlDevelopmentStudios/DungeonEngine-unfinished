using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonbladeHK_Remake
{
    public class ProceedCommand : BaseCommand
    {
        public ProceedCommand() : base()
        {
            CommandNames.Add(GlobalVars.LoadTranslatedString("Proceed_Yes1"));
            CommandNames.Add(GlobalVars.LoadTranslatedString("Proceed_Yes2"));
            CommandNames.Add(GlobalVars.LoadTranslatedString("Proceed_No1"));
            CommandNames.Add(GlobalVars.LoadTranslatedString("Proceed_No2"));
        }

        public override void ExecuteCommand()
        {
            if (string.IsNullOrWhiteSpace(Input))
                return;

            if (Input.Contains(GlobalVars.LoadTranslatedString("Proceed_Yes1"), StringComparison.InvariantCultureIgnoreCase) ||
                Input.Contains(GlobalVars.LoadTranslatedString("Proceed_Yes2"), StringComparison.InvariantCultureIgnoreCase))
            {
                GameVars.proceed = true;
            }
            else if (Input.Contains(GlobalVars.LoadTranslatedString("Proceed_No1"), StringComparison.InvariantCultureIgnoreCase) ||
                Input.Contains(GlobalVars.LoadTranslatedString("Proceed_No2"), StringComparison.InvariantCultureIgnoreCase))
            {
                GameVars.proceed = false;
            }
        }
    }

    public class PlayerNameCommand : BaseCommand
    {
        public PlayerNameCommand() : base()
        {
            NoCommandNames = true;
        }

        public override void ExecuteCommand()
        {
            if (string.IsNullOrWhiteSpace(Input) || Input.Length < 2)
                return;

            GameVars.playerName = Input;
            GameVars.playerFullName = GameVars.playerName;
        }
    }
}
