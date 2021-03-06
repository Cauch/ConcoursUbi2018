﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener {

	public enum EaseType : uint{
		Linear = 1,
		Smooth = 2
	}

	private bool _isActive = false;

	private bool _isRepeating = false;
	public bool IsRepeating {
		get { return _isRepeating; }
		set { _isRepeating = value;}
	}

	private bool _stopRepeating = false;
	public bool StopRepeating {
		get { return _stopRepeating; }
		set { _stopRepeating = value;}
	}


	private float _currentTime;
	private EaseType _easeType;
	private float _beginValue;
	private float _endValue;
	private bool _bounceDone;
	private float _currentValue;
	private float _duration; // In seconds

	public Tweener() {
		_easeType = EaseType.Linear;
		_beginValue = 0F;
		_endValue = 0F;
		_duration = 0F;
		_bounceDone = true;
	}

	public Tweener(EaseType easeType, float beginValue, float endValue, float duration) {
		_easeType = easeType;
		_beginValue = beginValue;
		_endValue = endValue;
		_duration = duration;
		_bounceDone = true;
	}

	// Use this for initialization
	public void Start () {
		if (_duration != 0F && _beginValue != _endValue) {
			_isActive = true;
			_currentTime = 0;
			_currentValue = _beginValue;
		}
	}

	public void Stop() {
		_isActive = false;
	}
	
	// Update is called once per frame
	public void Update () {
		if (!_isActive || _duration == 0F)
			return;
		
		_currentTime += Time.deltaTime ;

		if (_currentTime < _duration) {
			Interpolate ();
		} else {
			_currentValue = _endValue;
			_bounceDone = !_bounceDone;
			if (IsRepeating && (!_stopRepeating || !_bounceDone)) {
				_endValue = _beginValue;
				_beginValue = _currentValue;
				_currentTime = 0;
			} else {
				Stop ();
				if (_stopRepeating)
					_stopRepeating = false;
			}
		}
	}

	void Interpolate() {
		float t = _currentTime / _duration;

		switch (_easeType) {
		case EaseType.Smooth:
			t = Smoothstep (t);
			break;
		case EaseType.Linear: 
			t = Linear (t);
			break;
		default:
			break;
		}

		// return c*t/d + b;
		_currentValue = (_endValue - _beginValue) * t + _beginValue;
	}

	float Linear(float t) {
		return t;
	}

	float Smoothstep(float t) {
		return t * t * (3F - 2F * t);
	}

	public void SetBeginValue(float value) {
		_beginValue = value;
	}

	public void SetEndValue(float value) {
		_endValue = value;
	}

	public void SetDuration(float value) {
		_duration = value;
	}

	public void SetEaseType(EaseType easeType) {
		_easeType = easeType;
	}

	public float GetCurrentValue() {
		return _currentValue;
	}

	public bool IsActive() {
		return _isActive;
	}
}
