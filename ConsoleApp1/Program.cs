using System.Text;
using System.Xml;

var inputFile = "";
var outputFile = "";

XmlDocument doc = new XmlDocument();

doc.Load(inputFile);
var sb = new StringBuilder();

var PersonANames = new string[]
{
};

var PersonBNames = new string[]
{
};

var allNames = new List<string>();

var fixNames = false;

foreach (XmlNode node in doc.ChildNodes)
{
    if (node.Name == "Log")
    {
        foreach (XmlNode node2 in node.ChildNodes)
        {
            foreach (XmlNode node3 in node2.ChildNodes)
            {
                if (node3.Name != "To")
                {
                    
                    if (node3.Name == "From")
                    {
                        var cleanedText = node3.InnerXml.Replace("<User FriendlyName=\"", "").Replace("\" />", "").Replace("&gt;: ", "");
                        if (!allNames.Contains(cleanedText))
                        {
                            allNames.Add(cleanedText);
                        }

                        if (fixNames)
                        {
                            if (PersonANames.Contains(cleanedText))
                            {
                                cleanedText = "Person A";
                            }
                            if (PersonBNames.Contains(cleanedText))
                            {
                                cleanedText = "Person B";
                            }
                        }
                        sb.Append(cleanedText);
                    }
                    else if (node3.Name == "Text")
                    {
                        sb.Append($": {node3.InnerXml}");
                        Console.WriteLine(sb.ToString());
                        if (fixNames)
                        {
                            File.AppendAllText($"{outputFile}-fixedNames", $"{sb.ToString()}\n");
                        }
                        else
                        {
                            File.AppendAllText(outputFile, $"{sb.ToString()}\n");
                        }
                        
                        sb.Clear();
                    }
                }
            }
        }
    }
}

foreach (var name in allNames)
{
    Console.WriteLine(name);
}

Console.ReadLine();