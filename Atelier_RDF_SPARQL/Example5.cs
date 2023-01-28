using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;

// Example 5: Get titles of movies of genre Science Fiction
public static class Example5
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

        // SPARQL query to select titles of movies of genre Science Fiction
        string queryString = @"
            PREFIX movie: <http://example.org/movies#>
            PREFIX genre: <http://example.org/genres#>

            SELECT ?title
            WHERE {
                ?movie movie:hasGenre genre:ScienceFiction.
                ?movie movie:title ?title.
            }
        ";

        // Execute the SPARQL query on the loaded RDF document
        SparqlResultSet resultSet = (graph.ExecuteQuery(queryString) as SparqlResultSet)!;

        // Iterate over the results and print the movie titles
        Console.WriteLine("The movies of genre Science Fiction are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["title"].ToString());
        }

    }
}
