using System;

namespace ConsoleProgressbar;

public struct ProgressbarOptions
{
    /// <summary>
    /// Number of characters of which the progressbar is composed. Default is 51.
    /// </summary>
    public int Lenght { get; set; }

    /// <summary>
    /// If true print also the progress in this format: "10/35". Default is true
    /// </summary>
    public bool ShowText { get; set; }

    /// <summary>
    /// Color of the terminal foreground for the completed part of the progressbar. Default is black.
    /// </summary>
    public ConsoleColor CompletedForegroundColor { get; set; }

    /// <summary>
    /// Color of the terminal background for the completed part of the progressbar. Default is white.
    /// </summary>
    public ConsoleColor CompletedBackgroundColor { get; set; }

    /// <summary>
    /// Color of the terminal foreground for the uncompleted part of the progressbar. Default is black.
    /// </summary>
    public ConsoleColor UncompletedForegroundColor { get; set; }

    /// <summary>
    /// Color of the terminal background for the uncompleted part of the progressbar. Default is dark gray.
    /// </summary>
    public ConsoleColor UncompletedBackgroundColor { get; set; }

    public ProgressbarOptions()
    {
        Lenght = 51;
        ShowText = true;

        CompletedForegroundColor = ConsoleColor.Black;
        CompletedBackgroundColor = ConsoleColor.White;
        UncompletedForegroundColor = ConsoleColor.Black;
        UncompletedBackgroundColor = ConsoleColor.DarkGray;
    }
}
