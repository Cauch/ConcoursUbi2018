using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> Objectives;
    public List<Objective> CurrentObjectives;

    // Use this for initialization
    void Awake()
    {
        CurrentObjectives = new List<Objective>();
        Objectives = new List<Objective> {
            new Objective
            {
                Id = 0,
                Title = "Avoid hole",
                Description = "Reach the exit of the Mira center parking lot without falling in any hole",
                SuccessPoints = 1000,
                FailurePoints = -100,
            },
            new Objective
            {
                Id = 1,
                Title = "Exit Mira Center parking",
                Description = "Reach the exit of the Mira center parking lot.",
                SuccessPoints = 1000,
                FailurePoints = 0,
            },
            new Objective
            {
                Id = 2,
                Title = "Change traffic light",
                Description = "Press the traffic light button in order to change the traffic lights to red and make cars stop.",
                SuccessPoints = 300,
                FailurePoints = -200,
            }
        };
    }

    public void Add(Objective objective)
    {
        if(!IsCurrentObjective(objective) && objective.Status == ObjectiveStateEnum.BACKLOG)
        {
            objective.Status = ObjectiveStateEnum.PROGRESS;
            CurrentObjectives.Add(objective);
        }
    }

    public void RemoveObjective(Objective objective)
    {
        if (IsCurrentObjective(objective))
        {
            CurrentObjectives.Remove(objective);
        }
    }

    public int Complete(Objective objective)
    {
        if (IsCurrentObjective(objective))
        {
            RemoveObjective(objective);
            return objective.Succeed();
        }
        return 0;
    }

    public int Fail(Objective objective)
    {
        if (IsCurrentObjective(objective))
        {
            RemoveObjective(objective);
            return objective.Fail();
        }
        return 0;
    }

    public bool IsCurrentObjective(Objective objective)
    {
        return CurrentObjectives.Contains(objective);
    }

    [Command]
    void Cmd_AddObjective(Objective toDisplay)
    {
        manager.Rpc_SetObjective(toDisplay);
    }

    [ClientRpc]
    void Rpc_SetObjective(Objective toDisplay)
    {

    }

}