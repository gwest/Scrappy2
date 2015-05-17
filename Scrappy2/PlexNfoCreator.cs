namespace Scrappy2
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class PlexNfoCreator
    {
        private readonly Course course;

        public PlexNfoCreator(Course course)
        {
            this.course = course;
        }

        public List<Nfo> BuildVideoNfos()
        {
            var docs = new List<Nfo>();

            for (var topicNumber = 0; topicNumber < course.Topics.Count; topicNumber++)
            {
                var currentTopic = course.Topics[topicNumber];

                for (var videoNumber= 0; videoNumber < currentTopic.Videos.Count; videoNumber++)
                {
                    var currentVideo = currentTopic.Videos[videoNumber];

                    var xml = new XDocument();

                    var episodeDetails = new XElement("episodedetails", 
                        new XAttribute("xsd", "http://www.w3.org/2001/XMLSchema"), 
                        new XAttribute("xsi", "http://www.w3.org/2001/XMLSchema-instance"));

                    episodeDetails.Add(new XElement("title", currentVideo));
                    episodeDetails.Add(new XElement("season", topicNumber + 1));
                    episodeDetails.Add(new XElement("episode", videoNumber + 1));
                    episodeDetails.Add(new XElement("aired", course.Released.ToString("yyyy-MM-dd")));
                    episodeDetails.Add(new XElement("plot"));
                    episodeDetails.Add(new XElement("displayseason", currentTopic.Name));
                    episodeDetails.Add(new XElement("displayepisode", currentVideo));
                    episodeDetails.Add(new XElement("thumb"));
                    episodeDetails.Add(new XElement("watched", false));
                    episodeDetails.Add(new XElement("credits"));
                    episodeDetails.Add(new XElement("director"));
                    episodeDetails.Add(new XElement("rating", course.Rating));
                    
                    episodeDetails.Add(this.GetActors());
                    
                    xml.Add(episodeDetails);

                    var name = string.Format("{0} - S{1}E{2} - {3}", course.Name, topicNumber + 1, videoNumber + 1, currentVideo);
                    docs.Add(new Nfo(name, xml));
                }
            }

            return docs;
        }

        public Nfo BuildCourseNfo()
        {
            var xml = new XDocument();

            var tvShow = new XElement("tvshow",
                new XAttribute("xsd", "http://www.w3.org/2001/XMLSchema"),
                new XAttribute("xsi", "http://www.w3.org/2001/XMLSchema-instance"));

            tvShow.Add(new XElement("title", new XText(course.Name)));
            tvShow.Add(new XElement("rating", course.Rating));
            tvShow.Add(new XElement("plot", course.Description));
            tvShow.Add(new XElement("episodeguide", new XElement("url", course.CourseUrl)));
            tvShow.Add(new XElement("episodeguideurl", course.CourseUrl));
            tvShow.Add(new XElement("mpaa", "TV-U"));
            tvShow.Add(new XElement("id"));

            foreach (var tag in this.course.Tags)
            {
                tvShow.Add(new XElement("genre", tag));
            } 
            
            tvShow.Add(new XElement("premiered", course.Released.ToString("yyyy-MM-dd")));
            tvShow.Add(new XElement("studio", "Pluralsight"));
            tvShow.Add(this.GetActors());

            xml.Add(tvShow);

            return new Nfo("tvshow", xml);
        }

        private XElement GetActors()
        {
            var actors = new XElement("actors");

            foreach (var author in this.course.Authors)
            {
                var actor = new XElement("actor", new XElement("name", author), new XElement("role"), new XElement("thumb"));

                actors.Add(actor);
            }
            return actors;
        }
    }
}