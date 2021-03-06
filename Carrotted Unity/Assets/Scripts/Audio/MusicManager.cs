﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour 
{
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicManager>();

                if (_instance == null) Debug.LogError("Missing GameSettings");
                else
                {
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }

    private AudioClip clip;
    private string menu_music = "ambient1";
    private string match_music = "ambient1";


    public void Awake()
    {
        // if this is the first instance, make this the singleton
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            // destroy other instances that are not the already existing singleton
            if (this != _instance)
                Destroy(this.gameObject);
        }

        // first load (once only)
        OnLevelWasLoaded(0);
    }

    public void Update()
    {
        GetComponent<AudioSource>().volume = GameSettings.Instance.volume_music;
    }
    public void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 0: // menu
                clip = Resources.Load(menu_music) as AudioClip;
                break;
            case 1: // match
                if (!GameSettings.Instance.music_on)
                {
                    GetComponent<AudioSource>().Stop();
                    return;
                }
                clip = Resources.Load(match_music) as AudioClip;
                break; 
        }

        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
	
}
