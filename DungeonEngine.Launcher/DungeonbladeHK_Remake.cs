namespace DungeonbladeHK_Remake
{
    public class DungeonbladeHK_Remake : Game
    {
        public DungeonbladeHK_Remake(string[] args) : base(args)
        {

        }

        public override void Init()
        {
            LoadLanguages();
            LoadDifficulties();

            // do player name here.
            Console.WriteLine("player name here");
            Console.ReadKey();
            Console.Clear();

            LoadStory();

            Console.ReadKey();
            GameShutdown = true;
        }

        void LoadLanguages(bool Error = false)
        {
            FileManagement.JsonFileList LanguageList = new FileManagement.JsonFileList(FileManagement.LanguagePath);
            List<Language> tempLangList = new List<Language>();

            foreach (string item in LanguageList.CleanedFileNames)
            {
                TaggedLanguage language = new TaggedLanguage(item);
                ConsoleEx.WriteColoredLine(language.Name + "/" + language.ShortName, ConsoleColor.Cyan);
                tempLangList.Add(language);
            }

            LangCommand langcmd = new LangCommand(tempLangList);
            ConsoleEx.SendCommand(langcmd, 
                "Enter Language:", 
                ConsoleColor.Green, 
                "Language not available.",
                ConsoleColor.Red,
                LoadLanguages,
                Error);
        }

        void LoadDifficulties(bool Error = false)
        {
            FileManagement.JsonFileList DifficultyList = new FileManagement.JsonFileList(FileManagement.DifficultyPath);
            List<Difficulty> tempDiffList = new List<Difficulty>();

            foreach (string item in DifficultyList.CleanedFileNames)
            {
                Difficulty difficulty = new Difficulty(item);
                ConsoleEx.WriteColoredLine(difficulty.Name + "/" + difficulty.ShortName, ConsoleColor.Cyan);
                tempDiffList.Add(difficulty);
            }

            DiffCommand diffcmd = new DiffCommand(tempDiffList);
            ConsoleEx.SendCommand(diffcmd, 
                "Difficulty_Select", 
                ConsoleColor.Green, 
                "Difficulty_Error",
                ConsoleColor.Red,
                LoadDifficulties, 
                Error);
        }

        void LoadStory(bool Error = false)
        {
            List<string> story = new List<string>();

            for (int i = 1; i < 19; i++)
            {
                story.Add(GlobalVars.LoadTranslatedString("Story_Line" + i));
            }

            story.Add(GlobalVars.LoadTranslatedString("Proceed_Prompt"));

            string storyRes = "";

            foreach (string item in story)
            {
                storyRes = string.Join("\n", story.ToArray());
            }

            ProceedCommand proceed = new ProceedCommand();
            ConsoleEx.SendCommand(proceed,
               storyRes,
               ConsoleColor.DarkCyan,
               "Proceed_Error",
               ConsoleColor.Red,
               LoadStory,
               Error);

            if (GameVars.proceed && !Error)
            {
                Console.Clear();
                for (int i = 1; i < 4; i++)
                {
                    ConsoleEx.WriteColoredLineTranslated("Proceed_Line" + i, ConsoleColor.DarkCyan);
                }

                //so we can use this for other things.
                GameVars.proceed = false;
            }
            else
            {
                Console.Clear();
                for (int i = 1; i < 5; i++)
                {
                    ConsoleEx.WriteColoredLineTranslated("GoHome_Line" + i, ConsoleColor.Red);
                }
            }
        }
    }
}
