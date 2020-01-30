using System.Collections.Generic;
using System.Linq;

namespace Vidzy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dbContext = new VidzyContext();

            // **********************************  LINQ Syntax  ************************************
            System.Console.WriteLine("\n**********************************  LINQ Syntax  ************************************");
            //  LINQ Syntax - Action movies sorted by name
            //  LINQ Syntax - Action movies sorted by name
            //  LINQ Syntax - Action movies sorted by name
            System.Console.WriteLine("\nLINQ Syntax - Action movies sorted by name:");

            var videos =
                from v in dbContext.Videos
                where v.Genre.Name.Contains("Action")
                orderby v.Name
                select v;

            foreach (var video in videos)
                System.Console.WriteLine("\t" + video.Name);

            //  LINQ Syntax - Gold drama movies sorted by release date (newest first)
            //  LINQ Syntax - Gold drama movies sorted by release date (newest first)
            //  LINQ Syntax - Gold drama movies sorted by release date (newest first)
            System.Console.WriteLine("\nLINQ Syntax - Gold drama movies sorted by release date (newest first):");

            videos =
               from v in dbContext.Videos
               where v.Genre.Name.Contains("Drama")
               && v.Classification == Classification.Gold
               orderby v.ReleaseDate descending
               select v;

            foreach (var video in videos)
                System.Console.WriteLine("\t" + video.Name);

            //  LINQ Syntax - All movies projected into an anonymous type with two properties: MovieName and Genre
            //  LINQ Syntax - All movies projected into an anonymous type with two properties: MovieName and Genre
            //  LINQ Syntax - All movies projected into an anonymous type with two properties: MovieName and Genre
            System.Console.WriteLine("\nLINQ Syntax - All movies projected into an anonymous type with two properties: MovieName and Genre:");

            var videosQ =
               from v in dbContext.Videos
               select new { MovieName = v.Name, Genre = v.Genre.Name };

            foreach (var video in videosQ)
                System.Console.WriteLine("\tName: {0}, Genre: {1}", video.MovieName, video.Genre);

            //  LINQ Syntax - All movies grouped by their classification
            //  LINQ Syntax - All movies grouped by their classification
            //  LINQ Syntax - All movies grouped by their classification
            System.Console.WriteLine("\nLINQ Syntax - All movies grouped by their classification:");

            var groups =
              from v in dbContext.Videos
              orderby v.Name
              group v by v.Classification into g
              select new { Classification = g.Key, Videos = g };

            foreach (var group in groups)
            {
                System.Console.WriteLine("Group Classification(key): " + group.Classification);

                System.Console.WriteLine("\t" + group.Videos);
            }

            //  LINQ Syntax - List of classifications sorted alphabetically and count of videos in them.
            //  LINQ Syntax - List of classifications sorted alphabetically and count of videos in them.
            //  LINQ Syntax - List of classifications sorted alphabetically and count of videos in them.
            System.Console.WriteLine("\nLINQ Syntax - List of classifications sorted alphabetically and count of videos in them:");

            var groups2 =
              from v in dbContext.Videos
              orderby v.Classification
              group v by v.Classification into g
              select new { Classification = g.Key, VideosQuantity = g.Count() };

            foreach (var group in groups2)
            {
                System.Console.WriteLine("Classification: {0}, Number of Videos: {1}", group.Classification, group.VideosQuantity);
            }

            //  LINQ Syntax - List of genres and number of videos they include, sorted by the number of videos
            //  LINQ Syntax - List of genres and number of videos they include, sorted by the number of videos
            //  LINQ Syntax - List of genres and number of videos they include, sorted by the number of videos
            System.Console.WriteLine("\nLINQ Syntax - List of genres and number of videos they include, sorted by the number of videos:");

            var groups3 =
              from v in dbContext.Videos
              orderby v.Name
              group v by v.Genre.Name into g
              select new { GenreName = g.Key, VideosQuantity = g.Count() };

            foreach (var group in groups3)
            {
                System.Console.WriteLine("Genre: {0}, Number of Videos: {1}", group.GenreName, group.VideosQuantity);
            }

            //  ******************************  LINQ Extension Methods ******************************
            System.Console.WriteLine("\n******************************  LINQ Extension Methods ******************************");
            //  LINQ Extension Methods - Action movies sorted by name
            //  LINQ Extension Methods - Action movies sorted by name
            //  LINQ Extension Methods - Action movies sorted by name
            System.Console.WriteLine("\nLINQ Extension Methods - Action movies sorted by name:");

            videos = dbContext.Videos
                .Where(v => v.Genre.Name == "Action")
                .OrderBy(v => v.Name);

            foreach (var video in videos)
                System.Console.WriteLine("\t" + video.Name);

            //  LINQ Extension Methods - Gold drama movies sorted by release date (newest first)
            //  LINQ Extension Methods - Gold drama movies sorted by release date (newest first)
            //  LINQ Extension Methods - Gold drama movies sorted by release date (newest first)
            System.Console.WriteLine("\nLINQ Extension Methods - Gold drama movies sorted by release date (newest first):");

            videos = dbContext.Videos
                .Where(v => v.Genre.Name == "Drama" && v.Classification == Classification.Gold)
                .OrderByDescending(v => v.ReleaseDate);

            foreach (var video in videos)
                System.Console.WriteLine("\t" + video.Name);

            //  LINQ Extension Methods - All movies projected into an anonymous type with two properties: MovieName and Genre
            //  LINQ Extension Methods - All movies projected into an anonymous type with two properties: MovieName and Genre
            //  LINQ Extension Methods - All movies projected into an anonymous type with two properties: MovieName and Genre
            System.Console.WriteLine("\nLINQ Extension Methods - All movies projected into an anonymous type with two properties: MovieName and Genre:");
            var videos2 = dbContext.Videos.Select(v => new
            {
                MovieName = v.Name,
                Genre = v.Genre.Name
            });

            foreach (var video in videos2)
                System.Console.WriteLine("\tName: {0}, Genre: {1}", video.MovieName, video.Genre);

            //  LINQ Extension Methods - All movies grouped by their classification
            //  LINQ Extension Methods - All movies grouped by their classification
            //  LINQ Extension Methods - All movies grouped by their classification
            System.Console.WriteLine("\nLINQ Extension Methods - All movies grouped by their classification:");
            var groups4 = dbContext.Videos.GroupBy(v => v.Classification).Select(g => new
            {
                Classification = g.Key.ToString(),
                Videos = g.OrderBy(v => v.Name)
            }); ;

            foreach (var group in groups4)
            {
                System.Console.WriteLine("Group Classification(key): " + group.Classification);

                foreach (var movie in group.Videos)
                {
                    System.Console.WriteLine("\t" + movie.Name);
                }
            }

            //  LINQ Extension Methods - List of classifications sorted alphabetically and count of videos in them
            //  LINQ Extension Methods - List of classifications sorted alphabetically and count of videos in them
            //  LINQ Extension Methods - List of classifications sorted alphabetically and count of videos in them
            System.Console.WriteLine("\nLINQ Extension Methods - List of classifications sorted alphabetically and count of videos in them:");

            var groups1 = dbContext.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(),
                    VideosQuantity = g.Count()
                }).OrderBy(v => v.Classification);

            foreach (var group in groups2)
            {
                System.Console.WriteLine("Classification: {0}, Number of Videos: {1}", group.Classification, group.VideosQuantity);
            }

            //  LINQ Extension Methods - List of genres and number of videos they include, sorted by the number of videos
            //  LINQ Extension Methods - List of genres and number of videos they include, sorted by the number of videos
            //  LINQ Extension Methods - List of genres and number of videos they include, sorted by the number of videos
            System.Console.WriteLine("\nLINQ Extension Methods - List of genres and number of videos they include, sorted by the number of videos:");
            //videos = null;

            foreach (var video in videos)
                System.Console.WriteLine("\t" + video.Name);

            System.Console.ReadLine();
        }
    }
}