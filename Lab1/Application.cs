using System;
using System.Collections.Generic;

namespace Lab1
{
    public class Application
    {
        readonly List<ICommand> commands = new List<ICommand>();
        readonly Dictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();
        readonly NotFoundCommand notFound = new NotFoundCommand();
        private bool keepRunning = true;

        public readonly List<Tuple<Sort, string>> sortMethods = new List<Tuple<Sort, string>>();
        public int[] sequence { get; set; }
        public int iterations = 1;

        public delegate int[] Sort(int[] toSort);

        public void Run()
        {
            sortMethods.Add(new Tuple<Sort, string>(SortingMethods.BubbleSort, "Bubble Sort"));
            sortMethods.Add(new Tuple<Sort, string>(SortingMethods.ShellSort, "Shell Sort"));
            sortMethods.Add(new Tuple<Sort, string>(SortingMethods.HeapSort, "Heap Sort"));
            sortMethods.Add(new Tuple<Sort, string>(SortingMethods.StandardSort, "Array.Sort"));

            AddCommand(new HelpCommand(this));
            AddCommand(new ExitCommand(this));
            AddCommand(new SequenceCommand(this));
            AddCommand(new RandomizeCommand(this));
            AddCommand(new SetIterationsCommand(this));
            AddCommand(new TestingCommand(this));

            while (keepRunning)
            {
                Console.Write("Its your command: ");
                getCommand(Console.ReadLine());
            }
        }

        public ICommand FindCommand(string name)
        {
            if (commandMap.ContainsKey(name))
            {
                return commandMap[name];
            }
            notFound.Name = name;
            return notFound;
        }

        public void Exit()
        {
            keepRunning = false;
        }

        public IList<ICommand> Commands => commands;

        public void AddCommand(ICommand cmd)
        {
            commands.Add(cmd);
            if (commandMap.ContainsKey(cmd.Name))
            {
                throw new Exception(String.Format("������� {0} ��� ���������", cmd.Name));
            }
            commandMap.Add(cmd.Name, cmd);
            foreach (var s in cmd.Synonyms)
            {
                if (commandMap.ContainsKey(s))
                {
                    Console.WriteLine("ERROR: ��������� ������� {0} ��� ������� {1}, ��������� ��� {0}  ��� ������������", s, cmd.Name);
                    continue;
                }
                commandMap.Add(s, cmd);
            }
        }

        public void getCommand(string args)
        {
            if (args == null)
            {
                Console.WriteLine("������ ������� ��� � ������ ��������� ������." +
                    " ������� �� ���������� ��������� ���������� � ���� �� �������� � ������� ������� help.");
                return;
            }

            string[] tokens = args.Split(
                new[] { ' ', '\t' },
                        StringSplitOptions.RemoveEmptyEntries
                );

            if (tokens.Length == 0)
            {
                return;
            }

            string[] parameters = new string[tokens.Length - 1];
            Array.Copy(tokens, 1, parameters, 0, tokens.Length - 1);
            ICommand cmd = FindCommand(tokens[0]);
            cmd.Execute(parameters);
        }
    }
}
