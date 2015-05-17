namespace Scrappy2
{
    using System;
    using System.Linq;

    using HtmlAgilityPack;

    public class CourseScraper
    {
        private readonly string courseHtml;

        private readonly string descriptionHtml;

        private readonly Course course;

        public CourseScraper(Uri courseUri)
        {
            this.course = new Course();
            this.course.CourseUrl = courseUri;

            var driver = new SeleniumDriver();

            var descriptionLink = GetDesciptionUri(courseUri);

            this.courseHtml = driver.GetHtml(courseUri);
            this.descriptionHtml = driver.GetHtml(descriptionLink);

            driver.Quit();
        }

        private static Uri GetDesciptionUri(Uri originalLink)
        {
            var link = originalLink.AbsoluteUri.Insert(35, "description/");
            var uri = new Uri(link);

            return uri;
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

            this.course.Name = htmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText;

            var contents = htmlDoc.DocumentNode.SelectSingleNode("//section[@id='table-of-contents']");

            var sections = contents.SelectNodes(".//div[contains(@class, 'section')]");

            for (int i = 1; i < sections.Count; i++)
            {
                var topic = new Course.Topic();
                topic.Name = sections[i].SelectSingleNode(".//p[@class='title']").SelectSingleNode(".//a").InnerText;

                foreach (HtmlNode video in sections[i].SelectNodes(".//h5"))
                {
                    topic.Videos.Add(video.InnerText);
                }

                this.course.Topics.Add(topic);
            }
        }

        public void GetCourseDescription()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.descriptionHtml);

            var description = htmlDoc.DocumentNode.SelectSingleNode("//div[@ng-view]");
            this.course.Description = description.SelectSingleNode(".//p").InnerText;

            this.course.Rating = Convert.ToDouble(htmlDoc.DocumentNode.SelectSingleNode("//input[@name='score']")
                .Attributes["value"]
                .Value) * 2;

            var released = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='line-list']")
                .ChildNodes
                .First(x => x.InnerText.Contains("Released "))
                .SelectSingleNode(".//span")
                .InnerText;

            this.course.Released = Convert.ToDateTime(released);

            var tags = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='related-tag-list']").SelectNodes(".//a");

            foreach (var tag in tags)
            {
                this.course.Tags.Add(tag.ChildNodes.First().InnerText.Trim());
            }

            var authors = description.SelectNodes(".//div[@ng-repeat='author in courseAuthors']");

            foreach (var author in authors)
            {
                this.course.Authors.Add(author.SelectSingleNode(".//h4").InnerText);
            }
        }
    }
}