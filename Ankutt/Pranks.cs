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
            switch(GetRandomNumber(4))
            {
                case 0:
                    PlayWavFile(WavFiles.Tralalala);
                    break;
                case 1:
                    PlayWavFile(WavFiles.BoKo1);
                    break;
                case 2:
                    PlayWavFile(WavFiles.BoKo2);
                    break;
                case 3:
                    PlayWavFile(WavFiles.BoKo3);
                    break;
            }
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
            switch(GetRandomNumber(5))
            {
                case 0:
                    ShowErrorMessage("Kunde inte formatera C:\\. Vänligen försök igen", "Format Error");
                    break;
                case 1:
                    ShowErrorMessage("The domain Efukt.com was blocked by the firewall due to network policy.", "Cisco Firewall");
                    break;
                case 2:
                    ShowErrorMessage("Mother modem could not process instructions: 0x0002 0xAD 0xAF 0x0001", "Internal Error");
                    break;
                case 3:
                    ShowErrorMessage("Please insert disc.", "CD-Rom");
                    break;
                case 4:
                    ShowErrorMessage("Floppy drive is empty.", "A:\\");
                    break;
                default:
                    ShowErrorMessage();
                    break;
            }
        }

        public static void ShowErrorMessage(string message="Ett väntat felet inträffades.", string title="Fel")
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
                int m = GetRandomNumber(100) % 2;
                if (m == 0)
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
            string url = "https://www.google.com/search?q=" + searchText;
            OpenWebBrowser(url);
        }
        public static int GetRandomNumber(int max, int min = 0)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}
