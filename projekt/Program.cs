using System;

namespace projekt
{
    internal class Program
    {
        public struct OpenQuestion
        {
            public OpenQuestion(string question, string answer)
            {
                Question = question;
                Answer = answer;
            }

            public string Question {get;}
            public string Answer {get;}
        }

        public struct ClosedQuestion
        {
            public ClosedQuestion(string question, string trueAnswer ,string falseAnswerA, string falseAnswerB)
            {
                Question = question;
                TrueAnswer = trueAnswer;
                FalseAnswerA = falseAnswerA;
                FalseAnswerB = falseAnswerB;
            }

            public string Question {get;}
            public string TrueAnswer {get;}
            public string FalseAnswerA {get;}
            public string FalseAnswerB {get;}

        }

        static void Main(string[] args)
        {
            bool continueGame = true;
            int playerPoints = 0;
            string playerInput;

            List<OpenQuestion> openQuestions = new List<OpenQuestion>();
            List<ClosedQuestion> closedQuestions = new List<ClosedQuestion>();

            LoadQuestionsLists(openQuestions, closedQuestions);
        
            Console.WriteLine("Zagrajmy w grę >:D !");
            Console.ReadKey();
            
            while(continueGame)
            {
                Console.Clear();
                Console.WriteLine("Posiadasz " + playerPoints + " punktów. ");

                if(closedQuestions.Count() > 0) // jeśli nadal są pytania zamknięte to wyświetl:
                {
                    Console.WriteLine("1. Zamknięte ");

                    if(openQuestions.Count() > 0)   // jeśli nadal są pytania otwarte to wyświetl:
                        Console.WriteLine("2. Otwarte");
                }
                else if(openQuestions.Count() > 0)  // jeśli zostały tylko pytania otwarte to wyświetl:
                    Console.WriteLine("1. Otwarte");
                else
                    break;

                playerInput = Console.ReadLine();
                
                bool questionResult = true;
                
                if(closedQuestions.Count() > 0 )    // analogicznie jak wyżej - zamiast wyświetlania teraz są podejmowane decyzje w zależności od odpowiedzi gracza
                {
                    if(playerInput == "1")  
                    {
                        questionResult = AskClosedQuestion(closedQuestions);
                        if(questionResult)
                            playerPoints += 1;
                    }

                    if(openQuestions.Count() > 0 && playerInput == "2")
                    {
                        questionResult = AskOpenQuestion(openQuestions);
                        if(questionResult)
                            playerPoints += 5;
                    }
                }
                else if(openQuestions.Count() > 0 && playerInput == "1")
                {
                    questionResult = AskOpenQuestion(openQuestions);
                    if(questionResult)
                        playerPoints += 5;
                }

                continueGame = questionResult;
            }

            if(openQuestions.Count() > 0 || closedQuestions.Count() > 0)    // jeśli nie odpowiedziano na wszystkie pytania
                WriteOutFailureScreen(playerPoints);
            else
                WriteOutWinScreen(playerPoints);

            Console.ReadKey();
        }

        public static void WriteOutFailureScreen(int actualPoints)
        {
            Console.Clear();
            Console.WriteLine("Bardzo się starałeś, lecz z gry wyleciałeś! HAHAHA >:D");
            Console.WriteLine();
            Console.WriteLine("Kończysz grę z następującą ilością punktów: " + actualPoints + ". ");
        }

        public static void WriteOutWinScreen(int actualPoints)
        {
            Console.Clear();
            Console.WriteLine("Gratuluję! Udało Ci się odpowiedzieć poprawnie na wszystkie pytania :>");
            Console.WriteLine();
            Console.WriteLine("Zdobywasz maksymalną ilość punktów! czyli: " + actualPoints + "!");
        }

        public static bool AskOpenQuestion(List<OpenQuestion> openQuestions)
        {
            Console.Clear();
            Random rand = new Random();
            int index = (int) rand.NextInt64(openQuestions.Count());

            Console.WriteLine(openQuestions[index].Question);
            Console.WriteLine();
            string playerAnswer = Console.ReadLine();
            if(playerAnswer.ToLower().Equals(openQuestions[index].Answer.ToLower()))
            {
                openQuestions.Remove(openQuestions[index]);
                return true;
            }
            
            return false;
        }
        
