using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectiveSync : NetworkBehaviour
{
    public enum Instruction { ADD, FAIL, COMPLETE }

    [SyncVar]
    Objective _syncObjective;

    [SyncVar]
    Instruction _syncInstruction;

    private ObjectiveManager _objectiveManager;

    void Start()
    {
        _objectiveManager = GameObject.FindGameObjectWithTag(ConstantsHelper.ObjectiveManagerTag).GetComponent<ObjectiveManager>();
    }

    void FixedUpdate()
    {
        if (hasAuthority)
        {
            Cmd_SendObjectiveToServer()
        }
    }

    void InputTest()
    {
        if (Input.GetKeyDown("1"))
        {
            syncState = State.Red;
            objMeshRenderer.material.color = Color.red;
        }
        else if (Input.GetKeyDown("2"))
        {
            syncState = State.Blue;
            objMeshRenderer.material.color = Color.blue;
        }
        else if (Input.GetKeyDown("3"))
        {
            syncState = State.Green;
            objMeshRenderer.material.color = Color.green;
        }
    }

    [Command]
    void Cmd_SendObjectiveToServer(Objective objective, Instruction instruction)
    {
        _syncObjective = objective;
        _syncInstruction = instruction;
    }


    [ClientRpc]
    public void Rpc_ReceiveObjective(Objective objective, Instruction instruction)
    {
        switch(instruction)
        {
            case Instruction.ADD:
                _objectiveManager.Add(objective);

                break;
            case Instruction.COMPLETE:
                _objectiveManager.Complete(objective);

                break;
            case Instruction.FAIL:
                _objectiveManager.Fail(objective);

                break;
        }
    }
}