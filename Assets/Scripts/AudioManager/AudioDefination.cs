using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioSO playAudioEvent;
    public AudioClip audioClip;
    public bool isPlayEnable;
    private void OnEnable()
    {
        if (isPlayEnable)
        {
            PlayAudioClip();
        }
    }
    void PlayAudioClip()
    {
        playAudioEvent.OnEventRaised(audioClip);
    }

}
