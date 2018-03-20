﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScoreManager : NetworkBehaviour {

    private NetworkScoreManager scoreManager;

    public void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<NetworkScoreManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ScoreObj bob = new ScoreObj();
            bob.points = 1000;

            Cmd_AddPoints(bob);
        }
    }

    [Command]
    public void Cmd_AddPoints(ScoreObj score)
    {
        scoreManager.Rpc_AddPoints(score);
    }

    [Command]
    public void Cmd_LosePoints(ScoreObj score)
    {
        scoreManager.Rpc_LosePoints(score);
    }
}