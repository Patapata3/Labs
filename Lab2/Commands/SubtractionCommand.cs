﻿using System;
using ConsoleUI;

namespace Lab2
{
    class SubtractionCommand: ICommand
    {
        public string Name => "sub";
        public string Help => "Вычисляет разность рациональных чисел";

        public string[] Synonyms => new string[] { };

        public string Description => "Выводит разность рациональных чисел. " +
                                     "Формат: \"sub r1 r2\", где r1 и r2 - рациональные или целые числа";

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
            if (!(r1Success && r2Success))
            {
                Console.WriteLine("Были введены не рациональные/целые числа");
                return;
            }

            Console.WriteLine((r1 - r2).ToString());
        }
    }
}