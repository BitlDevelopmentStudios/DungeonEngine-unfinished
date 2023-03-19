public static class ConsoleEx
{
    public static void SendCommand(BaseCommand cmd, string commandPrompt = "Command_Prompt", ConsoleColor promptColor = ConsoleColor.Green, string errorMessage = "Invalid_Command", ConsoleColor errorColor = ConsoleColor.Red, Action<bool> ?restartAction = null, bool ShowErrorMessage = false)
    {
        if (ShowErrorMessage)
        {
            WriteColoredLineTranslated(errorMessage, errorColor);
        }

        WriteColoredLineTranslated(commandPrompt, promptColor);
        bool success = true;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string command = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        cmd.Input = command;
        if (!cmd.InitalizeCommand())
        {
            success = false;
        }

        Console.Clear();

        if (!success)
        {
            if (restartAction != null)
            {
                restartAction.Invoke(true);
            }
        }
        else
        {
            return;
        }
    }

    public static void WriteColoredTranslated(string? value = "", ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        WriteColored(GlobalVars.LoadTranslatedString(value), color);
    }

    public static void WriteColoredLineTranslated(string? value = "", ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        WriteColoredLine(GlobalVars.LoadTranslatedString(value), color);
    }

    public static void WriteColored(string? value = "", ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        Console.ForegroundColor = color;
        Console.Write(value);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void WriteColoredLine(string? value = "", ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        Console.ForegroundColor = color;
        Console.WriteLine(value);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
