using System;

namespace ConsoleProgressbar;

public class Progressbar
{
    public long MaxValue { get => _maxValue; private set => _maxValue = value; }
    public long ActualValue { get => _actualValue; private set => _actualValue = value; }
    public (int left, int top) ProgressBarCursorPosition { get; set; }

    private long _maxValue;
    private long _actualValue;
    private readonly ProgressbarOptions _options;

    public Progressbar() : this(100) { }
    public Progressbar(long maxValue) : this(maxValue, 0) { }
    public Progressbar(long maxValue, int actualValue) : this (maxValue, actualValue, new ProgressbarOptions()) { }
    public Progressbar(long maxValue, long actualValue, ProgressbarOptions options)
    {
        _maxValue = maxValue;
        _actualValue = actualValue;
        _options = options;

        ProgressBarCursorPosition = Console.GetCursorPosition();
    }

    public void SetValue(long value)
    {
        if (value > MaxValue) value = MaxValue;

        ActualValue = value;
        Print();
    }
    public void StepOver() => SetValue(++ActualValue);
    public void StepOver(long value) => SetValue(ActualValue + value);

    public void SetProgressBarCursorPosition(int row, int column) => ProgressBarCursorPosition = (column, row);

    public void Print()
    {
        const char stepChar = ' ';  //carattere da stampare per rappresentare la progressbar

        //salvo lo stato del terminale prima che inizi a stampare
        var previousCP = Console.GetCursorPosition();
        var previousFgColor = Console.ForegroundColor;
        var previousBgColor = Console.BackgroundColor;
        Console.SetCursorPosition(ProgressBarCursorPosition.left, ProgressBarCursorPosition.top);

        //mi calcolo in anticipo tutti i valori necessari alla stampa della stringa
        double perc = (_actualValue / Convert.ToDouble(_maxValue)) * 100.0;
        int ProgressValue = (int)Math.Round((((double)_options.Lenght) / 100.0) * perc);

        string progresStr = $"{ActualValue}/{MaxValue}";
        int middleProgressCharIndex = (int)Math.Round((_options.Lenght / 2.0) + 0.01);                                  //carattere che si trova al centro della progress bar
        int startingCharProgressIndex = middleProgressCharIndex - (int)Math.Round((double)(progresStr.Length / 2));     //indice a partire dal quale dovrò iniziare a scrivere la stringa dentro la barra progressiva
        int progressStringIndex = 0;                                                                                    //indice del carattere della stringa che devo scrivere
        for (int i = 0; i < _options.Lenght; i++)
        {
            bool needToPrintText = (i >= startingCharProgressIndex) && (i < startingCharProgressIndex + progresStr.Length);
            needToPrintText &= _options.ShowText;

            char charToPrint = needToPrintText ? progresStr[progressStringIndex++] : stepChar;

            if (i < ProgressValue)
            {
                Console.ForegroundColor = _options.CompletedForegroundColor;
                Console.BackgroundColor = _options.CompletedBackgroundColor;
            }
            else
            {
                Console.ForegroundColor = _options.UncompletedForegroundColor;
                Console.BackgroundColor = _options.UncompletedBackgroundColor;
            }

            Console.Write(charToPrint);
        }
        Console.Write('\n');

        //ripristino lo stato del terminale a prima della stampa
        Console.ForegroundColor = previousFgColor;
        Console.BackgroundColor = previousBgColor;
        Console.SetCursorPosition(previousCP.Left, previousCP.Top);
    }
}