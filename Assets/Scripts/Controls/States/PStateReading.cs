using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStateReading : PlayerState {
    private const string AnimatorAction = "Talking";
    private const string ClipName = "Girl_Reading";

    private const float ReadingTime = 1f;

    private float _time;
    private float _clipLength;

    public PStateReading(Player player) : base(player)
    { 
        foreach (AnimationClip clip in _player.Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == ClipName)
            {
                _clipLength = clip.length;
                break;
            }
        }
        
    }

    public override void InterpretInput()
    {
        _time += Time.deltaTime;
        if (_time > _clipLength)
        {
            _player.ChangeState(StateEnum.GROUNDED);
        }
    }

    public override void OnEnter(object o)
    {
        _player.Animator.SetBool(AnimatorAction, true);
        _time = 0f;
    }

    public override void OnExit()
    {
        _player.Animator.SetBool(AnimatorAction, false);
    }
}
