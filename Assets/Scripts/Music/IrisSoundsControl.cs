using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisSoundsControl : CharacterSoundsControl {

	public AudioClip[] CanneClips;
	public AudioClip PushClip;

	private bool m_IsPushPlaying = false;

	protected override void Step()
	{
		base.Step ();

		if (!m_IsPushPlaying) {
			// Iris canne sound
			m_AudioSource [2].PlayOneShot (GetRandomClip (CanneClips));
		}
    }

    public override void Talk()
    {
        m_AudioSource[1].pitch = 1.5f;
        base.Talk();
    }

    public void StartPush()
	{
		if (m_AudioSource [1].clip != PushClip || !m_IsPushPlaying) {
			m_IsPushPlaying = true;
			m_AudioSource [2].loop = true;
			m_AudioSource [2].clip = PushClip;
			m_AudioSource [2].Play ();
		}
	}

	public void StopPush()
	{
		if (m_IsPushPlaying) {
			m_IsPushPlaying = false;
			m_AudioSource [2].Stop ();
			m_AudioSource [2].loop = false;
		}
	}
}
