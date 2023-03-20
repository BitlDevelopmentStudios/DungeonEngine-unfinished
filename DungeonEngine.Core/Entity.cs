using System.Text.Json;

/// <summary>
/// The base object for loading JSON-based enemies, items and such
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// The name we are assigning the entity.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The description for the entity.
    /// </summary>
    public string Desc { get; set; }
    /// <summary>
    /// Allows inherited classes to use any value from here.
    /// </summary>
    public JsonElement Attributes { get; }
    /// <summary>
    /// The root of the entity's JSON file.
    /// </summary>
    public JsonElement Root { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName"></param>
    public BaseEntity(string fileName)
    {
        string file = File.ReadAllText(FileManagement.GenerateJSONFilePath(fileName));
        JsonDocument doc = JsonDocument.Parse(file);
        Root = doc.RootElement;
        Name = FileManagement.LoadTranslatedElementString(Root, "Name").ToString();
        Desc = FileManagement.LoadTranslatedElementString(Root, "Desc").ToString();
        Attributes = FileManagement.LoadElementProperty(Root, "Attributes");
    }
}

/// <summary>
/// The shared BaseEntity for players and enemies. Manages health.
/// </summary>
public class BaseActor : BaseEntity
{
    /// <summary>
    /// Current Hitpoints/Health.
    /// </summary>
    public int HP { get; set; }
    /// <summary>
    /// Maximum Hitpoints/Health.
    /// </summary>
    public int MaxHP { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName"></param>
    public BaseActor(string fileName) : base(fileName)
    {
        HP = Convert.ToInt32(FileManagement.LoadElementProperty(Attributes, "HP").ToString());
        MaxHP = HP;
    }

    /// <summary>
    /// Adjusts the main health value. 
    /// </summary>
    /// <param name="value"></param>
    virtual public void HealthEvent(int value)
    {
        HP += value;

        if (HP <= 0)
        {
            HP = 0;
        }

        if (HP >= MaxHP)
        {
            HP = MaxHP;
        }
    }

    /// <summary>
    /// Adjusts the maximum health value. Good for upgrades/powerups.
    /// </summary>
    /// <param name="value"></param>
    virtual public void MaxHealthEvent(int value)
    {
        MaxHP += value;

        if (MaxHP <= 0)
        {
            HP = 0;
            MaxHP = 0;
        }

        if (MaxHP >= HP)
        {
            HP = MaxHP;
        }
    }

    /// <summary>
    /// Kills Jim.
    /// </summary>
    /// <returns></returns>
    public void InstantKill()
    {
        HP = 0;
    }

    /// <summary>
    /// He's dead, Jim.
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return (HP <= 0);
    }

    /// <summary>
    /// What happens when we die.
    /// </summary>
    virtual public void DeathEvent()
    {
        // event here
    }
}

