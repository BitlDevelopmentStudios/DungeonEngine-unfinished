using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DungeonbladeHK_Remake
{
    public class GameActor : BaseActor
    {
        public JsonElement Attachments { get; }

        public GameActor(string fileName) : base(fileName)
        {
            Attachments = FileManagement.LoadElementProperty(Attributes, "Attachments");
        }
    }

    public class PlayerPosition
    {
        public int x;
        public int y;

        public PlayerPosition()
        {
            x = 0;
            y = 0;
        }

        public PlayerPosition(int xPos, int yPos)
        {
            x = xPos;
            y = yPos;
        }
    }

    public class Player : GameActor
    {
        public int Stamina { get; set; }
        public int MaxStamina { get; set; }

        public int GoldCoins { get; set; } = 0;
        public int GoldBonusHP { get; set; }
        private bool HasGoldPowers { get; set; } = false;
        private PlayerPosition CurLocation = new PlayerPosition();

        public Player() : base(FileManagement.ActorPath + "player")
        {
            Stamina = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "Stamina").ToString());
            MaxStamina = Stamina;
            GoldBonusHP = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "GoldBonusHP").ToString());
        }

        public void UpdateLocation(int x, int y)
        {
            CurLocation.x = x;
            CurLocation.y = y;
        }

        public void UpdateName()
        {
            GameVars.playerFullName = GameVars.playerName +
                (HasGoldPowers ? " " + GlobalVars.LoadTranslatedString("Player_GoldTitle") : "") +
                (Stamina <= 0 ? " " + GlobalVars.LoadTranslatedString("Player_StaminaDepleted") : "");
        }

        public void StaminaEvent(int value)
        {
            Stamina += value;

            if (Stamina <= 0)
            {
                Stamina = 0;
            }

            if (Stamina >= MaxStamina)
            {
                Stamina = MaxStamina;
            }

            UpdateName();
        }

        public void MaxStaminaEvent(int value)
        {
            MaxStamina += value;

            if (MaxStamina <= 0)
            {
                Stamina = 0;
                MaxStamina = 0;
            }

            if (MaxStamina >= Stamina)
            {
                Stamina = MaxStamina;
            }

            UpdateName();
        }

        public void GoldUpgrade()
        {
            HasGoldPowers = true;
            MaxHP = MaxHP + GoldBonusHP;
            HP = MaxHP;

            UpdateName();
        }
    }
}
