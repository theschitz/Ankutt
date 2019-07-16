using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Ankutt
{
    static class Pranks
    {
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
        [DllImport("user32.dll")]
#pragma warning disable IDE1006 // Naming Styles
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
#pragma warning restore IDE1006 // Naming Styles
        public static void PlayRandomWavFile()
        {
            WavFiles wavFiles = new WavFiles();
            PlayWavFile(wavFiles.fileList[GetRandomNumber(wavFiles.fileList.Count)]);
        }
        private static void PlayWavFile(string soundFilePath)
        {
            SoundPlayer player = new SoundPlayer(soundFilePath);
            player.Play();

        }
        public static void PlayRandomWindowsSound()
        {
            SystemSound sound = GetRandomWindowsSound();
            for (int i = 0; i < 10; i++)
            {
                sound.Play();
                Thread.Sleep(500);
            }
        }
        private static SystemSound GetRandomWindowsSound()
        {
            switch (GetRandomNumber(5))
            {
                case 0:
                    return SystemSounds.Exclamation;
                case 1:
                    return SystemSounds.Exclamation;
                case 2:
                    return SystemSounds.Beep;
                case 3:
                    return SystemSounds.Hand;
                case 4:
                    return SystemSounds.Question;
                default:
                    return SystemSounds.Exclamation;
            }
        }
        public static void RandomErrorMessage()
        {
            string[] errorMessages = Program.GetTextFileLines(Properties.Settings.Default.ErrorMessageFilePath);
            string[] error = errorMessages[GetRandomNumber(errorMessages.Length)].Split(';');
            ShowErrorMessage(error[0], error[1]);
        }

        public static void ShowErrorMessage(string message="An expected error occured.", string title="Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }      
        public static void PressCapsLock()
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;            
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
        }
        public static void SlowlyRaiseVolumeToMax()
        {
            for (int i = 0; i < 50; i++)
            {
                VolumeChanger.VolumeUp();
                Thread.Sleep(1000);
            }
            
        }
        public static void ShakeMouseCursor()
        {
            for (int i = 0; i < 50; i++)
            {
                int x = GetRandomNumber(min: 50, max: 100);
                if (GetRandomNumber(100) % 2 == 0)
                {
                    Cursor.Position = new Point(Cursor.Position.X + x, Cursor.Position.Y + 10);
                }
                else
                {
                    Cursor.Position = new Point(Cursor.Position.X - x, Cursor.Position.Y - 10);
                }
                Thread.Sleep(100);
            }
        }
        public static void OpenWebBrowser(string url)
        {
            Process.Start(url);
        }
        public static void OpenRandomWebPage()
        {
            string[] listOfUrls = Program.GetTextFileLines(Properties.Settings.Default.UrlFilePath);
            OpenWebBrowser(listOfUrls[GetRandomNumber(listOfUrls.Length)]);
        }
        public static void KillSpotify()
        {
            Process[] processes = Process.GetProcessesByName("Spotify");
            if (processes.Length > 0)
            {
                for(int i = 0; i < processes.Length; i++)
                {
                    processes[i].Kill();
                }
            }
        }
        public static void RandomGoogle()
        {
            string[] listOfSearchStrings = Program.GetTextFileLines(Properties.Settings.Default.GoogleSearchStrings);
            GoogleThis(listOfSearchStrings[GetRandomNumber(listOfSearchStrings.Length)]);
        }
        public static void GoogleThis(string searchText)
        {
            OpenWebBrowser($"https://www.google.com/search?q={searchText}");
        }
        public static int GetRandomNumber(int max, int min = 0)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}
