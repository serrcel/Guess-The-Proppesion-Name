namespace GuessGame
{
    public class GameProcess
    {
        public DecisionTreeNode Root { get; set; }

        public GameProcess()
        {
            Root = DecisionTreeNode.LoadTree();
        }
        public void ShowMenu()
        {
            Console.WriteLine("1) Показать всю базу знаний\n2) Проверить вариант");
        }
        public void ShowAllTree()
        {
            Console.WriteLine("Нажмите любой символ для начала игры или H для отображения всех вариантов");
            var key = Console.ReadKey();
            Console.Clear();
            if (key.Key == ConsoleKey.H)
            {
                Root.PrintToConsole();
                Console.ReadLine();
            }

        }
        public void StartGame()
        {

            Console.WriteLine("Добро пожаловать в игру - \"Угадай профессию\"");
            Console.WriteLine("Отвечайте да (Y) или нет (N) на наводящие вопросы, а я угадаю, кого вы загадали!");

            while (true)
            {
                DecisionTreeNode current = this.Root;
                DecisionTreeNode previous = this.Root;
                var amogus = true;
                var bibabooba = 1;
                while (amogus)
                {
                    if (current.IsQuestion)
                    {
                        Console.WriteLine("\n" + current.Value);
                        Console.WriteLine("Выберете ответ Y/N");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Y)
                        {
                            previous = current;
                            current = current.Right;
                            continue;
                        }
                        if (key.Key == ConsoleKey.N)
                        {
                            previous = current;
                            current = current.Left;
                            continue;
                        }
                        Console.Clear();
                        Console.WriteLine("\nВы нажали что-то не то!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nЯ знаю что это за профессия! Это {current.Value}! Y/N");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Y)
                        {
                            Console.Clear();
                            Console.WriteLine("Супер! Я отгадал!");
                            break;
                        }
                        if (key.Key == ConsoleKey.N)
                        {
                            Console.Clear();
                            Console.WriteLine("Жаль. Какую профессию вы загадали?");
                            var profession = Console.ReadLine();
                            Console.WriteLine("Какой вопрос однозначно описывает эту профессию?");
                            var question = Console.ReadLine();
                            Console.WriteLine("Какой ответ должен быть на этот вопрос? Y/N");
                            key = Console.ReadKey();
                            Console.Clear();
                            if (key.Key == ConsoleKey.Y || key.Key == ConsoleKey.N)
                            {
                                var newQuestionNode = new DecisionTreeNode(question, true);
                                var newProfessionNode = new DecisionTreeNode(profession, false);

                                if (key.Key == ConsoleKey.Y)
                                {
                                    newQuestionNode.Right = newProfessionNode;
                                }
                                else
                                {
                                    newQuestionNode.Left = newProfessionNode;
                                }
                                if (previous.Right == current)
                                {
                                    previous.AddNewProfession(newQuestionNode, true);
                                }
                                else
                                {
                                    previous.AddNewProfession(newQuestionNode, false);
                                }
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы нажали что-то не то!");
                            break;
                        }
                    }
                }
                while (true)
                {
                    DecisionTreeNode.SaveToJson(Root);
                    Console.WriteLine("Сыграем еще раз?");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Y)
                    {
                        break;
                    }
                    if (key.Key == ConsoleKey.N)
                    {
                        Console.WriteLine("До встречи");
                        return;
                    }
                    Console.WriteLine("Вы нажали что-то не то!");
                }
            }
        }
    }
}