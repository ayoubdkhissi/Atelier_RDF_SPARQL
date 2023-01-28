using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;
using System.Collections;
using System.Xml.Linq;

namespace Atelier_RDF_SPARQL;

// Example 3: Get names of all actors of the movie "Interstellar"
public static class Example3
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


        // SPARQL query to select names of actors that acted in a movie named 'Interstellar'
        string queryString = @"
          PREFIX movie: <http://example.org/movies#>
          PREFIX actor: <http://example.org/actors#>

          SELECT ?name
          WHERE {
            ?movie movie:title 'Interstellar'.
            ?movie movie:hasActor ?actor.
            ?actor actor:name ?name.
          }
        ";

        // Execute the SPARQL query
        SparqlResultSet resultSet = graph.ExecuteQuery(queryString) as SparqlResultSet;

        // Iterate over the results and print the names of actors
        Console.WriteLine("The actors of the movie 'Interstellar' are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["name"].ToString());
        }
    }
}
