using System.Xml;
using Bogus;

namespace MigrationTool.Web.Data;

public class Seeder
{
    private static readonly Random random = new();
    public static void Init()
    {
        List<Entities.Person> personSeedData = GeneratePersons(100);
        CreateXmlFile(personSeedData);
    }

    static List<Entities.Person> GeneratePersons(int count)
    {
        List<Entities.Person> persons = [];

        var faker = new Faker<Entities.Person>()
            .RuleFor(p => p.Uuid, f => Guid.NewGuid())
            .RuleFor(p => p.GivenName, f => TrySetName(f.Name.FirstName()))
            .RuleFor(p => p.FamilyName, f => TrySetName(f.Name.LastName()));

        for (int i = 0; i < count; i++)
        {
            persons.Add(faker.Generate());
        }
        return persons;
    }

    private static string? TrySetName(string name)
    {
        int chance = random.Next(0, 100);

        if (chance < 10)
            return null;

        if (chance < 20)
            return string.Empty;

        if (chance < 30)
            return " ";

        else
            return name;
    }

    static void CreateXmlFile(List<Entities.Person> persons)
    {
        XmlDocument doc = new XmlDocument();

        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.InsertBefore(xmlDeclaration, doc.DocumentElement);

        XmlElement rootElement;
        rootElement = doc.CreateElement("p", "root", "http://www.test.data/xml/person");
        doc.AppendChild(rootElement);

        XmlComment comment = doc.CreateComment("Root element content goes here");
        rootElement.AppendChild(comment);

        foreach (var person in persons)
        {
            XmlElement personElement = doc.CreateElement("p", "person", "http://www.test.data/xml/person");

            XmlElement uuidElement = doc.CreateElement("p", "uuid", "http://www.test.data/xml/person");
            uuidElement.InnerText = person.Uuid.ToString();
            personElement.AppendChild(uuidElement);
            
            
            XmlElement namnElement = doc.CreateElement("p", "namn", "http://www.test.data/xml/person");

            if (!string.IsNullOrWhiteSpace(person.GivenName))
            {
                XmlElement fornamnElement = doc.CreateElement("p", "fornamn", "http://www.test.data/xml/person");
                fornamnElement.InnerText = person.GivenName;
                namnElement.AppendChild(fornamnElement);
            }

            if (!string.IsNullOrWhiteSpace(person.FamilyName))
            {
                XmlElement efternamnElement = doc.CreateElement("p", "efternamn", "http://www.test.data/xml/person");
                efternamnElement.InnerText = person.FamilyName;
                namnElement.AppendChild(efternamnElement);
            }
            personElement.AppendChild(namnElement);
            
            rootElement.AppendChild(personElement);
        }
        doc.Save("data.xml");
    }
}