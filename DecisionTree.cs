using Newtonsoft.Json;

namespace GuessGame
{
    public class DecisionTreeNode
    {
        [JsonProperty("isQuestion")]
        public bool IsQuestion { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("no")]
        public DecisionTreeNode Left { get; set; }

        [JsonProperty("yes")]
        public DecisionTreeNode Right { get; set; }


        public DecisionTreeNode(string newValue, bool IsQuestion)
        {
            this.Value = newValue;
            this.IsQuestion = IsQuestion;
        }

        public void AddNewProfession(DecisionTreeNode newNode, bool isRight)
        {
            if (isRight)
            {
                DecisionTreeNode tempNode = this.Right;
                this.Right = newNode;
                if (newNode.Left == null)
                {
                    newNode.Left = tempNode;
                }
                else
                {
                    newNode.Right = tempNode;
                }
            }
            else
            {
                DecisionTreeNode tempNode = this.Left;
                this.Left = newNode;
                if (newNode.Left == null)
                {
                    newNode.Left = tempNode;
                }
                else
                {
                    newNode.Right = tempNode;
                }
            }
        }

        public static void SaveToJson(DecisionTreeNode decisionTree)
        {
            string json = JsonConvert.SerializeObject(decisionTree, Formatting.Indented);
            File.WriteAllText("decisionTree", json);
        }

        public static DecisionTreeNode LoadTree()
        {
            DecisionTreeNode node;
            try
            {
                var json = File.ReadAllText("decisionTree");
                node = JsonConvert.DeserializeObject<DecisionTreeNode>(json);
            }
            catch
            {
                node = new DecisionTreeNode("Любитель дебажить код по 20 часов?", true)
                {
                    Left = new DecisionTreeNode("Слесарь", false),
                    Right = new DecisionTreeNode("Программист", false),
                    IsQuestion = true,
                };
            }
            return node;
        }

        public void PrintToConsole(string indent = "")
        {
            Console.WriteLine(indent + (IsQuestion ? "Вопрос: " : "Профессия: ") + Value);

            if (Left != null)
            {
                Console.Write(indent + "  Нет: ");
                Left.PrintToConsole(indent + "    ");
            }

            if (Right != null)
            {
                Console.Write(indent + "  Да: ");
                Right.PrintToConsole(indent + "    ");
            }
        }
    }
}