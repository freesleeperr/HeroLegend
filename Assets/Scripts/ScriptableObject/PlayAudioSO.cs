using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/PlayAudioSOEventSO")]
public class PlayAudioSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;
    public void EventRaised(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);
    }
}
