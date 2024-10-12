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
        var dataParser = new DataParser();

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Namn", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("saknas", dataParser.GetValue(OutputKeys.LastName));
    }
    // Case B: Nycklarna finns, men null
    [Fact]
    public void ParseData_NullInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.FirstName, null!);
        dataParser.SetValue(InputKeys.LastName, null!);

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Namn", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("saknas", dataParser.GetValue(OutputKeys.LastName));
    }
    // Case C: Nycklarna finns, men tomma
    [Fact]
    public void ParseData_EmptyInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.FirstName, "");
        dataParser.SetValue(InputKeys.LastName, "");

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Namn", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("saknas", dataParser.GetValue(OutputKeys.LastName));
    }
    // Case D: Nycklarna finns, men whitespace
    [Fact]
    public void ParseData_WhiteSpaceInputKeys_ShoudSetDefaultOutputValues()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.FirstName, " ");
        dataParser.SetValue(InputKeys.LastName, " ");

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Namn", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("saknas", dataParser.GetValue(OutputKeys.LastName));
    }
    // Om bara förnamn saknas ska förnamn bli tomma strängen och efternamnet bli det angivna efternamnet. 
    [Fact]
    public void ParseData_OnlyLastNameExists_ShouldSetFirstNameToEmptyAndLastNameToLastName()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.LastName, "Testsson");

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal(string.Empty, dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("Testsson", dataParser.GetValue(OutputKeys.LastName));
    }
    // Om efternamnet saknas ska förnamnet bli det angivna förnamnet, och efternamnet bli "Efternamn saknas". 
    [Fact]
    public void ParseData_OnlyFirstNameExists_ShouldSetLastNameToMissing()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.FirstName, "Håkan");

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Håkan", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("Efternamn saknas", dataParser.GetValue(OutputKeys.LastName));
    }
    // Om båda finns ska båda anges rakt av.
    [Fact]
    public void ParseData_BothNamesExist_ShouldSetBothNamesDirectly()
    {
        // Arrange
        var dataParser = new DataParser();
        dataParser.SetValue(InputKeys.FirstName, "Håkan");
        dataParser.SetValue(InputKeys.LastName, "Bråkan");

        // Act
        dataParser.Execute();

        // Assert
        Assert.Equal("Håkan", dataParser.GetValue(OutputKeys.FirstName));
        Assert.Equal("Bråkan", dataParser.GetValue(OutputKeys.LastName));
    }
}