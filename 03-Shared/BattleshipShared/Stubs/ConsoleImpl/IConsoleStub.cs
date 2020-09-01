using System;
using BattleshipShared.Enums;

namespace BattleshipShared.Stubs.ConsoleImpl
{
    public interface IConsoleStub
    {
        void Write(string value);

        void WriteLine(string value);

        void WriteLine();

        string ReadLine();

        void SetConsoleColors(ConsoleColorsType consoleColorsType);

        void SetConsoleColors(bool wasHit);
    }
}