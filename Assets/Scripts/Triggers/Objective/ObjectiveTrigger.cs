using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectiveTrigger : MonoBehaviour {
    public int ObjectiveId;
    public Objective Objective;
    public ObjectiveStateEnum Status;
    private ObjectiveManager _objectiveManager;

    private void Start()
    {
        _objectiveManager = GameEssentials.ObjectiveManager;
        Objective = _objectiveManager.Objectives.Where(obj => obj.Id == ObjectiveId).First();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (Status)
        {
            case ObjectiveStateEnum.FAIL:
                _objectiveManager.Fail(Objective);
        
                break;
            case ObjectiveStateEnum.SUCCESS:
                _objectiveManager.Complete(Objective);
        
                break;
            case ObjectiveStateEnum.PROGRESS:
                _objectiveManager.Add(Objective);
        
                break;
            default:
                break;
        }
        
    }
}
