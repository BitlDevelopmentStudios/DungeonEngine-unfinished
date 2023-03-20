using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

public class DiffCommand : BaseCommand
{
    List<Difficulty> diffList = new List<Difficulty>();

    public DiffCommand(List<Difficulty> difficulties) : base()
    {
        diffList = difficulties;

        foreach (Difficulty diff in diffList)
        {
            CommandNames.Add(diff.Name);
            CommandNames.Add(diff.ShortName);
        }
    }

    public override bool InitalizeCommand()
    {
        if (GlobalVars.CurDifficulty != null)
        {
            return false;
        }

        return base.InitalizeCommand();
    }

    public override void ExecuteCommand()
    {
        if (string.IsNullOrWhiteSpace(Input))
            return;

        foreach (Difficulty diff in diffList)
        {
            if (Input.Contains(diff.ShortName, StringComparison.InvariantCultureIgnoreCase))
            {
                GlobalVars.CurDifficulty = diff;
                break;
            }
        }
    }
}

public class Difficulty
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Desc { get; set; }
    public JsonElement Adjustments { get; }
    public JsonElement Root { get; }

    public Difficulty(string fileName)
    {
        string file = File.ReadAllText(FileManagement.GenerateJSONFilePath(FileManagement.DifficultyPath + fileName));
        JsonDocument doc = JsonDocument.Parse(file);
        Root = doc.RootElement;
        Name = FileManagement.LoadTranslatedElementString(Root, "Name").ToString();
        ShortName = FileManagement.LoadTranslatedElementString(Root, "ShortName").ToString();
        Desc = FileManagement.LoadTranslatedElementString(Root, "Desc").ToString();
        Adjustments = FileManagement.LoadElementProperty(Root, "Adjustments");
    }

    public string LoadValue(string valueName)
    {
        return FileManagement.LoadElementProperty(Adjustments, valueName).ToString();
    }
}
