using System.Reflection;
using System.Text.Json;

public class FileManagement
{
    public class JsonFileList
    {
        public List<string> FileNames = new List<string>();
        public List<string> CleanedFileNames = new List<string>();

        public JsonFileList(string folderPath)
        {
            foreach (string fileName in Directory.GetFiles(folderPath, "*.json"))
            {
                FileNames.Add(fileName);
            }

            foreach (string fileName in FileNames)
            {
                CleanedFileNames.Add(Path.GetFileNameWithoutExtension(fileName));
            }
        }
    }

    public static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory + Assembly.GetEntryAssembly().GetName().Name + @"\";
    public static readonly string ScriptPath = BasePath + @"scripts\";
    public static readonly string ResourcePath = BasePath + @"resource\";
    public static readonly string DifficultyPath = ScriptPath + @"difficulties\";
    public static readonly string LanguagePath = ResourcePath + @"languages\";
    public static readonly string EntityPath = ScriptPath + @"entities\";
    public static readonly string ActorPath = EntityPath + @"actors\";

    public static string GenerateJSONFilePath(string fileName)
    {
        return fileName + ".json";
    }

    public static JsonElement LoadElementProperty(JsonElement element, string name)
    {
        JsonElement result;
        element.TryGetProperty(name, out result);
        return result;
    }

    public static string LoadTranslatedElementString(JsonElement element, string name)
    {
        JsonElement untranslated = LoadElementProperty(element, name);
        return GlobalVars.LoadTranslatedString(untranslated.ToString());
    }
}
