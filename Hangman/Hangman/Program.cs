internal class Program
{
    // The maximum number of guesses
    private static int maxGuesses = 6;
    // Which letters have the user already guessed
    private static List<char> guessedLetters = new();
    // The characters we display to the user
    private static List<char> wordToDisplay = new();
    // Which guess is the user currently on
    private static int currentGuess;
    
    public static void Main(string[] args)
    {
        

        while (true)
        {
            // Which guess is the user currently on
            currentGuess = 0;
            // Which letters have the user already guessed
            guessedLetters = new();
            // The characters we display to the user
            wordToDisplay = new();
            
            Console.Clear();
            Console.WriteLine("-- Welcome to hangman --");
            // Prompt user for if they wanna convert again or quit
            Console.WriteLine("Please select a action!\n\n1. Play\n2. Quit");
            int action = int.Parse(Console.ReadLine());
            if (action == 2) return;
            
            Console.Clear();
            Console.WriteLine("Please enter a word or phrase to guess...");
            string inputPhrase = Console.ReadLine();
            List<char> secretWord = inputPhrase.ToLower().ToList(); // Store secret word

            // Fill display array
            foreach (var letter in secretWord)
            {
                if (letter == ' ')
                {
                    wordToDisplay.Add(' ');
                    continue;
                }
                wordToDisplay.Add('_');
            }

            while (true)
            {
                
                DrawScreen();
                
                // Do input and make sure its lowercase
                var guess = char.ToLower(Console.ReadKey().KeyChar);
                
                // Skip repeat guesses
                if (guessedLetters.Contains(guess))
                {
                    continue;
                }

                guessedLetters.Add(guess);

                // Add to guess count if wrong
                if (!secretWord.Contains(guess))
                {
                    currentGuess++;
                }

                // Replace word to display with right guess
                for (int i = 0; i < secretWord.Count; i++)
                {
                    if (guess == secretWord[i])
                    {
                        wordToDisplay[i] = guess;
                    }
                }

                DrawScreen();

                // https://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                // Used this to make sure lists are equal
                if (wordToDisplay.All(secretWord.Contains))
                {
                    Console.WriteLine("You won!!!!!!!");
                    Thread.Sleep(5000);
                    break;
                }

                if (currentGuess < maxGuesses) continue;
                Console.WriteLine("You lost!!!!!!");
                Thread.Sleep(5000);
                break;
            }
        }
    }

    private static void DrawScreen()
    {
        Console.Clear();
        Console.WriteLine("=========");
        Console.WriteLine("  +---+");
        Console.WriteLine("  |   |");
                
        // Handle the first line of visuals
        if (currentGuess >= 1)
        {
            Console.WriteLine("  0   |");
        }
        else
        {
            Console.WriteLine("      |");
        }
                
        // Handle the second line of visuals
        switch (currentGuess)
        {
            case 0:
            case 1:
                Console.WriteLine("      |");
                break;
            case 2:
                Console.WriteLine("  |   |");
                break;
            case 3:
                Console.WriteLine(" /|   |");
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                Console.WriteLine(@" /|\  |");
                break;
            default:
                Console.WriteLine(@" /|\  |");
                break;
        }
                
        // Handle the third line of visuals
        switch (currentGuess)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                Console.WriteLine("      |");
                break;
            case 5:
                Console.WriteLine(" /    |");
                break;
            case 6:
            case 7:
                Console.WriteLine(@" / \  |");
                break;
            default:
                Console.WriteLine(@" / \  |");
                break;
        }
                
        // Empty line
        Console.WriteLine("      |");
                
        // Handle the second to last line
        Console.Write("=========   ");
        
        string wordToDisplayString = "";
        foreach (var character in wordToDisplay)
        {
            wordToDisplayString += character;
        }
        Console.WriteLine(wordToDisplayString);
                
        // Handle the last line
        Console.Write("Guessed Letters: ");
        foreach (var guessedLetter in guessedLetters)
        {
            Console.Write(guessedLetter + " ");
        }
        Console.WriteLine();
    }
}