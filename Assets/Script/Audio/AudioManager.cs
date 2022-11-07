using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;
    // Sound source for sound effects
    public AudioSource soundSource;
    // Sound source for soundtrack
    public AudioSource musicSource;
    // Soundtrack
    public AudioClip track;
    // Wave start sfx
    public AudioClip waveStart;
    // Enemy reached capture point sfx
    public AudioClip captured;

    // Player click UI sfx
    public AudioClip uiClick;
    // Defeat sfx
    public AudioClip defeat;

    // Die sfx is played now
    private bool dieCoroutine = false;


    void OnEnable()
    {
        instance = this;
        EventManager.StartListening("GamePaused", GamePaused);
        EventManager.StartListening("WaveStart", WaveStart);
        EventManager.StartListening("Captured", Captured);

        EventManager.StartListening("UserUiClick", UserUiClick);

        EventManager.StartListening("Defeat", Defeat);

    }

    void OnDisable()
    {
        EventManager.StopListening("GamePaused", GamePaused);
        EventManager.StopListening("WaveStart", WaveStart);
        EventManager.StopListening("Captured", Captured);

        EventManager.StopListening("UserUiClick", UserUiClick);

        EventManager.StopListening("Defeat", Defeat);

    }
    void Start()
    {
        Debug.Assert(soundSource && musicSource, "Wrong initial settings");
        // Set volume from stored configurations
        //SetVolume(DataManager.instance.configs.soundVolume, DataManager.instance.configs.musicVolume);

    }
    void OnDestroy()
    {
        StopAllCoroutines();
        if (instance == this)
        {
            instance = null;
        }
    }
    private void GamePaused(GameObject obj, string param)
    {
        if (param == bool.TrueString) // Paused
        {
            // Pause soundtrack
            musicSource.Pause();
        }
        else // Unpaused
        {
            // Play soundtrack
            if (track != null)
            {
                musicSource.clip = track;
                musicSource.Play();
            }
        }
    }
    public void SetVolume(float sound, float music)
    {
        soundSource.volume = sound;
        musicSource.volume = music;
    }
    public void PlaySound(AudioClip audioClip)
    {
        soundSource.PlayOneShot(audioClip, soundSource.volume);
    }

    public void PlayDie(AudioClip audioClip)
    {
        if (dieCoroutine == false)
        {
            StartCoroutine(DieCoroutine(audioClip));
        }
    }
    private IEnumerator DieCoroutine(AudioClip audioClip)
    {
        dieCoroutine = true;
        PlaySound(audioClip);
        // Wait for clip end
        yield return new WaitForSeconds(audioClip.length);
        dieCoroutine = false;
    }
    private void WaveStart(GameObject obj, string param)
    {
        if (waveStart != null)
        {
            PlaySound(waveStart);
        }
    }
    private void Captured(GameObject obj, string param)
    {
        if (captured != null)
        {
            PlaySound(captured);
        }
    }
    private void UserUiClick(GameObject obj, string param)
    {
        if (obj != null)
        {
            PlaySound(uiClick);
        }
    }
    private void Defeat(GameObject obj, string param)
    {
        if (defeat != null)
        {
            PlaySound(defeat);
        }
    }
}
