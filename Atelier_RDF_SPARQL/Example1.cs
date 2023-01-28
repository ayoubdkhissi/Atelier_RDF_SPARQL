using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;
using System.IO;

namespace Atelier_RDF_SPARQL;

// Example 1: Get titles of all movies in the RDF document
public static class Example1
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

        // Write the SPARQL query to select all movie titles
        string queryString = @"
          PREFIX movie: <http://example.org/movies#>
          SELECT ?title
          WHERE {
            ?movie movie:title ?title.
          }
        ";

        // Execute the SPARQL query on the loaded RDF document, the ! is to tell the compiler that the result is not null
        SparqlResultSet resultSet = (graph.ExecuteQuery(queryString) as SparqlResultSet)!;

        // Iterate over the results and print the movie titles
        Console.WriteLine("The movies in the RDF doc are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["title"].ToString());
        }
    }

}
