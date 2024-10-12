using MigrationTool.Web.Constants;

namespace MigrationTool.Web;

public class DataParser
{
    private Dictionary<string, string> parserDataObject = [];

    public string? GetValue(string key) // Nullable sträng smidigare
    {
        if (parserDataObject.TryGetValue(key, out string? value))
            return value;

        return null;
    }

    public void SetValue(string key, string value)
    {
        parserDataObject[key] = value;
    }

    public void Execute()
    {
        string? firstName = GetValue(InputKeys.FirstName);
        string? lastName = GetValue(InputKeys.LastName);

        if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
        {
            SetValue(OutputKeys.FirstName, "Namn");
            SetValue(OutputKeys.LastName, "saknas");
        }
        else
        {
            SetValue(OutputKeys.FirstName, string.IsNullOrWhiteSpace(firstName) ? string.Empty: firstName);
            SetValue(OutputKeys.LastName, string.IsNullOrWhiteSpace(lastName) ? "Efternamn saknas" : lastName);
        }
    }
}