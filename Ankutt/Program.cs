using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Ankutt
{
    static class Program
    {
        /// <summary>
        /// Harmless but very anoying pranks.
        /// </summary>
        static readonly int MIN_SLEEP_TIME_MINUTES = Properties.Settings.Default.MinSleepTimeMinutes;
        static readonly int MAX_SLEEP_TIME_MINUTES = Properties.Settings.Default.MaxSleepTimeMinutes;
        static readonly string LOG_PATH = Properties.Settings.Default.LogFilePath;
        
        [STAThread]
        static void Main()
        {
            while (true)
            {
                try
                {
                    Prank();
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                }
                Thread.Sleep(GetSleepyTime());
            }
        }

        static void Prank()
        {
            switch (Pranks.GetRandomNumber(10)) {
                case 0:
                    Console.WriteLine($"Executing: {nameof(Pranks.LockWorkStation)}");
                    Pranks.LockWorkStation();                    
                    Console.WriteLine($"Executing: {nameof(Pranks.PressCapsLock)}");
                    Pranks.PressCapsLock();      
                    break;
                case 1:
                    Console.WriteLine($"Executing: {nameof(Pranks.RandomErrorMessage)}");
                    Pranks.RandomErrorMessage();
                    break;
                case 2:
                    Console.WriteLine($"Executing: {nameof(Pranks.SlowlyRaiseVolumeToMax)}");
                    Pranks.SlowlyRaiseVolumeToMax();
                    break;
                case 3:
                    Console.WriteLine($"Executing: {nameof(Pranks.PressCapsLock)}");
                    Pranks.PressCapsLock();
                    break;
                case 4:
                    Console.WriteLine($"Executing: {nameof(Pranks.PlayRandomWavFile)}");
                    Pranks.PlayRandomWavFile();
                    break;
                case 5:
                    Console.WriteLine($"Executing: {nameof(Pranks.OpenRandomWebPage)}");
                    Pranks.OpenRandomWebPage();
                    break;
                case 6:
                    Console.WriteLine($"Executing: {nameof(Pranks.PlayRandomWindowsSound)}");
                    Pranks.PlayRandomWindowsSound();
                    break;
                case 7:
                    Console.WriteLine($"Executing: {nameof(Pranks.KillSpotify)}");
                    Pranks.KillSpotify();
                    break;
                case 8:
                    Console.WriteLine($"Executing: {nameof(Pranks.RandomGoogle)}");
                    Pranks.RandomGoogle();
                    break;
                case 9:
                    Console.WriteLine($"Executing: {nameof(Pranks.ShakeMouseCursor)}");
                    Pranks.ShakeMouseCursor();
                    break;
            }
        }
        public static int GetSleepyTime()
        {
            int sleep = Pranks.GetRandomNumber(min:MIN_SLEEP_TIME_MINUTES, max:MAX_SLEEP_TIME_MINUTES);
            return sleep * 60 * 1000 ;
        }
        private static void Log(string logText)
        {
            DateTime dt = DateTime.Now;
            string logTextLine = dt.ToString() + ": " + logText;
            if (!File.Exists(LOG_PATH))
            {
                using (StreamWriter sw = File.CreateText(LOG_PATH))
                {
                    sw.WriteLine(logTextLine);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(LOG_PATH))
                {
                    sw.WriteLine(logTextLine);
                }
            }
        }
        public static string[] GetTextFileLines(string filepath)
        {
            try
            {
                string[] textFileLines = File.ReadLines(filepath).ToArray();
                // "#" is used as comment.
                return textFileLines.Where(val => val[0].ToString() != "#").ToArray();
            }
            catch(Exception err)
            {
                Log(err.Message);
            }
            return new string[0];
        }
    }
}
