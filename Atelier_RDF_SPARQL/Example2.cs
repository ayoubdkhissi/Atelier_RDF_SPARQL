using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;

// Example 2: Get all movies released in 2014
public static class Example2
{
    public static void Run()
    {
        // Get path of the RDF document (you can use absolute path instead)
        string baseDirectory = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())!
            .Parent!.Parent!.Parent!.FullName;
        string filePath = Path.Combine(baseDirectory, "IMDB.rdf");

        // Load the RDF document into an IGraph object
        IGraph graph = new Graph();
        FileLoader.Load(graph, filePath);

        

        // The query selects the title of movies released in a specific year. 
        // The year is passed as a parameter to the query using the SetLiteral method.
        SparqlParameterizedString query = new SparqlParameterizedString();
        query.CommandText = @"
        PREFIX movie: <http://example.org/movies#>
        SELECT ?title
        WHERE {
            ?movie movie:released @year .
            ?movie movie:title ?title .
        }";
        query.SetLiteral("year", "2014");

        // Execute the SPARQL query on the RDF graph using the ExecuteQuery method of the IGraph object. 
        // The result is casted to a SparqlResultSet object. the ! is to tell the compiler that the result is not null
        SparqlResultSet resultSet = (graph.ExecuteQuery(query) as SparqlResultSet)!;

        // Iterate over the results and print the title of each movie.
        Console.WriteLine("The movies released in 2014 are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["title"].ToString());
        }
    }
}
