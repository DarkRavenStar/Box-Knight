using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource effect;
    public AudioSource bgm;

    public static AudioManager Instance = null;

    private void Start()
    {
        
    }

    // Use this for initialization
    void Awake () {
		if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}
//  play effect sound  
    public void Play(AudioClip clip)
    {
        effect.clip = clip;
        effect.Play();
        Debug.Log("Hit!");
    }
    public void PlayMusic(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }
/*    public void StopAudio(AudioClip clips)
    {
        effect.clip = clips;
        effect.Stop();
    }*/
}
