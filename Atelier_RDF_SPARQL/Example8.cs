using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using VDS.RDF;
using VDS.RDF.Query;

namespace Atelier_RDF_SPARQL;


// Exemple 8: Get the number of movies in each genre
public static class Example8
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

        // SPARQL query to get the count of movies in each genre
        // the STR function is just to cast the COUNT result to a string,
        // so that the datatype information is not included in the output
        string query = @"
            PREFIX movie: <http://example.org/movies#>
            PREFIX genre: <http://example.org/genres#>

            SELECT ?name (STR(COUNT(?movie)) as ?nbr)
            WHERE {

                ?movie movie:hasGenre ?genre.
                ?genre genre:name ?name.

            }
            GROUP BY ?name";


        // Execute the SPARQL query on the loaded RDF document
        SparqlResultSet resultSet = graph.ExecuteQuery(query) as SparqlResultSet;

        // Iterate over the results and print the genres as well as the number of movies in each.
        foreach (SparqlResult result in resultSet)
        {
            Console.WriteLine(result["name"].ToString() + " nbrFilms: " + result["nbr"]);
        }
    }
}
