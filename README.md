# Anagram Solver

### High-Level Description
Design a system that will allow the player to enter up to 3 random letters. The system will return the beste, largest, valid English word that can be made up of the 3 letters(blanks/spaces are not allowed). For example: **TCA** should return **CAT**, **OZR** should return **OR**, **ZZA** should return **A**.

**Extension: Expand the project so that it supports 4 letter anagrams(*optional: up to max of 6*)**

### Concept
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

### Flowchart
![Flowchart](https://github.com/codrin-axinte/Anagram-Solver/blob/master/AnagramSolver.png)

### Concept In Depth
* **Database File**
We allow the player the specify a database file to read, otherwise we will use the default one if left blank. This allows the application to be more dynamic. For now the app is working only with **.csv** files.
* **Anagram validation**
  For the anagram validation we will use a regex pattern to solve all at once. The pattern consists in only alphabetics characters A to Z (Upper or Lower case) and the characters length range defined. Minimum 1 character with a maximum of 3. Pattern example `^[a-zA-Z]}{1,3}$`.


### Improving The Algorithm
When working with large database files, having a cache file it can be a big improvement. After the the user inputs a valid anagram, the cache manager will **search for the anagram** in it's file, if it finds a result it will output it, otherwise it will go normally by *reading the database*, *collecting solutions*, *sorting*, *writing to cache* and finally *output* it.
The **cache file** name it's the **database path encrypted**, because this allows each database to have it's own cache file. *Different databases may have different cache values*.
