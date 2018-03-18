using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectiveSync : NetworkBehaviour
{
    public enum Instruction { ADD, FAIL, COMPLETE }

    private ObjectiveManager _objectiveManager;
    public GameObject ObjectiveManagerGameObject;
    void Start()
    {
        _objectiveManager = GameEssentials.ObjectiveManager;
        _objectiveManager.CurrentObjectives.Callback = OnObjectivesChanged;
    }

    private void OnObjectivesChanged(SyncListObjectives.Operation op, int index)
    {
        Debug.Log("What the hell is going on");
    }

    //[Command]
    //public void Cmd_SendObjectiveToServer(Objective objective, Instruction instruction)
    //{
    //    //if (hasAuthority)
    //    //{
    //    //    _syncObjective = objective;
    //    //    _syncInstruction = instruction;
    //    //}
    //}

    //[ClientRpc]
    //public void Rpc_ReceiveObjective(Objective objective, Instruction instruction)
    //{
    //    switch(instruction)
    //    {
    //        case Instruction.ADD:
    //            _objectiveManager.Add(objective, false);
    //            break;

    //        case Instruction.COMPLETE:
    //            _objectiveManager.Complete(objective, false);
    //            break;

    //        case Instruction.FAIL:
    //            _objectiveManager.Fail(objective, false);
    //            break;
    //    }
    //}
}