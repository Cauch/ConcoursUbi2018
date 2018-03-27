﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class PickupObject : NetworkBehaviour {
    bool isCarryingObject = false;
    GameObject pickableObject;
    GameObject carriedObject;
    GameObject droppedObject;
    public Transform anchorPoint;
    private HintUI hintUI;
    private Player _player;

    private bool isPickingUp = false;

    private float oldObjectMass = 1.0f;
    private RigidbodyConstraints rigidbodyConstraints;

    private void Start()
    {
        hintUI = FindObjectOfType<HintUI>();
        _player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
		if(isPickingUp && isCarryingObject)
        {
            ThrownableObject thrownable = carriedObject.GetComponentInParent<ThrownableObject>();
            if (thrownable != null && thrownable.IsInThrownZone)
            {
                hintUI.Display(Controls.Y, "Throw in the garbage");
            }
            else
            {
                hintUI.Display(Controls.Y, "Drop the object");
            }

            Carry(carriedObject);
            Drop();
        }
        else
        {
            Pickup();
        }
	}

    void Carry(GameObject obj)
    {
        obj.transform.position = anchorPoint.position;
        obj.transform.rotation = anchorPoint.rotation;      
    }

    public void PickingUpAnimation()
    {
        isPickingUp = true;
    }

    void Pickup()
    {
        if (Input.GetButtonDown("Y"))
        {
            if(pickableObject != null)
            {
                isCarryingObject = true;
                carriedObject = pickableObject;
                _player.ChangeState(StateEnum.GRABBING);

                StartCoroutine(WaitForPickUp());
                CmdDisableRigidBody(carriedObject);
            }
        }
    }

    public IEnumerator WaitForPickUp()
    {
        yield return new WaitForSeconds(0.75f);
        PickingUpAnimation();
    }

    public void InsertKeyInDoor()
    {
        isCarryingObject = false;
        carriedObject = null;
    }

    void Drop()
    {   
        if (Input.GetButtonDown("Y"))
        {
            droppedObject = carriedObject;
            CmdEnableRigidBody(droppedObject);
            
            ThrownableObject thrownable = carriedObject.GetComponentInParent<ThrownableObject>();
            if (thrownable != null && thrownable.IsInThrownZone)
            {
                thrownable.ThrowAway();
                hintUI.Hide();
            }
            isCarryingObject = false;
            isPickingUp = false;
            carriedObject = null;
        }
    }

    [Command]
    void CmdDisableRigidBody(GameObject gameObj)
    {
        RpcDisableRigidBody(gameObj);
    }
    
    [Command]
    void CmdEnableRigidBody(GameObject gameObj)
    {
        RpcEnableRigidBody(gameObj);
    }
    
    [ClientRpc]
    void RpcDisableRigidBody(GameObject gameObject)
    {
        Rigidbody pickableRigidBody = gameObject.GetComponent<Rigidbody>();
        if (pickableRigidBody != null)
        {
            oldObjectMass = pickableRigidBody.mass;
            rigidbodyConstraints = pickableRigidBody.constraints;
            Destroy(pickableRigidBody);
        }

        pickableObject.GetComponents<Collider>().Where((c) => !c.isTrigger).First().enabled = false;
    }
    
    [ClientRpc]
    void RpcEnableRigidBody(GameObject gameObj)
    {
        if(gameObj != null)
        {
            gameObj.GetComponents<Collider>().Where((c) => !c.isTrigger).First().enabled = true;
            gameObj.AddComponent<Rigidbody>();
    
            Rigidbody carriedNewRigidBody = gameObj.GetComponent<Rigidbody>();
            carriedNewRigidBody.useGravity = true;
            carriedNewRigidBody.mass = oldObjectMass;
            carriedNewRigidBody.constraints = rigidbodyConstraints;

            gameObj = null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (GetComponent<ObjectSync>().hasAuthority && collider.gameObject.CompareTag("PickableObject"))
        {
            hintUI.Display(Controls.Y, "Pick up " + collision.gameObject.name);
            pickableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (GetComponent<ObjectSync>().hasAuthority && collider.gameObject.CompareTag("PickableObject"))
        {
            hintUI.Hide();
            pickableObject = null;
        }
    }
}
