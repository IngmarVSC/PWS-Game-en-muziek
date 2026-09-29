using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    [Header("Music")]
    public AudioClip menuMusic;

    // music files list
    public AudioClip[] gameplayMusic;

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

    public void PlayRandomGameplayMusic()
    {
        if (gameplayMusic.Length == 0)
            return;

        // play random song -- could maybe be just a loop
        foreach (AudioClip track in gameplayMusic)
        {
            musicSource.clip = track;
            musicSource.loop = true;
            musicSource.Play();
            
        
        }

        /*
        
        int randomIndex = Random.Range(0, gameplayMusic.Length);

        musicSource.clip = gameplayMusic[randomIndex];
        musicSource.loop = true;
        musicSource.Play(); 
        
        */

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}