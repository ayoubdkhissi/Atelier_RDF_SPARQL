using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;

// Example 4: Extract all movies of Leonardo DiCaprio
public static class Example4
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


        // SPARQL query to select titles of movies of Leonardo DiCaprio
        string queryString = @"
            PREFIX movie: <http://example.org/movies#>
            PREFIX actor: <http://example.org/actors#>

            SELECT ?title
            WHERE {
                ?movie movie:hasActor actor:LeonardoDiCaprio.
                ?movie movie:title ?title.
            }
        ";

        // Execute the SPARQL query on the loaded RDF document
        SparqlResultSet resultSet = (graph.ExecuteQuery(queryString) as SparqlResultSet)!;

        // Iterate over the results and print the movie titles
        Console.WriteLine("The movies of Leonardo DiCaprio are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["title"].ToString());
        }
    }
}
