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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public static string BasePath = AppDomain.CurrentDomain.BaseDirectory + Assembly.GetEntryAssembly().GetName().Name + @"\";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    public static string ScriptPath = BasePath + @"scripts\";
    public static string ResourcePath = BasePath + @"resource\";
    public static string DifficultyPath = ScriptPath + @"difficulties\";
    public static string LanguagePath = ResourcePath + @"languages\";
    public static string EntityPath = ScriptPath + @"entities\";
    public static string ActorPath = EntityPath + @"actors\";

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
