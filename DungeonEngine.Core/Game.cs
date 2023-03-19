public class Game
{
    public bool GameShutdown { get; set; } = false;

    public Game(string[] args)
    {
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
}
