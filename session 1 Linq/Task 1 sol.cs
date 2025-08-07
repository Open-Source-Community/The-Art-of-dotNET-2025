using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public double Salary { get; set; }
    public int Experience { get; set; }
}

public class Movie
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int directorId { get; set; }
}

public class Director
{
    public int id { get; set; }
    public string name { get; set; }
    public List<Movie> movies { get; set; } = new List<Movie>();
    public int nationalityId { get; set; }
}

public class Nationality
{
    public int id { get; set; }
    public string name { get; set; }
    public int numberOfDirectors { get; set; }
}

public class Context
{
    public List<Employee> employees;

    public List<Movie> movies;

    public List<Director> directors;

    public List<Nationality> nationalities;
    public Context()
    {
        employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR", Salary = 55000, Experience = 3 },
            new Employee { Id = 2, Name = "Bob", Department = "IT", Salary = 75000, Experience = 5 },
            new Employee { Id = 3, Name = "Charlie", Department = "Finance", Salary = 62000, Experience = 4 },
            new Employee { Id = 4, Name = "David", Department = "IT", Salary = 80000, Experience = 6 },
            new Employee { Id = 5, Name = "Eva", Department = "HR", Salary = 58000, Experience = 2 },
            new Employee { Id = 6, Name = "Frank", Department = "Marketing", Salary = 70000, Experience = 5 },
            new Employee { Id = 7, Name = "Grace", Department = "Finance", Salary = 60000, Experience = 4 }
        };

        movies = new List<Movie>
        {
            new Movie { id = 1, title = "Inception", description = "A thief who steals corporate secrets through dream-sharing technology.", directorId = 101 },
            new Movie { id = 2, title = "The Dark Knight", description = "Batman faces the Joker, a criminal mastermind.", directorId = 101 },
            new Movie { id = 3, title = "Interstellar", description = "A team travels through a wormhole to find a new home for humanity.", directorId = 101 },
            new Movie { id = 4, title = "Pulp Fiction", description = "The lives of two hitmen and others intertwine in LA.", directorId = 102 },
            new Movie { id = 5, title = "Django Unchained", description = "A freed slave sets out to rescue his wife from a brutal plantation owner.", directorId = 102 },
            new Movie { id = 6, title = "The Grand Budapest Hotel", description = "A quirky concierge teams up with a lobby boy to prove his innocence.", directorId = 103 },
            new Movie { id = 7, title = "Tenet", description = "A secret agent manipulates the flow of time to prevent World War III.", directorId = 101 },
            new Movie { id = 8, title = "Kill Bill: Vol. 1", description = "A former assassin seeks revenge on her ex-colleagues.", directorId = 102 }
        };

        directors = new List<Director>
        {
            new Director { id = 101, name = "Christopher Nolan", nationalityId = 1 },
            new Director { id = 102, name = "Quentin Tarantino", nationalityId = 2 },
            new Director { id = 103, name = "Wes Anderson", nationalityId = 1 }
        };
        
        nationalities = new List<Nationality>
        {
            new Nationality { id = 1, name = "British" },
            new Nationality { id = 2, name = "American" }
        };

        foreach (var director in directors)
        {
            director.movies = movies
                .Where(m => m.directorId == director.id)
                .ToList();
        }
    }
}

public class LinqTask
{
    private readonly Context _context;

    public LinqTask()
    {
        _context = new Context();
    }

    // 1. Get a list of all movie titles that contain the word "The" and sort them alphabetically.
    public IEnumerable<string> GetMoviesContainingThe()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Where(m => m.title.Contains("The"))
            .OrderBy(m => m.title)
            .Select(m => m.title);

        // Alternative (Inefficient) Solution
        // return _context.movies
        //     .OrderBy(m => m.title)
        //     .Where(m => m.title.Contains("The"))
        //     .Select(m => m.title);

