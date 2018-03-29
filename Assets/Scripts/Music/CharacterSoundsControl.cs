using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundsControl : MonoBehaviour {

	public AudioClip[] FootstepsClips;
    public AudioClip[] TalkingClips;

	protected AudioSource[] m_AudioSource;

	// Use this for initialization
	void Awake () {
		m_AudioSource = GetComponents<AudioSource> ();
	}

	protected virtual void Step()
	{
		m_AudioSource[0].PlayOneShot(GetRandomClip (FootstepsClips));
	}

    public virtual void Talk()
    {
        m_AudioSource[1].PlayOneShot(GetRandomClip(TalkingClips));
    }

	protected AudioClip GetRandomClip(AudioClip[] clip)
	{
		return clip[Random.Range (0, clip.Length)];
	}
}
