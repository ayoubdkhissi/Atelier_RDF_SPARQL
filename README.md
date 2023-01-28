<h1 align="center"> Atelier_RDF </h1>

The following code is provided as support for the RDF-SPARQL workshop conducted by me and my collogue <a href="https://github.com/aissameXyz">@aissameXyz<a/> as a part of the Web Semantics course taught by Mr. Khalid Amechnoue during the last semester of 2022/2023 in the GINF3 (Software Engineering) program at the <a href="http://ensat.ac.ma/">National School of Applied Sciences of Tangier</a>.

## Code Structure
The code is organized in a way where each query example is inside a static class named "Example(x)" where x represents the example number. Each class has a static function called "Run" which is used to execute the example. In the main program.cs file, you can choose which example to run by uncommenting the appropriate code and running the program


## The Schema of IMDB.rdf document
![Schema](https://user-images.githubusercontent.com/73041562/215267064-662fe174-c775-4556-b106-5a65435d7d8f.png)


## Notes:
- In VS code you can install the extension <a href="https://marketplace.visualstudio.com/items?itemName=Elsevier.linked-data">Linked Data</a> to visualize the graph of 
any rdf document. It also gives the ability to perform multiple operations on the RDF document such as Running Queries, Converting rdf into turtle(ttl), visualizing queries outputs in tabular format...
