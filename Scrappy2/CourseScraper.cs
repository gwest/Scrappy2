namespace Scrappy2
{
    using System;
    using System.Linq;

    using HtmlAgilityPack;

    public class CourseScraper
    {
        private readonly string courseHtml;

        private readonly Course course;

        public CourseScraper(Uri courseUri)
        {
            this.course = new Course();
            this.course.CourseUrl = courseUri;

            var driver = new SeleniumDriver();

            this.courseHtml = driver.GetHtml(courseUri);

            driver.Quit();
        }

        public Course GetCourse()
        {
            this.GetCourseTopics();
            this.GetCourseDescription();

            return this.course;
        }

        public void GetCourseTopics()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.courseHtml);

            this.course.Name = htmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText.Trim();

            var contents = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'tab-content')]");

            var sectionTitles = contents.SelectNodes(".//a[contains(@class, 'accordion-title__title')]");
            var sectionVideos = contents.SelectNodes(".//div[contains(@class, 'accordion-content')]");

            for (int i = 0; i < sectionTitles.Count; i++)
            {
                var topic = new Course.Topic();
                topic.Name = sectionTitles[i].InnerText.Trim();

                var videos = sectionVideos[i]
                    .SelectNodes(".//span[contains(@class, 'accordion-content__row__title')]");

                foreach (HtmlNode video in videos)
                {
                    topic.Videos.Add(video.InnerText.Trim());
                }

                this.course.Topics.Add(topic);
            }
        }

        public void GetCourseDescription()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.courseHtml);

            var description = htmlDoc
                .DocumentNode
                .SelectSingleNode("//div[contains(@class, 'course-description-tile')]")
                .SelectSingleNode("//div[contains(@class, 'course-info-tile-right')]")
                .SelectSingleNode(".//p")
                .InnerText.Trim();

            this.course.Description = description;

            var ratingsContainer = htmlDoc
                .DocumentNode
                .SelectSingleNode("//div[contains(@class, 'course-info__row--right course-info__row--rating')]");

            if (ratingsContainer != null)
            { 
                var ratings = ratingsContainer
                    .ChildNodes
                    .Where(x => x.Name == "i")
                    .ToList();

                this.course.Rating = ratings.Count;

                foreach (var rating in ratings)
                {
                    if (rating.Attributes.Any(a => a.Name == "class" && a.Value.Contains("gray")))
                    {
                        this.course.Rating -= 1;
                    }
                }

                var halfRating = ratings
                    .Any(x => x
                        .Attributes
                            .Any(a => 
                                a.Name == "class" 
                             && a.Value.Contains("fa fa-star-half-o")));
            
                this.course.Rating = this.course.Rating - (halfRating ? 0.5 : 0);
                this.course.Rating *= 2;
            }

            var released = htmlDoc.DocumentNode
                .SelectSingleNode("//meta[@name='publish-date']")
                .Attributes
                .First(x => x.Name == "content")
                .Value
                .Substring(0, 16);

            this.course.Released = Convert.ToDateTime(released);

            var authorsString = htmlDoc.DocumentNode
                .SelectSingleNode("//meta[@name='authors']")
                .Attributes
                .First(x => x.Name == "content")
                .Value;

            var authors = authorsString.Split(',');
            this.course.Authors.AddRange(authors);
        }
    }
}