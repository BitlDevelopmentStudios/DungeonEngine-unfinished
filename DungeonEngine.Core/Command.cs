/// <summary>
/// The core class related to commands.
/// </summary>
public class BaseCommand
{
    /// <summary>
    /// A String List of possible command names.
    /// </summary>
    public List<string> CommandNames = new List<string>();

    /// <summary>
    /// The input string.
    /// </summary>
    public string? Input { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="input"></param>
    public BaseCommand() {}

    /// <summary>
    /// Checks if the command is valid and related to this one, then calls it.
    /// </summary>
    virtual public bool InitalizeCommand()
    {
        if (CommandNames == null ||
            CommandNames.Count <= 0 ||
            string.IsNullOrWhiteSpace(Input))
        {
            return false;
        }

        foreach (string cmd in CommandNames)
        {
            if (Input.Contains(cmd, StringComparison.InvariantCultureIgnoreCase))
            {
                ExecuteCommand();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// The command execution itself.
    /// </summary>
    virtual public void ExecuteCommand()
    {
        //commands override this
    }
}
