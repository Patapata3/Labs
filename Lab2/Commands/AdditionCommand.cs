﻿using System;
using ConsoleUI;

namespace Lab2
{
    class AdditionCommand : ICommand
    {
        public string Name => "add";
        public string Help => "Складывает рациональные числа";

        public string[] Synonyms => new string[] { };

        public string Description => "Выводит результат сложения рациональных чисел. " +
                                     "Формат: \"add r1 r2\", где r1 и r2 - рациональные или целые числа";

        public void Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Введено некорректное количество аргументов");
                return;
            }

            Rational r1 = default(Rational);
            Rational r2 = default(Rational);
            bool r1Success;
            bool r2Success;

            try
            {
                r1Success = Rational.TryParse(args[0], out r1);
                r2Success = Rational.TryParse(args[1], out r2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            bool success = r1Success && r2Success;
            if (!r1Success)
            {
                Console.WriteLine("'" + args[0] + "' - не является рациональным числом.");
            }
            if (!r2Success)
            {
                Console.WriteLine("'" + args[1] + "' - не является рациональным числом.");
            }
            if (!success)
            {
                return;
            }

            Console.WriteLine((r1 + r2).ToString());
        }
    }
}
