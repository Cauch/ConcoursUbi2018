using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Objective {
    public ObjectiveStateEnum Status;

    public int Id;
    public string Title;
    public string Description;
    public int SuccessPoints;
    public int FailurePoints;

    public int Succeed()
    {
        if (Status == ObjectiveStateEnum.PROGRESS)
        {
            Status = ObjectiveStateEnum.SUCCESS;
            return SuccessPoints;
        }
        return 0;
    }

    public int Fail()
    {
        if(Status == ObjectiveStateEnum.PROGRESS)
        {
            Status = ObjectiveStateEnum.FAIL;
            return FailurePoints;
        }

        return 0;
    }
}
