using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;

// Example 6: retrieve a list of actors who have played in at least one movie of the "Biography" genre
public static class Example6
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

        // SPARQL query retrieve actors who have played in at least one movie of the "Biography" genre
        string queryString = @"
            PREFIX movie: <http://example.org/movies#>
            PREFIX actor: <http://example.org/actors#>
            PREFIX genre: <http://example.org/genres#>

            SELECT ?name
            WHERE {
                ?movie movie:hasGenre genre:Biography.
                ?movie movie:hasActor ?actor.
                ?actor actor:name ?name.
            }
        ";

        // Execute the SPARQL query on the loaded RDF document
        SparqlResultSet resultSet = graph.ExecuteQuery(queryString) as SparqlResultSet;

        // Iterate over the results and print the actor names
        Console.WriteLine("The actors who have played in at least one movie of the Biography genre are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["name"].ToString());
        }
    }
}
