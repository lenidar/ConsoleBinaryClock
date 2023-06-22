using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBinaryClock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime timeNow = DateTime.Now;
            int[] timeBreakdown = breakDownDateTime(timeNow);
            int[] stringLengths = { 2, 2, 4, 2, 2, 2 };
            string[] formattedStrings = formattedString(timeBreakdown, stringLengths);
            int[] display = new int[14];
            for(int x = 0; x < display.Length; x++)
                display[x] = 0;


            display = arrangeForDisplay(formattedStrings);
            displayClock(display, stringLengths);
            while (true)
            {
                timeNow = DateTime.Now;
                timeBreakdown = breakDownDateTime(timeNow);
                formattedStrings = formattedString(timeBreakdown, stringLengths);
                for (int x = 0; x < display.Length; x++)
                    display[x] = 0;

                display = arrangeForDisplay(formattedStrings);
                displayClock(display, stringLengths);

                Thread.Sleep(1000);
            }
        }

        static int[] breakDownDateTime(DateTime timeNow)
        {
            int[] timeBreakdown = new int[6]; // 0 - Month, 1 - Day, 2 - Year, 3 - Hour, 4 - Min, 5 - Seconds

            timeBreakdown[0] = timeNow.Month;
            timeBreakdown[1] = timeNow.Day;
            timeBreakdown[2] = timeNow.Year;
            timeBreakdown[3] = timeNow.Hour;
            timeBreakdown[4] = timeNow.Minute;
            timeBreakdown[5] = timeNow.Second;

            return timeBreakdown;
        }

        static string intToStringOfCharacters(int value, int length)
        {
            string temp = value.ToString();

            while (temp.Length < length)
                temp = "0" + temp;

            return temp;
        }

        static string[] formattedString(int[] timeBreakdown, int[] stringLengths)
        {
            string[] formattedStrings = new string[timeBreakdown.Length];

            for(int x = 0; x < formattedStrings.Length; x++)
                formattedStrings[x] = intToStringOfCharacters(timeBreakdown[x], stringLengths[x]);

            return formattedStrings;
        }

        static int[] arrangeForDisplay(string[] formattedStrings)
        {
            int[] display = new int[14];
            string temp = "";

            for(int x = 0; x < formattedStrings.Length; x++)
            {
                for(int y = 0; y < formattedStrings[x].Length; y++)
                {
                    temp += formattedStrings[x][y];
                }
            }

            for(int x = 0; x<display.Length; x++)
            {
                display[x] = int.Parse(temp[x] + "");
            }


            return display;
        }

        static void displayClock(int[] display, int[] lengths)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(" MMDDYYYYHHMMSS");

            for(int x = 0; x < 9; x++) 
            {
                Console.SetCursorPosition(0, 1 + x);
                Console.Write(x);
            }

            for(int x = 0; x < display.Length; x++) // columns
            {
                for(int y = 0; y <= 9; y++) // rows
                {
                    Console.SetCursorPosition(x + 1, y + 1);
                    if (y <= display[x])
                        Console.Write("X");
                    else
                        Console.Write(" ");
                }
            }
        }
    }
}
