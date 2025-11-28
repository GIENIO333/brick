using System.Threading;

namespace BrickGame.Services;

public class SoundService
{
    private bool soundEnabled;
    private Thread musicThread;
    private bool stopMusic;

    public SoundService()
    {
        try
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.Beep(440, 100);
                soundEnabled = true;
            }
            else
            {
                soundEnabled = false;
            }
        }
        catch
        {
            soundEnabled = false;
        }
    }

    public void PlayMenuMusic()
    {
        if (!soundEnabled) return;

        StopMusic();
        musicThread = new Thread(() =>
        {
            while (!stopMusic)
            {
                PlayNotes(new[] {
                    (659, 125), (659, 125), (659, 125), (523, 125), (659, 125), (784, 250),
                    (392, 125), (392, 125), (392, 125), (330, 125), (440, 125), (494, 125), (466, 125)
                });

                if (stopMusic) break;
                Thread.Sleep(500);

                PlayNotes(new[] {
                    (440, 125), (494, 125), (466, 125), (440, 125), (392, 125), (659, 125), (784, 125),
                    (880, 125), (698, 125), (784, 125), (659, 125), (523, 125), (587, 125), (494, 250)
                });

                if (stopMusic) break;
                Thread.Sleep(1000);
            }
        });
        musicThread.IsBackground = true;
        musicThread.Start();
    }

    public void PlayGameMusic()
    {
        if (!soundEnabled) return;

        StopMusic();
        musicThread = new Thread(() =>
        {
            while (!stopMusic)
            {
                PlayNotes(new[] {
                    (523, 150), (587, 150), (659, 150), (698, 150), (784, 300),
                    (784, 150), (698, 150), (659, 150), (587, 150), (523, 300)
                });

                if (stopMusic) break;
                Thread.Sleep(800);
            }
        });
        musicThread.IsBackground = true;
        musicThread.Start();
    }

    public void PlayVictoryMusic()
    {
        if (!soundEnabled) return;

        StopMusic();
        musicThread = new Thread(() =>
        {
            PlayNotes(new[] {
                (523, 200), (659, 200), (784, 200), (1047, 400),
                (784, 200), (1047, 400),
                (1175, 200), (1047, 200), (784, 200), (659, 200), (523, 600)
            });
        });
        musicThread.IsBackground = true;
        musicThread.Start();
    }

    private void PlayNotes((int frequency, int duration)[] notes)
    {
        foreach (var note in notes)
        {
            if (stopMusic) break;
            try
            {
                if (soundEnabled)
                    Console.Beep(note.frequency, note.duration);
            }
            catch
            {
                soundEnabled = false;
                break;
            }
        }
    }

    public void StopMusic()
    {
        stopMusic = true;
        if (musicThread != null && musicThread.IsAlive)
        {
            Thread.Sleep(150);
        }
        stopMusic = false;
    }
}