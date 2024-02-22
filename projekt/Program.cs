using System;

/*
1. Wyświetlanie tekstu
2. Pobieranie tekstu od graczy
3. Minimum 3 proste instrukcje warunkowe (if / else if / switch)
4. Wykorzystanie przynajmniej 3 zmiennych
5. Wykorzystanie bardziej złożonych warunków
6. Przynajmniej jedna pętla
7. Pobieranie liczb od graczy
8. Warunki z porównywaniem liczb
9. Modyfikowanie wartości zmiennych, przechowujących liczby
10. Wykorzystanie random
11. Stworzenie własnej funkcji
*/

namespace projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueGame = true;
            int playerPoints = 0;
            string playerInput;
        
            Console.WriteLine("Zagrajmy w grę >:D !");
            //Console.ReadKey();
            
            while(continueGame)
            {
                //Console.Clear(); bo wyrzuca dzikie znaki
                WritePoints(playerPoints);
                Console.WriteLine();
                Console.WriteLine("Wybierz rodzaj pytania: ");
                Console.WriteLine("1. Zamknięte ");
                Console.WriteLine("2. Otwarte");
                
                playerInput = Console.ReadLine();
                
                bool questionResult = true;
                
                if(playerInput == "1")
                {
                    questionResult = QuestionClosed("Bardzo sensowne pytanie typu ABC", "B", "Podchwytliwa odpowiedź A", "Prawdziwa odpowiedź B", "Kompletnie pozbawiona sensu odpowiedź C");
                    if(questionResult)
                        playerPoints += 1;
                }
                else if(playerInput == "2")
                {
                    questionResult = QuestionOpen("Bardzo sensowne pytanie wymagające od gracza odpowiedzi \"Okoń\". ", "Okoń");
                    if(questionResult)
                        playerPoints += 1;
                }
                
                continueGame = questionResult;
                
            }
            EndScreen(playerPoints);
            Console.ReadKey();
        }

        public static void EndScreen(int actualPoints)
        {
            //Console.Clear(); bo wyrzuca dzikie znaki
            Console.WriteLine("Bardzo się starałeś, lecz z gry wyleciałeś! HAHAHA >:D");
            Console.WriteLine();
            WritePoints(actualPoints);
        }
    
        public static void WritePoints(int actualPoints)
        {
            Console.WriteLine("Udało Ci się zdobyć " + actualPoints + " punktów!");
        }

        public static bool QuestionOpen(string question, string answer)
        {
            // Console.Clear(); bo wyrzuca dzikie znaki
            Console.WriteLine(question);
            Console.WriteLine();
            string playerAnswer = Console.ReadLine();
            if(playerAnswer.ToLower().Equals(answer.ToLower()))
                return true;
            return false;
        }
        
        public static bool QuestionClosed(string question, string trueABCAnswer ,string answerA, string answerB, string answerC)
        {
            // Console.Clear(); bo wyrzuca dzikie znaki
            Console.WriteLine(question);
            Console.WriteLine("A. " + answerA);
            Console.WriteLine("B. " + answerB);
            Console.WriteLine("C. " + answerC);
            Console.WriteLine();
            string playerAnswer = Console.ReadLine();
            
            if(playerAnswer.ToLower().Equals(trueABCAnswer.ToLower()))
                return true;
            return false;
        }
    }
}