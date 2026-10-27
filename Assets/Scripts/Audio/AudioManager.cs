using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip[] gameplayMusic;

    public string currentMusicType { get; private set; }

    private static int[] shuffledIndices;
    private static int nextTrackIndex = 0;

    void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameplayMusic()
    {
        if (gameplayMusic.Length == 0)
            return;

        // create a new shuffled playlist when needed
        if (shuffledIndices == null ||
            nextTrackIndex >= shuffledIndices.Length)
        {
            ShufflePlaylist();
        }

        int randomIndex = shuffledIndices[nextTrackIndex];
        nextTrackIndex++;

        musicSource.clip = gameplayMusic[randomIndex];
        musicSource.loop = true;
        musicSource.Play();

        switch (randomIndex)
        {
            case 0:
                currentMusicType = "Slow (90 BPM)";
                break;

            case 1:
                currentMusicType = "Medium (120 BPM)";
                break;

            case 2:
                currentMusicType = "Fast (150 BPM)";
                break;

            default:
                currentMusicType = "Unknown";
                break;
        }
    }

    public void ShufflePlaylist()
    {
        shuffledIndices = new int[gameplayMusic.Length];

        // fill array with 0, 1, 2...
        for (int i = 0; i < shuffledIndices.Length; i++)
        {
            shuffledIndices[i] = i;
        }

        // shuffling algorithm
        // sometimes causes last played item to be played again
        for (int i = shuffledIndices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            int temp = shuffledIndices[i];
            shuffledIndices[i] = shuffledIndices[j];
            shuffledIndices[j] = temp;
        }

        nextTrackIndex = 0;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}