using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphMaterial : MonoBehaviour {
    public Material M0;
    public Material M1;

    public float MorphTime = 1f;

    public bool IsActive;

    public GameObject go;

    private Renderer _renderer;

    private float _time;

    private void Start()
    {
        _renderer = go.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (IsActive)
        {
            _time += Time.deltaTime;

            _renderer.material.Lerp(M0, M1, Mathf.Clamp(_time / MorphTime, 0f, 1f));

            if (_time > MorphTime)
            {
                _renderer.material = M1;

                Destroy(this);
            }
        }
    }
}
