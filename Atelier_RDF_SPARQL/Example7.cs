using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;

// Example 7: Get names of actors who have played in movies released in 2014
public static class Example7
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

        // SPARQL query to select names of actors who have played in movies released in 2014
        SparqlParameterizedString query = new SparqlParameterizedString();
        query.CommandText = @"
            PREFIX movie: <http://example.org/movies#>
            PREFIX actor: <http://example.org/actors#>

            SELECT ?name
            WHERE {
                ?movie movie:released @year .
                ?movie movie:hasActor ?actor.
                ?actor actor:name ?name.
            }";
        query.SetLiteral("year", "2014");

        // Execute the SPARQL query on the loaded RDF document
        SparqlResultSet resultSet = graph.ExecuteQuery(query) as SparqlResultSet;

        // Iterate over the results and print the actor names
        Console.WriteLine("The actors who have played in movies released in 2014 are:\n");
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["name"].ToString());
        }
    }
}
