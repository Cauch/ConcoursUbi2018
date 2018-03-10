using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementMode {
    public static void StrafeMode(Player player, float movementSpeed, float rotationSpeed)
    {
        float HorizontalAxis = Input.GetAxis("Horizontal_Move");

        if (HorizontalAxis >= float.Epsilon || HorizontalAxis <= -float.Epsilon)
        {
            player.RigidBody.AddForce(HorizontalAxis * player.transform.right * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

        float VerticalAxis = Input.GetAxis("Vertical_Move");

        if (VerticalAxis >= float.Epsilon || VerticalAxis <= -float.Epsilon)
        {
            player.RigidBody.AddForce(VerticalAxis * player.transform.forward * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

        float HorizontalRotation = Input.GetAxis("Horizontal_Rotation");

        if (HorizontalRotation != 0)
        {
            player.transform.eulerAngles += new Vector3(0, Time.deltaTime * rotationSpeed * HorizontalRotation, 0);
        }
    }

    public static void ForwardMode(Player player, float movementSpeed, float rotationSpeed)
    {
        float HorizontalAxis = Input.GetAxis("Horizontal_Move");

        if (HorizontalAxis >= float.Epsilon || HorizontalAxis <= -float.Epsilon)
        {
            player.transform.eulerAngles += new Vector3(0, Time.deltaTime * rotationSpeed * HorizontalAxis, 0);
        }

        float VerticalAxis = Input.GetAxis("Vertical_Move");

        if (VerticalAxis >= float.Epsilon || VerticalAxis <= -float.Epsilon)
        {
            player.RigidBody.AddForce(VerticalAxis * player.transform.forward * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        
    }
}
