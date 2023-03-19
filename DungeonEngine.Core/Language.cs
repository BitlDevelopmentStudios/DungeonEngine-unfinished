using System.Text.Json;

public class LangCommand : BaseCommand
{
    List<Language> langList = new List<Language>();

    public LangCommand(List<Language> languages) : base()
    {
        langList = languages;

        foreach (Language lang in langList)
        {
            CommandNames.Add(lang.Name);
            CommandNames.Add(lang.ShortName);
        }
    }

    public override bool InitalizeCommand()
    {
        if (GlobalVars.CurLanguage != null)
        {
            return false;
        }

        return base.InitalizeCommand();
    }

    public override void ExecuteCommand()
    {
        if (string.IsNullOrWhiteSpace(Input))
            return;

        foreach (Language lang in langList)
        {
            if (Input.Contains(lang.ShortName, StringComparison.InvariantCultureIgnoreCase))
            {
                GlobalVars.CurLanguage = lang;
                break;
            }
        }
    }
}

public class Language
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public JsonElement Values { get; }
    public JsonElement Root { get; }

    public Language(string fileName)
    {
        string file = File.ReadAllText(FileManagement.GenerateJSONFilePath(FileManagement.LanguagePath + fileName));
        JsonDocument doc = JsonDocument.Parse(file);
        Root = doc.RootElement;
        Name = FileManagement.LoadElementProperty(Root, "Name").ToString();
        ShortName = FileManagement.LoadElementProperty(Root, "ShortName").ToString();
        Values = FileManagement.LoadElementProperty(Root, "Values");
    }

    public virtual string TranslateString(string propertyName)
    {
        JsonElement jsonResult = FileManagement.LoadElementProperty(Values, propertyName);
        string result = jsonResult.ToString();
        return result;
    }
}


