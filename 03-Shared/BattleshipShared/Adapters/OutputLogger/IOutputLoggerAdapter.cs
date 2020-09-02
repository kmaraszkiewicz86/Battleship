using System;
using BattleshipShared.Enums;

namespace BattleshipShared.Adapters.OutputLogger
{
    /// <summary>
    /// Wrap output to print data on user ui
    /// </summary>
    public interface IOutputLoggerAdapter
    {
        /// <summary>
        /// Set backgroud and foreground colors for outpus user ui
        /// </summary>
        /// <param name="wasHit"></param>
        void SetConsoleColors(bool wasHit);

        /// <summary>
        /// Set backgroud and foreground colors for outpus user ui
        /// </summary>
        /// <param name="consoleColorsType"><see cref="ConsoleColorsType"/></param>
        void SetConsoleColors(ConsoleColorsType consoleColorsType);

        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred</exception>
        void Write(string value);

        /// <summary>
        ///     Writes the specified string value, followed by the current line terminator, to
        ///     the standard output stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <exception cref="System.IO.IOException">An I/O error occurred</exception>
        void WriteLine(string value);

        /// <summary>
        /// Writes the current line terminator to the standard output stream.
        /// </summary>
        /// <exception cref="System.IO.IOException">An I/O error occurred</exception>
        void WriteLine();

        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns>The next line of characters from the input stream, or null if no more lines are available.</returns>
        /// <exception cref="System.IO.IOException">An I/O error occurred</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string</exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than System.Int32.MaxValue</exception>
        string ReadLine();

        /// <summary>
        ///     Obtains the next character or function key pressed by the user. The pressed key
        ///     is displayed in the console window.
        /// </summary>
        /// <returns>
        ///     An object that describes the System.ConsoleKey constant and Unicode character,
        ///     if any, that correspond to the pressed console key. The System.ConsoleKeyInfo
        ///     object also describes, in a bitwise combination of System.ConsoleModifiers values,
        ///     whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously
        ///     with the console key.
        /// </returns>
        /// <exception cref="InvalidOperationException">The System.Console.In property is redirected from some stream other than the console</exception>
        ConsoleKeyInfo ReadKey();
    }
}