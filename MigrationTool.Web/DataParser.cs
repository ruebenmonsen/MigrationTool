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
        string firstNameKeyIn = "p:namn/p:fornamn";
        string lastNameKeyIn = "p:namn/p:efternamn";
        string firstNameKeyOut = "Namn.fornamn";
        string lastNameKeyOut = "Namn.efternamn";

        bool hasFirstName = parserDataObject.TryGetValue(firstNameKeyIn, out string? firstNameValue) &&
        !string.IsNullOrWhiteSpace(firstNameValue);

        bool hasLastName = parserDataObject.TryGetValue(lastNameKeyIn, out string? lastNameValue) &&
        !string.IsNullOrWhiteSpace(lastNameValue);

        if (!hasFirstName && !hasLastName)
        {
            parserDataObject[firstNameKeyOut] = "Namn";
            parserDataObject[lastNameKeyOut] = "saknas";
            return;
        }

        // no first name but last name exists
        if (!hasFirstName && hasLastName)
        {
            parserDataObject[firstNameKeyOut] = string.Empty;
            parserDataObject[lastNameKeyOut] = lastNameValue!;
            return;
        }

        // no last name but first name exists
        if (hasFirstName && !hasLastName)
        {
            parserDataObject[firstNameKeyOut] = firstNameValue!;
            parserDataObject[lastNameKeyOut] = "Efternamn saknas";
            return;
        }

        // both first name and last name exists, use existing values
        if (hasFirstName && hasLastName)
        {
            parserDataObject[firstNameKeyOut] = firstNameValue!;
            parserDataObject[lastNameKeyOut] = lastNameValue!;
            return;
        }
    }
}