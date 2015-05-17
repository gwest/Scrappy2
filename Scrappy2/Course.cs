namespace Scrappy2
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public Uri CourseUrl { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public DateTime Released { get; set; }

        public List<string> Authors { get; set; }
        public List<string> Tags { get; set; }
        public List<Topic> Topics { get; set; }
        
        public class Topic
        {
            public string Name { get; set; }
            public List<string> Videos { get; set; }

            public Topic()
            {
                this.Videos = new List<string>();
            }
        }

        public Course()
        {
            this.Authors = new List<string>();
            this.Tags = new List<string>();
            this.Topics = new List<Topic>();
        }

        public Course FakeCourse()
        {
            this.Name = "Test Title";
            this.Description = "Test Desc";
            this.Rating = 8.5;
            this.Released = new DateTime(2014, 11, 14);

            this.Authors.Add("Test Author 1");
            this.Authors.Add("Test Author 2");

            this.Tags.Add(".NET");
            this.Tags.Add("Test1");

            var topic1 = new Topic();
            topic1.Name = "Test Topic 1";
            topic1.Videos.Add("Topic 1 Video 1");
            topic1.Videos.Add("Topic 1 Video 2");

            this.Topics.Add(topic1);

            var topic2 = new Topic();
            topic2.Name = "Test Topic 2";
            topic2.Videos.Add("Topic 2 Video 1");
            topic2.Videos.Add("Topic 2 Video 2");

            this.Topics.Add(topic2);

            return this;
        }
    }
}