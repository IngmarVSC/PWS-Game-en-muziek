using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    [Header("Music")]
    public AudioClip menuMusic;

    // music files list
    public AudioClip[] gameplayMusic;
    // public AudioClip[] gameplaymusicTracksPlayed;
    

    public string currentMusicType {get; private set; }


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
        
        int randomIndex = Random.Range(0, gameplayMusic.Length);

        musicSource.clip = gameplayMusic[randomIndex];
        musicSource.loop = true;
        musicSource.Play(); 

        switch (randomIndex)
        {
            case 0:
                currentMusicType = "Slow";
                break;

            case 1:
                currentMusicType = "Medium";
                break;

            case 2:
                currentMusicType = "Fast";
                break;

            default:
                currentMusicType = "Unknown";
                break;
        }

    }


    public void StopMusic()
    {
        musicSource.Stop();
    }
}