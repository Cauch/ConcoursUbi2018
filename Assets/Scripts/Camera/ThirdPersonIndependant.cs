﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonIndependant : ThirdPerson {
    public float DistanceOffset = 5;
    public Vector3 BaseOffsetVector = new Vector3(0,1,0);

    public float VerticalSpeed = 5f;
    public float LateralSpeed = 5f;

    public float ReturnSpeed = 0.0001f;

    public float MaxY = 0.8f;
    
    Vector3 _offsetVector;

	// Update is called once per frame
	void Update () {
        float verticalRotation = Input.GetAxis("Vertical_Rotation");

        if (verticalRotation != 0)
        {
            _offsetVector = Quaternion.AngleAxis(verticalRotation * VerticalSpeed, Vector3.right) * _offsetVector;
            _offsetVector.y = Mathf.Clamp(_offsetVector.y, 0.0f, MaxY);
            _offsetVector.Normalize();
        }

        float horizontalRotation = Input.GetAxis("Horizontal_Rotation");

        if (horizontalRotation != 0)
        {
            _offsetVector = Quaternion.AngleAxis(horizontalRotation * LateralSpeed, Vector3.up) * _offsetVector;
            _offsetVector.Normalize();
        }
        Vector3 offset = (_offsetVector + BaseOffsetVector).normalized*DistanceOffset;

        if(Input.GetButton("ReturnCamera"))
        {
            GoBackToFront();
        }

        this.transform.position = Player.transform.position + offset;
        this.transform.LookAt(Player);
    }

    public override void SetPlayer(Transform Player)
    {
        base.SetPlayer(Player);
        _offsetVector = -Player.forward;
    }

    public void GoBackToFront()
    {
        _offsetVector = Vector3.RotateTowards(_offsetVector, -Player.forward, ReturnSpeed, 0.0f);
    }
}
