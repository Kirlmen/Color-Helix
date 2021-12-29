using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        if (this.gameObject.tag == "Hit")
        {
            _meshRenderer.material.color = GameController.Instance.hitColor;
        }
        else
        {
            _meshRenderer.material.color = GameController.Instance.failColor;
        }
    }
}
