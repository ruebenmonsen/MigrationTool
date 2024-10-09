// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
using MigrationTool.Web;
using MigrationTool.Web.Constants;

namespace MigrationTool.Tests;

public class DataParserTests
{
    // Om varken förnamn eller efternamn finns i ursprungsdatat ska förnamn bli "Namn" och efternamn bli "saknas".
    // Case A: Nycklarna finns inte
    [Fact]
    public void ParseData_MissingInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>();
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal("Namn", parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("saknas", parserDataObject[OutputKeys.LastName]);
    }
    // Case B: Nycklarna finns, men null
    [Fact]
    public void ParseData_NullInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>
        {
            { InputKeys.FirstName, null! },
            { InputKeys.LastName, null! }
        };
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal("Namn", parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("saknas", parserDataObject[OutputKeys.LastName]);
    }
    // Case B: Nycklarna finns, men tomma
    [Fact]
    public void ParseData_WhiteSpaceInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>
        {
            { InputKeys.FirstName, "" },
            { InputKeys.LastName, "" }
        };
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal("Namn", parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("saknas", parserDataObject[OutputKeys.LastName]);
    }
    // Om bara förnamn saknas ska förnamn bli tomma strängen och efternamnet bli det angivna efternamnet. 
    [Fact]
    public void ParseData_OnlyLastNameExists_ShouldSetFirstNameToEmptyAndLastNameToLastName()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>
        {
            { InputKeys.LastName, "Pakola" }
        };
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal(string.Empty, parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("Pakola", parserDataObject[OutputKeys.LastName]);
    }
    // Om efternamnet saknas ska förnamnet bli det angivna förnamnet, och efternamnet bli "Efternamn saknas". 
    [Fact]
    public void ParseData_OnlyFirstNameExists_ShouldSetLastNameToMissing()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>
        {
            { InputKeys.FirstName, "Håkan" }
        };
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal("Håkan", parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("Efternamn saknas", parserDataObject[OutputKeys.LastName]);
    }
    // Om båda finns ska båda anges rakt av.
    [Fact]
    public void ParseData_BothNamesExist_ShouldSetBothNamesDirectly()
    {
        // Arrange
        var parserDataObject = new Dictionary<string, string>
        {
            { InputKeys.FirstName, "Håkan" },
            { InputKeys.LastName, "Bråkan" }
        };
        var dataParser = new DataParser();

        // Act
        dataParser.Execute(parserDataObject);

        // Assert
        Assert.Equal("Håkan", parserDataObject[OutputKeys.FirstName]);
        Assert.Equal("Bråkan", parserDataObject[OutputKeys.LastName]);
    }
}