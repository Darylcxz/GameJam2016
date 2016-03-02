using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class BGM_Manager : MonoBehaviour
{
    // BGM AudioSource
    [SerializeField] AudioSource quietSource;
    [SerializeField] AudioSource activeSource;
    public AudioMixerSnapshot quietShot;
    public AudioMixerSnapshot activeShot;

    // To store a list of Background musics
    public AudioClip[] bgms;

    // To give a different key to each Background Music
    Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();

    void Awake()
    {
        for (byte i = 0; i < bgms.Length; i++)
        {
            // Add a new key to the Dictionary for each new BGM
            if (!bgmDict.ContainsKey(bgms[i].name))
            {
                bgmDict.Add(bgms[i].name, bgms[i]);
            }
        }
    }

    public void QuietTransition()
    {
        quietShot.TransitionTo(2);
    }

    public void ActiveTransition(string bgmName)
    {
        if (activeSource.clip != bgmDict[bgmName])
        {
            activeSource.clip = bgmDict[bgmName];
        }
        activeSource.Play();
        activeShot.TransitionTo(2);
    }
}
