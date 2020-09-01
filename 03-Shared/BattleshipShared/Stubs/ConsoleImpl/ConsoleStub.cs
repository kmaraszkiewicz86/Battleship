using System;
using BattleshipShared.Enums;

namespace BattleshipShared.Stubs.ConsoleImpl
{
    public class ConsoleStub : IConsoleStub
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void SetConsoleColors(bool wasHit)
        {
            SetConsoleColors(wasHit ? ConsoleColorsType.CheckedField : ConsoleColorsType.UncheckedField);
        }

        public void SetConsoleColors(ConsoleColorsType consoleColorsType)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            switch (consoleColorsType)
            {
                case ConsoleColorsType.Default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case ConsoleColorsType.UncheckedField:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;

                case ConsoleColorsType.CheckedField:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
            }
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}