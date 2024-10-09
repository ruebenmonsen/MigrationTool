using MigrationTool.Web.Constants;

namespace MigrationTool.Web;

public class DataParser
{
    private Dictionary<string, string> parserDataObject = [];

    public object? GetValue(string key)
    {
        if (parserDataObject.TryGetValue(key, out string? value))
        {
            return value;
        }
        return null;
    }
    public void SetValue(string key, string value)
    {
        parserDataObject.Add(key, value);
    }
    public void Execute(Dictionary<string, string> parserDataObject)
    {
        bool hasFirstName = parserDataObject.TryGetValue(InputKeys.FirstName, out string? firstNameValue) &&
        !string.IsNullOrWhiteSpace(firstNameValue);

        bool hasLastName = parserDataObject.TryGetValue(InputKeys.LastName, out string? lastNameValue) &&
        !string.IsNullOrWhiteSpace(lastNameValue);

        if (!hasFirstName && !hasLastName)
        {
            parserDataObject[OutputKeys.FirstName] = "Namn";
            parserDataObject[OutputKeys.LastName] = "saknas";
        }
        else
        {
            parserDataObject[OutputKeys.FirstName] = hasFirstName ? firstNameValue! : string.Empty;
            parserDataObject[OutputKeys.LastName] = hasLastName ? lastNameValue! : "Efternamn saknas";
        }
    }
}