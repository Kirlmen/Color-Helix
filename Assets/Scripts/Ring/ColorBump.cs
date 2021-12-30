using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBump : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color color;



    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

    }

    void Start()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        color = GameController.Instance.colors[Random.Range(0, GameController.Instance.colors.Length)];
        _meshRenderer.material.color = color;

    }


    public Color GetColor()
    {
        return this.color;
    }
}
