using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    [Header("Music")]
    public AudioClip menuMusic;

    // music files list
    public AudioClip[] gameplayMusic;
    public AudioClip[] gameplaymusicTracksPlayed;

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

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}