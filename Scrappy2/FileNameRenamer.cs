namespace Scrappy2
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class FileNameRenamer
    {
        private readonly List<Nfo> nfos;
        private readonly string videoDirectory;
        private readonly string saveDirectory;
        private readonly string courseName;

        public FileNameRenamer(List<Nfo> nfos, string videoDirectory, string saveDirectory, string courseName)
        {
            this.nfos = nfos;
            this.videoDirectory = videoDirectory;
            this.saveDirectory = saveDirectory;
            this.courseName = this.MakeValidFileName(courseName);
        }

        public void RenameFiles()
        {
            var files = this.GetFiles();
            var videos = files.Where(x => x.Extension == ".mp4" || x.Extension == ".wmv").ToList();

            for (int index = 0; index < this.nfos.Count; index++)
            {
                var nfo = this.nfos[index];
                var video = videos[index];

                var directory = new DirectoryInfo(saveDirectory + this.courseName + "/" + nfo.TopicNumber.ToString("D2") + " " + nfo.Topic);

                if (!directory.Exists)
                {
                    directory.Create();
                }

                video.CopyTo(saveDirectory + this.courseName + "/" + nfo.TopicNumber.ToString("D2") + " " + nfo.Topic + "/" + this.MakeValidFileName(nfo.Name) + video.Extension);
                nfo.Save(this.saveDirectory, courseName);
            }

            var otherFiles = files.Where(x => x.Extension != ".mp4" && x.Extension != ".wmv");

            if (otherFiles.Any())
            {
                var courseDirectory = new DirectoryInfo(saveDirectory + this.courseName);

                foreach (var file in otherFiles.ToList())
                {
                    var fileDirectory = file.DirectoryName.Replace(this.videoDirectory, "");
                    courseDirectory = new DirectoryInfo(saveDirectory + this.courseName + "/" + fileDirectory);

                    if (!courseDirectory.Exists)
                    {
                        courseDirectory.Create();
                    }

                    file.CopyTo(saveDirectory + this.courseName + "/" + fileDirectory + "/" + file.Name);
                }
            }
        }

        private List<FileInfo> GetFiles()
        {
            var directory = new DirectoryInfo(this.videoDirectory);

            return directory.GetFiles("*.*", SearchOption.AllDirectories).ToList();
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