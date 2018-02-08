using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITile {
    public GameObject gameObject;

    public ITile(GameObject go)
    {
        gameObject = go;
    }

    public void Instantiate(Vector3 position)
    {
        GameObject.Instantiate(gameObject, position, Quaternion.identity);
    }
}
