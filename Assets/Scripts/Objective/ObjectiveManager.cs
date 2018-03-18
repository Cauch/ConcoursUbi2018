using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SyncListObjectives : SyncListStruct<Objective> { };

public class ObjectiveManager : NetworkBehaviour, INotifyPropertyChanged
{
    public List<Objective> Objectives;

    //private List<Objective> _currentObjectives;
    //public List<Objective> CurrentObjectives
    //{
    //    get { return _currentObjectives; }
    //    set { _currentObjectives = value; PropertyChanged(this, new PropertyChangedEventArgs("CurrentObjectives")); }
    //}

    private SyncListObjectives _currentObjectives;
    public SyncListObjectives CurrentObjectives
    {
        get { return _currentObjectives; }
        set { _currentObjectives = value; PropertyChanged(this, new PropertyChangedEventArgs("CurrentObjectives")); }
    }

    public class SyncListObjectives : SyncListStruct<Objective> { };

    private ObjectiveSync _objectiveSync;

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Use this for initialization
    void Awake()
    {
        GameEssentials.ObjectiveManager = this;
        PropertyChanged = delegate { };
        CurrentObjectives = new SyncListObjectives();
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

    private void Start()
    {
        GameObject localPlayer = GameObject.FindGameObjectWithTag(ConstantsHelper.PlayerGirlTag);

        if(!localPlayer)
        {
            localPlayer = GameObject.FindGameObjectWithTag(ConstantsHelper.PlayerDogTag);

        }
        _objectiveSync = localPlayer.GetComponent<ObjectiveSync>();
    }

    public void Add(Objective objective, bool SendServer = true )
    {
        if (!IsCurrentObjective(objective) && objective.Status == ObjectiveStateEnum.BACKLOG)
        {
            objective.Status = ObjectiveStateEnum.PROGRESS;
            CurrentObjectives.Add(objective);
            Debug.Log("Meme combat mon cul");
        }
    }

    public void RemoveObjective(Objective objective)
    {
        if (IsCurrentObjective(objective))
        {
            CurrentObjectives.Remove(objective);
        }
    }

    public int Complete(Objective objective, bool SendServer = true)
    {
        if (IsCurrentObjective(objective))
        {
            RemoveObjective(objective);
            return objective.Succeed();
        }
        return 0;
    }

    public int Fail(Objective objective, bool SendServer = true)
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
}