namespace Scrappy2
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    public class Nfo
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        public int TopicNumber { get; set; }
        public XDocument XDocument { get; set; }

        public Nfo(string name, string topic, int topicNumber, XDocument xDocument)
        {
            this.Name = name;
            this.Topic = this.MakeValidFileName(topic);
            this.TopicNumber = topicNumber;
            this.XDocument = xDocument;
        }

        public void Save(string path, string folderName)
        {
            var directory = new DirectoryInfo(path + this.MakeValidFileName(folderName));

            if (!directory.Exists)
            {
                directory.Create();
            }

            var fileName = directory.FullName + @"\";

            if (!string.IsNullOrWhiteSpace(this.Topic))
            {
                fileName += this.TopicNumber.ToString("D2") + " " + this.Topic + @"\";
            }

            fileName += this.MakeValidFileName(this.Name) + ".nfo";

            this.XDocument.Save(fileName);

            Console.WriteLine("Saved to : " + fileName);
        }

        private string MakeValidFileName(string name)
        {
            var builder = new StringBuilder();
            var invalid = Path.GetInvalidFileNameChars();

            foreach (var cur in name.Where(cur => !invalid.Contains(cur)))
            {
                builder.Append(cur);
            }

            return builder.ToString();
        }
    }
}