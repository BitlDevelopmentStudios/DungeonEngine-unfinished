public class Game
{
    public bool GameShutdown = false;
    public static readonly string TitleFilePath = FileManagement.ResourcePath + @"title.txt";

    public Game(string[] args)
    {
        SetGameTitle();
        Init();
        Run();
    }

    public void Run()
    {
        while (!GameShutdown)
        {
            PreUpdate();
            Update();
            PostUpdate();
        }
    }

    public virtual void Init()
    {
        
    }

    public virtual void PreUpdate()
    {

    }

    public virtual void Update()
    {
    }

    public virtual void PostUpdate()
    {

    }

    public virtual void SetGameTitle(string additionalText = "")
    {
        SetGameTitle(TitleFilePath, additionalText);
    }

    public virtual void SetGameTitle(string fileName, string additionalText = "")
    {
        if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
        {
            Random rand = new Random();
            Console.Title = "Game #" + rand.Next(1, int.MaxValue);
            return;
        }

        string title = File.ReadAllText(fileName);

        if (!string.IsNullOrWhiteSpace(additionalText))
        {
            title += " | " + additionalText;
        }

        Console.Title = title;
    }
}
