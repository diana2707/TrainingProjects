using System.Linq.Expressions;

// SECTION 1

    // ASSIGNMENT 1

        // Exercise 1
        // Comment this line using a single-line comment.

        // Exercise 2
        /* Comment this and the next line using a multiline comment.
        Visual Studio will show error if we don’t comment normal text.*/

        // Exercise 3
        // Console.WriteLine("Diana");

// SECTION 2

    // ASSIGNMENT 1

        // Exercise 1
        // Write C# code snippet to declare a character and assign value 'C'. 
        // Print the same to the Console.
        // char myChar = 'C';
        // Console.WriteLine(myChar);



        // Exercise 2
        // Write C# code snippet to read a name as string from console
        // and print "Hello, " along with the name to the Console.
        // string? name = Console.ReadLine();
        // Console.WriteLine("Hello, " + name);


        // Exercise 3
        // Rewrite the solution of Exercise 2 to use string interpolation ('$')
        // to combine two strings instead of using the '+' symbol. 
        string? name = Console.ReadLine();
        Console.WriteLine($"Hello, {name}");