        public static bool AskClosedQuestion(List<ClosedQuestion> closedQuestions)
        {
            Console.Clear();

            Random rand = new Random();
            int index = (int) rand.NextInt64(closedQuestions.Count());

            string content = closedQuestions[index].Question + "\nA. ";
            int seed = rand.Next(1,4);
            string trueAnsferPosition = "0";

            if(seed < 2) // Losowe rozłożenie odpowiedzi -> odpowiedź prawidłowa jest na PIERWSZYM miejscu
            {
                content += closedQuestions[index].TrueAnswer + "\nB. ";
                if(rand.Next(2) > 0)
                    content += closedQuestions[index].FalseAnswerA + "\nC. " + closedQuestions[index].FalseAnswerB;
                else
                    content += closedQuestions[index].FalseAnswerB + "\nC. " + closedQuestions[index].FalseAnswerA;
                trueAnsferPosition = "A";
            }
            else if(seed > 2) 
            {
                content += closedQuestions[index].FalseAnswerA + "\nB. ";
                if(rand.Next(2) > 0)
                {
                    content += closedQuestions[index].TrueAnswer + "\nC. " + closedQuestions[index].FalseAnswerB;   //odpowiedź prawidłowa jest na DRUGIM miejscu
                    trueAnsferPosition = "B";
                }
                else
                {
                    content += closedQuestions[index].FalseAnswerB + "\nC. " + closedQuestions[index].TrueAnswer;   // odpowiedź prawidłowa jest na TRZECIM miejscu
                    trueAnsferPosition = "C";
                }
            }
            else
            {
                content += closedQuestions[index].FalseAnswerB + "\nB. ";
                if(rand.Next(2) > 0)
                {
                    content += closedQuestions[index].FalseAnswerA + "\nC. " + closedQuestions[index].TrueAnswer; // odpowiedź prawidłowa jest na TRZECIM miejscu
                    trueAnsferPosition = "C";
                }
                else
                {
                    content += closedQuestions[index].TrueAnswer + "\nC. " + closedQuestions[index].FalseAnswerA; //odpowiedź prawidłowa jest na DRUGIM miejscu
                    trueAnsferPosition = "B";
                }
            }

            while(true)
            {
                Console.Clear();
                Console.WriteLine(content);
                
                string playerAnswer = Console.ReadLine();
                
                if(playerAnswer.ToLower().Equals(trueAnsferPosition.ToLower()) )
                {
                    closedQuestions.Remove(closedQuestions[index]);
                    return true;
                }
                
                if(playerAnswer.ToLower().Equals("a") || playerAnswer.ToLower().Equals("b") || playerAnswer.ToLower().Equals("c"))
                    return false;
            }
        }

        public static void LoadQuestionsLists(List<OpenQuestion> openQuestions, List<ClosedQuestion> closedQuestions)
        {
            openQuestions.Add(new OpenQuestion(
            "Czy 2 + 2 * 2 == 6 ? Tak/Nie", 
            "Tak"
            ));
            openQuestions.Add(new OpenQuestion(
            "Czy ten projekt powinien być bardziej dopracowany? Tak/Nie", 
            "Tak"
            ));
            openQuestions.Add(new OpenQuestion(
            "Czy to jest kreatywne pytanie? Tak/Nie", 
            "Nie"
            ));
            openQuestions.Add(new OpenQuestion(
            "Czy słowo \"Kot\" ma dokładnie 3 litery? Tak/Nie", 
            "Tak"
            ));

            closedQuestions.Add(new ClosedQuestion(
            "2 / 2 + 2 * 2 = ?",
            "5",
            "6",
            "1/4"
            ));
            closedQuestions.Add(new ClosedQuestion(
            "Znajdź poprawną odpowiedź!",
            "Poprawna odpowiedź",
            "Podchwytliwa odpowiedź",
            "Niepoprawna odpowiedź"
            ));
            closedQuestions.Add(new ClosedQuestion(
            "Gdyby _____ nie skakała to by nóżki nie złamała. ",
            "kózka",
            "kaczka",
            "kobyłka"
            ));
        }

    }
}