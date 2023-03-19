using System.Reflection;
using System.Text.Json;

public static class GlobalVars
{
    public static Language? CurLanguage;
    public static Difficulty? CurDifficulty;

    public static string LoadTranslatedString(string untranslated)
    {
        if (CurLanguage != null)
        {
            string result = CurLanguage.TranslateString(untranslated);
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                return untranslated;
            }
        }
        else
        {
            return untranslated;
        }
    }
}
