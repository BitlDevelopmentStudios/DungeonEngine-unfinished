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

    public class Player : GameActor
    {
        public class Position
        {
            public int x;
            public int y;

            public Position()
            {
                x = 0;
                y = 0;
            }

            public Position(int xPos, int yPos)
            {
                x = xPos;
                y = yPos;
            }
        }

        public class GameUI
        {
            private Player daddy;

            public GameUI(Player player)
            {
                daddy = player;
            }
        }

        public enum Direction
        {
            None,
            Left,
            Right,
            Forward,
            Backward,
            Random
        }

        public enum MoveResult
        {
            Walk,
            Run
        }

        public string NameInput { get; set; } = "Player";
        public int Stamina { get; set; }
        public int MaxStamina { get; set; }

        public int MinRunSteps { get; set; }
        public int MaxRunSteps { get; set; }

        public int GoldCoins { get; set; } = 0;
        public int GoldBonusHP { get; set; }
        private bool HasGoldPowers { get; set; } = false;
        private Position CurLocation = new Position();
        private Direction CurDirection = Direction.None;

        public Player() : base(FileManagement.ActorPath + "player")
        {
            Stamina = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "Stamina").ToString());
            MaxStamina = Stamina;
            GoldBonusHP = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "GoldBonusHP").ToString());
            MinRunSteps = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "MinRunSteps").ToString());
            MaxRunSteps = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "MaxRunSteps").ToString());
        }

        public MoveResult Move(Direction newDirection)
        {
            CurDirection = newDirection;
            int walkStamina = (Convert.ToInt32(GlobalVars.CurDifficulty.LoadValue("PlayerWalkStamina")) * -1); // negative value
            int runStamina = (Convert.ToInt32(GlobalVars.CurDifficulty.LoadValue("PlayerRunStamina")) * -1);
            int runRegenMin = Convert.ToInt32(GlobalVars.CurDifficulty.LoadValue("PlayerRandomRunMin"));
            int runRegenMax = Convert.ToInt32(GlobalVars.CurDifficulty.LoadValue("PlayerRandomRunMax"));
            int runStaminaRegen = Convert.ToInt32(GlobalVars.CurDifficulty.LoadValue("PlayerRunStaminaRegen"));
            bool canBreatheInstantly = Convert.ToBoolean(GlobalVars.CurDifficulty.LoadValue("PlayerInstantRunBreathing"));
            MoveResult result = MoveResult.Walk;

            switch (CurDirection)
            {
                case Direction.Left:
                    CurLocation.x += 1;
                    StaminaEvent(walkStamina);
                    break;
                case Direction.Right:
                    CurLocation.x -= 1;
                    StaminaEvent(walkStamina);
                    break;
                case Direction.Forward:
                    CurLocation.y += 1;
                    StaminaEvent(walkStamina);
                    break;
                case Direction.Backward:
                    CurLocation.y -= 1;
                    StaminaEvent(walkStamina);
                    break;
                case Direction.Random:
                    {
                        Random rand = new Random();
                        CurLocation.x += rand.Next(MinRunSteps, MaxRunSteps);
                        CurLocation.y += rand.Next(MinRunSteps, MaxRunSteps);
                        StaminaEvent(runStamina);

                        if (canBreatheInstantly)
                        {
                            StaminaEvent(runStaminaRegen);
                        }
                        else
                        {
                            int regenRand = rand.Next(runRegenMin, runRegenMax);

                            if (regenRand == runRegenMax)
                            {
                                StaminaEvent(runStaminaRegen);
                            }
                        }

                        result = MoveResult.Run;
                    }
                    break;
                case Direction.None:
                default:
                    break;
            }

            if (CurLocation.x <= 0)
            {
                CurLocation.x = 0;
            }

            if (CurLocation.y <= 0)
            {
                CurLocation.y = 0;
            }

            return result;
        }

        public void UpdateName()
        {
            Name = NameInput +
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
