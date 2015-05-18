namespace Scrappy2
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {

            var newPath = @"C:\PluralsightTest\";
            var currentPath = @"C:\PluralsightTest\Old";
            var link = new Uri("http://www.pluralsight.com/courses/developer-job-interviews");

            SortCourse(link, newPath, currentPath);
        }

        private static void SortCourse(Uri link, string newPath, string currentPath)
        {
            Course course;

            if (false)
            {
                var courseScraper = new CourseScraper(link);

                course = courseScraper.GetCourse();

                PrintCourse(course);
                Console.WriteLine();
            }
            else
            {
                course = new Course().FakeCourse();
            }

            var plexNfoCreator = new PlexNfoCreator(course);

            var nfos = plexNfoCreator.BuildVideoNfos();

            nfos.ForEach(x => Console.WriteLine(x.XDocument.ToString()));

            var courseNfo = plexNfoCreator.BuildCourseNfo();
            Console.WriteLine(courseNfo.XDocument);

            var fileNameRenamer = new FileNameRenamer(nfos, currentPath, newPath, course.Name);
            fileNameRenamer.RenameFiles();

            Console.ReadLine();
        }

        private static void PrintCourse(Course course)
        {
            Console.WriteLine(course.Name);
            Console.WriteLine(course.Description);
            Console.WriteLine(course.Rating);
            Console.WriteLine(course.Released);
            Console.WriteLine(string.Join(", ", course.Tags));

            foreach (var author in course.Authors)
            {
                Console.WriteLine(author);
            }

            foreach (var topic in course.Topics)
            {
                Console.WriteLine("\t" + topic.Name);

                foreach (var video in topic.Videos)
                {
                    Console.WriteLine("\t\t" + video);
                }
            }
        }
    }
}