        // Analysis: The recommended solution filters the collection first, reducing the number of items that need to be sorted.
        // Sorting is a computationally expensive operation (O(N log N)). By reducing N, we significantly improve performance.
        // The inefficient query sorts the entire collection first, then discards a majority of the items, wasting computational resources.
        // This highlights the order of methods matters!
    }

    // 2. Retrieve a list of movies with a title length greater than 10 characters, and project only the title and ID.
    public IEnumerable<dynamic> GetLongMovies()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Where(m => m.title.Length > 10)
            .Select(m => new { m.title, m.id });
    }

    // 3. Find the first movie (alphabetically) whose title starts with the letter "I".
    public Movie GetFirstMovieStartingWithI()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Where(m => m.title.StartsWith("I"))
            .OrderBy(m => m.title)
            .FirstOrDefault();
    }

    // 4. Join the movies with their respective directors and return a list of anonymous objects with movie title and director name.
    public IEnumerable<dynamic> GetMovieTitlesWithDirectorNames()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { MovieTitle = movie.title, DirectorName = director.name });

        // Alternative (Inefficient) Solution
        // return _context.movies
        //     .Select(m => new
        //     {
        //         MovieTitle = m.title,
        //         DirectorName = _context.directors.FirstOrDefault(d => d.id == m.directorId)?.name
        //     });

        // Analysis: The recommended solution uses the optimized `Join` method, which is purpose-built for correlating data between collections.
        // The inefficient solution performs a linear search (`FirstOrDefault`) on the `directors` collection for every single movie. This is
        // computationally expensive (O(N * M) complexity) and does not scale well with large datasets.
    }

    // 5. Find all movies directed by British directors by joining the Movie, Director, and Nationality collections.
    public IEnumerable<Movie> GetMoviesByBritishDirectors()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie, director })
            .Join(_context.nationalities,
                md => md.director.nationalityId,
                nationality => nationality.id,
                (md, nationality) => new { md.movie, nationality })
            .Where(x => x.nationality.name == "British")
            .Select(x => x.movie);

        // Alternative (Inefficient) Solution
        // return _context.movies
        //     .Where(m => _context.nationalities.FirstOrDefault(n => 
        //         n.id == _context.directors.FirstOrDefault(d => d.id == m.directorId).nationalityId)?.name == "British");

        // Analysis: Similar to query #4, the recommended solution uses a series of efficient `Join` operations. This is the correct,
        // scalable way to correlate data between multiple collections. The inefficient solution uses multiple nested `FirstOrDefault`
        // calls, performing a linear search on both `directors` and `nationalities` for every movie. This is extremely inefficient (O(N*M*P)) and
        // prone to `NullReferenceException` errors.
    }

    // 6. Group movies by director and list each director's name along with the number of movies they directed.
    public IEnumerable<dynamic> GetDirectorMovieCounts()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .GroupBy(m => m.directorId)
            .Join(_context.directors,
                group => group.Key,
                director => director.id,
                (group, director) => new { DirectorName = director.name, MovieCount = group.Count() });

        // Alternative (Inefficient) Solution
        // return _context.directors
        //     .Select(d => new { DirectorName = d.name, MovieCount = _context.movies.Count(m => m.directorId == d.id) });

        // Analysis: The recommended solution groups the movies once, then joins to get the director's name. This is a very efficient and clean
        // approach. The inefficient solution iterates through every director and performs a full scan of the `movies` collection for each director to get the count.
        // This is a much less performant nested loop approach.
    }

    // 7. Group all movies by their director’s nationality and calculate how many movies each nationality produced.
    public IEnumerable<dynamic> GetNationalityMovieCounts()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie, director })
            .Join(_context.nationalities,
                md => md.director.nationalityId,
                nationality => nationality.id,
                (md, nationality) => new { nationality.name, movie = md.movie })
            .GroupBy(x => x.name)
            .Select(group => new { Nationality = group.Key, MovieCount = group.Count() });

        // Alternative (Inefficient) Solution
        // return _context.nationalities
        //     .Select(n => new {
        //         Nationality = n.name,
        //         MovieCount = _context.movies.Count(m =>
        //             _context.directors.Any(d => d.id == m.directorId && d.nationalityId == n.id))
        //     });

        // Analysis: The recommended approach uses a series of `Join` and `GroupBy`
        // which are the optimized operators for this task.
    }

    // 8. Calculate the average movie ID for each director (group by director name, then average their movie IDs).
    public IEnumerable<dynamic> GetAverageMovieIdByDirector()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie.id, director.name })
            .GroupBy(x => x.name)
            .Select(group => new { DirectorName = group.Key, AverageMovieId = group.Average(x => x.id) });

        // Alternative (Inefficient) Solution
        // return _context.directors
        //     .Select(d => new {
        //         DirectorName = d.name,
        //         AverageMovieId = _context.movies.Where(m => m.directorId == d.id).Average(m => m.id)
        //     });
    }

    // 9. Aggregate all movie titles into a single comma-separated string (use Aggregate).
    public string GetAggregatedMovieTitles()
    {
        // Recommended (Most Efficient & Idiomatic) Solution
        // string.Join is specifically designed for this purpose and uses StringBuilder internally for optimal performance.
        return string.Join(", ", _context.movies.Select(m => m.title));

        // Alternative (Acceptable but Less Efficient for Large Collections) // The expected due to the question way.
        // The Aggregate method is a general-purpose aggregation operator.
        // return _context.movies
        //     .Select(m => m.title)
        //     .Aggregate((cumulative, next) => $"{cumulative}, {next}");

        // Analysis: The `string.Join` method is the most efficient and idiomatic C# solution for this task.
        // It has a linear time complexity (O(N)) because it uses a StringBuilder
        // to avoid creating new strings on each append operation.
        // The `Aggregate` method with string concatenation, while syntactically concise,
        // performs string concatenation in a loop.
        // For a large number of items,
        // this can be very inefficient due to its quadratic time complexity (O(N^2)).
        // It's acceptable for small collections but should be avoided for production code.
    }

    // 10. Skip the first 2 movies (ordered by title) and take the next 3. Return only their titles.
    public IEnumerable<string> GetSkippedAndTakenMovieTitles()
    {
        // Recommended Solution
        // This order is preferred for readability and to avoid accidental in-memory execution.
        return _context.movies
            .OrderBy(m => m.title)
            .Skip(2)
            .Take(3)
            .Select(m => m.title);

        // Alternative, functionally identical query
        // return _context.movies
        //     .Select(m => m.title)
        //     .OrderBy(m => m)
        //     .Skip(2)
        //     .Take(3);

        // Analysis:
        // In LINQ to Objects (with an in-memory List<Movie> / IEnumerable), both queries are functionally identical
        // and have the same performance characteristics due to lazy evaluation. The query is only
        // executed when the results are needed.

        // In LINQ to Entities (with an IQueryable<Movie> from a database context), the provider is intelligent
        // and can translate both queries into a single, optimized SQL statement as long as all methods are translatable.

        // However, the recommended order (`OrderBy` -> `Select`) is a defensive programming pattern.
        // It's a safeguard against accidentally using a C# method that is not translatable to SQL. If this happens,
        // the query will execute in-memory prematurely, moving the heavy lifting from the optimized database to your application's memory.

        // Example of the danger:
        //
        // var movies = _context.movies
        //     .Select(m => new { m.title, titleLength = m.title.Length })
        //     .OrderBy(x => x.titleLength) // This is now in-memory! The database returned ALL movie titles.
        //     .ToList();
        //
        // Example of the safe pattern:
        //
        // var movies = _context.movies
        //     .OrderBy(m => m.title.Length) // This is translatable to SQL and performed by the database.
        //     .Select(m => new { m.title, titleLength = m.title.Length })
        //     .ToList();
        //
        // The key takeaway is that by keeping projection (`Select`) at the end, you ensure that the query remains
        // an IQueryable for as long as possible, allowing EF Core to generate the most efficient SQL.
    }

    // 11. Get the top 2 directors (by number of movies directed) and list their names and counts.
    public IEnumerable<dynamic> GetTop2Directors()
    {
        // Recommended (Most Idiomatic for IQueryable) Solution
        // This leverages a navigation property, which is the most concise and readable approach in EF Core.
        // It is translated to a single, optimized SQL query (often using a JOIN and GROUP BY).
        return _context.directors
            .Select(d => new
            {
                DirectorName = d.name,
                MovieCount = d.movies.Count
            })
            .OrderByDescending(x => x.MovieCount)
            .Take(2);

        // Alternative (Excellent & Versatile) Solution
        // This solution is perfect for your in-memory list where navigation properties aren't populated.
        // It's also a fully translatable LINQ query that generates an equally efficient SQL statement.
        // return _context.movies
        //     .GroupBy(m => m.directorId)
        //     .Select(group => new { DirectorId = group.Key, MovieCount = group.Count() })
        //     .OrderByDescending(x => x.MovieCount)
        //     .Take(2)
        //     .Join(_context.directors,
        //         x => x.DirectorId,
        //         director => director.id,
        //         (x, director) => new { DirectorName = director.name, MovieCount = x.MovieCount });

        // Analysis:
        // Both solutions are highly efficient for a database context because they both translate to a single SQL query.
        // The Recommended Solution is preferred for its readability and brevity when navigation properties are available.
        // The Alternative Solution is a great general-purpose pattern that works reliably with or without navigation properties.
        // It's important to note that the GroupBy operator itself is very performant in both in-memory and database scenarios.
        // It uses an efficient hashing algorithm to group items in a single pass, with a time complexity of roughly O(N).
    }

    // 12. Take all movies except those directed by Quentin Tarantino and return their titles.
    public IEnumerable<string> GetMoviesNotByQuentinTarantino()
    {
        // Recommended (Most Scalable & Best for IQueryable) Solution
        // The Join operator is optimized for combining data from different sources in a single pass.
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie, director })
            .Where(x => x.director.name != "Quentin Tarantino")
            .Select(x => x.movie.title);

        // Alternative (Very Efficient for In-Memory Lists) Solution
        // This approach avoids the Join operation, which can be slightly faster for small
        // in-memory collections.
        // var id = _context.directors.Single(d => d.name == "Quentin Tarantino").id;
        // return _context.movies
        //     .Where(m => m.directorId != id)
        //     .Select(m => m.title);

        // Analysis:
        // For an in-memory collection (IEnumerable<T>), both solutions are very fast. The alternative
        // solution may even be marginally more performant as it avoids creating intermediate anonymous objects
        // during the join.
        //
        // For a database-backed IQueryable<T> (e.g., using Entity Framework Core), the recommended
        // solution is generally superior. The Join can be translated into a single, efficient SQL JOIN
        // query, resulting in only one database round trip. The alternative solution, however, would likely
        // result in two separate database queries: one to find the director's ID and a second to filter the movies.
        // Minimizing database round trips is a key principle for building performant backend applications.
    }

    // 13. Check if there are any movies directed by Wes Anderson.
    public bool HasMoviesByWesAnderson()
    {
        // Recommended (Most Efficient & Single Round Trip) Solution
        return _context.directors.Any(d => d.name == "Wes Anderson" && d.movies.Any());

        // Acceptable Solution
        // This query is fully translatable to SQL and results in a single, optimized database call.
        // return _context.movies.Any(m => _context.directors.Any(d => d.id == m.directorId && d.name == "Wes Anderson"));

        // Alternative (Less Efficient for IQueryable) Solution
        // This solution is correct for in-memory collections but results in two database round trips.
        // var directorId = _context.directors.Single(d => d.name == "Wes Anderson").id;
        // return _context.movies.Any(m => m.directorId == directorId);

        // Analysis:
        // A common concern with the recommended solution is its nested structure, which appears to have a
        // poor O(N^2) time complexity. However, this is a critical distinction between LINQ to Objects and LINQ to Entities.
        //
        // For an IQueryable<T> (e.g., a database context), EF Core does not execute a nested C# loop. It translates the entire
        // nested LINQ expression into a single, highly optimized SQL statement, often using a subquery with an EXISTS clause.
        // This allows the database's powerful query engine to perform the check efficiently, typically using indexes and
        // without iterating through every single record.
        //
        // Therefore, the recommended solution is the most performant because it completes the entire operation in a single
        // database round trip, whereas the alternative solution requires two separate and inefficient trips to the database.
    }

    // 14. Find the single movie (using Single) with the title "Inception" and throw an error if not exactly one match.
    public Movie GetSingleMovieInception()
    {
        // Solution
        return _context.movies.Single(m => m.title == "Inception");
    }

    // 15. Use GroupJoin to produce a list of directors, each with their corresponding list of movie titles.
    public IEnumerable<dynamic> GetDirectorsWithMoviesGroupJoin()
    {
        // Recommended (Most Efficient & Idiomatic) Solution
        return _context.directors
            .GroupJoin(_context.movies,
                director => director.id,
                movie => movie.directorId,
                (director, movies) => new { DirectorName = director.name, Movies = movies.Select(m => m.title) });

        // Alternative (Inefficient & Less Scalable) Solution
        // return _context.directors
        //     .Select(d => new
        //     {
        //         DirectorName = d.name,
        //         Movies = _context.movies.Where(m => m.directorId == d.id).Select(m => m.title).ToList()
        //     });

        // Analysis:
        // The Recommended Solution using GroupJoin is superior in both in-memory and database scenarios.
        //
        // For an in-memory collection (IEnumerable<T>), the alternative solution performs a nested scan of the
        // movies list for every director. This is an O(N * M) operation, where N is the number of directors
        // and M is the number of movies, which is very inefficient for large collections. GroupJoin, however,
        // is a single, optimized pass with a much better complexity, roughly O(N + M) with efficient hashing.
        //
        // For a database-backed IQueryable<T>, the performance difference is even more critical. GroupJoin
        // translates to a single, efficient LEFT OUTER JOIN query, resulting in just one database round trip.
        // The alternative solution, a classic N+1 problem, would execute one query to fetch all directors
        // and then a separate query for each director to get their movies. This is extremely inefficient
        // and can cause significant performance bottlenecks due to network latency.
    }

    // 16. Create a list of all combinations of directors and movie titles using SelectMany.
    public IEnumerable<dynamic> GetDirectorMovieCombinations()
    {
        // Solution
        return _context.directors
            .SelectMany(director => _context.movies,
                (director, movie) => new { DirectorName = director.name, MovieTitle = movie.title });

        // Analysis: `SelectMany` is designed specifically for this "cross join" or "all combinations" scenario.
        // It's a clean, declarative, and optimized way to flatten a collection of collections.
        // The unsuggested solution is to use nested `foreach` loops to manually produce the same result,
        // which is more verbose and less expressive of the developer's intent.
    }

    // 17. Join all movies with their directors and nationalities and return an object with: Title, DirectorName, NationalityName.
    public IEnumerable<dynamic> GetMovieDetailsWithNationality()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie, director })
            .Join(_context.nationalities,
                md => md.director.nationalityId,
                nationality => nationality.id,
                (md, nationality) => new
                {
                    Title = md.movie.title,
                    DirectorName = md.director.name,
                    NationalityName = nationality.name
                });

        // Alternative (Inefficient) Solution
        // return _context.movies
        //     .Select(m =>
        //     {
        //         var director = _context.directors.FirstOrDefault(d => d.id == m.directorId);
        //         var nationality = _context.nationalities.FirstOrDefault(n => n.id == director.nationalityId);
        //         return new
        //         {
        //             Title = m.title,
        //             DirectorName = director?.name,
        //             NationalityName = nationality?.name
        //         };
        //     });

        // Analysis: The recommended solution uses a series of
        // `Join`s which are optimized for relating multiple data sources.
        // The inefficient solution, again, relies on nested
        // `FirstOrDefault` calls within the `Select` clause.
        // This results in repeated linear scans of the director and nationality
        // collections for every movie, leading to poor performance and bad scalability.
    }

    // 18. Count the total number of unique directors who have made at least one movie.
    public int CountUniqueDirectorsWithMovies()
    {
        // Solution
        return _context.movies
            .Select(m => m.directorId)
            .Distinct()
            .Count();
    }

    // 19. Create a dictionary of movie ID as key and "Title by DirectorName" as value.
    public Dictionary<int, string> GetMovieDictionary()
    {
        // Recommended (Efficient) Solution
        return _context.movies
            .Join(_context.directors,
                movie => movie.directorId,
                director => director.id,
                (movie, director) => new { movie.id, movie.title, director.name })
            .ToDictionary(x => x.id, x => $"{x.title} by {x.name ?? "Unknown"}");

        // Alternative (Inefficient) Solution
        // var dictionary = new Dictionary<int, string>();
        // foreach (var movie in _context.movies)
        // {
        //     var directorName = _context.directors.FirstOrDefault(d => d.id == movie.directorId)?.name;
        //     dictionary[movie.id] = $"{movie.title} by {directorName}";
        // }
        // return dictionary;

        // Analysis: The `ToDictionary` method is the clean, single-pass, and highly optimized way
        // to convert a LINQ query result into a dictionary.
        // The inefficient solution faces the N + 1 problem.
    }

    /// <summary>
    /// A Note on the 'dynamic' Keyword
    /// 
    /// You'll notice that some of the methods return `IEnumerable<dynamic>`. This is used as a convenience to return a collection of 
    /// anonymous types (objects without a formal class name) that are created by the `Select` operator.
    ///
    /// It's important to remember that C# is a statically typed language. The `dynamic` keyword is a special feature that allows C#
    /// to defer type checking to runtime, primarily for scenarios where the object's structure isn't known at compile time.
    ///
    /// While it works for these session tasks, the professional best practice is to create a dedicated class or `record` to hold the data,
    /// ensuring full compile-time type safety for your application. For example:
    ///
    /// public record MovieDetails(string Title, string DirectorName);
    ///
    /// // Then, the method would return IEnumerable<MovieDetails>
    /// public IEnumerable<MovieDetails> GetMovieTitlesWithDirectorNames()
    /// {
    ///     return _context.movies
    ///         .Join(_context.directors, ...)
    ///         .Select((movie, director) => new MovieDetails(movie.title, director.name));
    /// }
    /// </summary>
}

class Program
{
    public static void Main(string[] args)
    {
        var task = new LinqTask();
    }
}