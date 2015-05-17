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
            var videos = this.GetVideoFiles();

            for (int index = 0; index < this.nfos.Count; index++)
            {
                var nfo = this.nfos[index];
                var video = videos[index];

                var directory = new DirectoryInfo(saveDirectory + this.courseName);
                
                if (!directory.Exists)
                {
                    directory.Create();
                }

                video.MoveTo(saveDirectory + this.courseName + "/" + this.MakeValidFileName(nfo.Name) + ".mp4");
                nfo.Save(this.saveDirectory, courseName);
            }
        }

        private List<FileInfo> GetVideoFiles()
        {
            var directory = new DirectoryInfo(this.videoDirectory);

            return directory.GetFiles("*.mp4", SearchOption.AllDirectories).ToList();
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