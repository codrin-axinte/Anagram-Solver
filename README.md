# Anagram Solver
> Design a system that will allow the player to enter up to 3 random letters. The system will return the beste, largest, valid English word that can be made up of the 3 letters(blanks/spaces are not allowed). For example: TCA should return CAT, OZR should return OR, ZZA should return A.
> **Extension: Expand the project so that it supports 4 letter anagrams(optional: up to max of 6)**

![Screenshot](https://github.com/codrin-axinte/Anagram-Solver/blob/master/AnagramSolver.png)
It searches for the best valid english word from a given file, matching criterias explained below. It will also provide some other suggestions if found.

## Installation

Windows:

```
> Open in visual studio and compile it.
> Run the executable
```
### Algorithm
* Get the database file path
* Get the angram input from the user
* Validate the input:
	- to have only letters
	- to not have any blank/empty space
	- to have a maximum and minimum length
* We read each line of the database file from a the given path
* At this point we parse the line:
	- Check if the given word length is less or equals the anagram length
	- Check if the given word contains one or more of the anagram characters
	- Add the valid line to the list of solutions
	- Return the all valid solutions as a list
* Check if there are any solutions
* Before we finish we do another step to sort the valid solutions by length and letter order
* Output the best solution
* Output other solutions as suggestions, if exists

## Solve Flowchart
![Anagram Solve Flowchart](https://github.com/codrin-axinte/Anagram-Solver/blob/master/Solve.png)

**Loop: While have lines**
```csharp
...
 _db.ReadLines((line) =>
{
// If one of these conditions is true, then we go to the next line
if (line.Length > anagram.Length || !line.All(anagram.Contains)) return;
// Otherwise we add the line as a possible solution
data.Add(line);
});
...
```

**ParseLine: return word**
```csharp
/// <summary>
/// We split the current line by a comma. Assumming that the format is: value, length (or anything else, it will be just ignored)
/// </summary>
/// <param name="line"></param>
/// <returns></returns>
private string ParseLine(string line) {           
    // We store the first row as lowercase for avoiding any case issues
    var row = line.Split(',');

    // We convert the second row to be able to compare it
    // Never trust the client, we will get the length by code
    // Make the word lower to avoid any error
    return row[0].ToLower();
}
```


## Usage example

The program allows the player to specify a file to read, if left blank, a default database will be used. On the next step the user will be asked to input the anagram until it matches the selected algorithm rules. Finally a solution may be displayed, if was found. In addition if there are other suggestions, those will be displayed too.

### Database File
We allow the player the specify a database file to read, otherwise we will use the default one if left blank. This allows the application to be more dynamic. For now the app is working only with comma separated values(*.csv*): `word,7`. 
*\* For now the second column is redundant and it should not be present.*

### Anagram validation
  For the anagram validation we will use a regex pattern to solve all at once. The pattern consists in only alphabetics characters A to Z (Upper or Lower case) and the characters length range defined. Minimum 1 character with a maximum of 6. Pattern example `^[a-zA-Z]{1,6}$`.

## Caching
When working with large database files, having a cache file it can be a big improvement. After the the user inputs a valid anagram, the cache manager will **search for the anagram** in it's file, if it finds a result it will output it, otherwise it will go normally by *reading the database*, *collecting solutions*, *sorting*, *writing to cache* and finally *output* it.
The **cache file** name it's the **database path encrypted**, because this allows each database to have it's own cache file. *Different databases may have different cache values*.
## Extending
You can create a custom algorithm by extending the `IAlgo` interface. 
* `bool IsValid(string anagram)`: Defines how your anagram is validated. 
* `List<string> Solve(string anagram)`: Implement the solving technique and returning a list of solutions.

## Possible Features
    * Having multiple algorithms
    * Better console menu, choose between algos
    * Command line argument execution
    * GUI(Altough console will be much faster)

## Release History
* 0.1.2
    * Allow the player the specify a database
    * Improved cache techique
* 0.1.1
    * Improved the algorithm
    * Added cache feature
* 0.1.0
    * The first proper release
    * Solve the anagram
    * Output solution suggestions
* 0.0.1
    * Work in progress
    * Parse the csv file

## Meta

Codrin Axinte – [@LinkedIn](https://www.linkedin.com/in/codrin-axinte-93776814b/) – xntcodrin@yahoo.com – loopbytes@yahoo.com

Distributed under the MIT license. See ``LICENSE`` for more information.

[https://github.com/codrin-axinte](https://github.com/codrin-axinte)

## Contributing

1. Fork it (<https://github.com/codrin-axinte/Anagram-Solver/fork>)
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request
