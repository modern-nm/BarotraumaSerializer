using System.Xml.Serialization;
using BarotraumaSerializer;
using System.Xml.Linq;
class Program
{
    static void Main(string[] args)
    {
        // Display the number of command line arguments.
        //XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerConfig));

        //using (FileStream stream = new FileStream(args[0], FileMode.Open))
        //{
        //    PlayerConfig? config = xmlSerializer.Deserialize(stream) as PlayerConfig;

        //}
        string configPath = @"C:\Program Files (x86)\Steam\steamapps\common\Barotrauma\config_player.xml";
        

        int argsLength = args.Length;

        if(argsLength > 0)
            // exports packages list from "config_player.xml" to specific file
            if (args.Contains(@"--read") && (args.Length>1))
            {
                string packagesPath = args[1];

                XDocument xDocument = XDocument.Load(configPath);
                
                XElement? root = xDocument.Element("config")?.Element("contentpackages");

                if (root != null)
                {
                    root.Save(packagesPath);
                }
                else { Console.WriteLine("root is null."); }
                
            }
            else if(args.Contains(@"--write") && (args.Length > 1))
            {
                // imports packages list from specific file to "config_player.xml"
                string packagesPath = args[1];

                XDocument xDocument = XDocument.Load(configPath);

                XElement? root = xDocument.Element("config")?.Element("contentpackages");

                XDocument xContent = XDocument.Load(packagesPath);
                XElement? packages = xContent.Element("contentpackages");

                if ((packages != null) && (root != null))
                {

                    root.AddBeforeSelf(packages);
                    root.Remove();
                    xDocument.Save(configPath);
                }

            }

        
    }
}