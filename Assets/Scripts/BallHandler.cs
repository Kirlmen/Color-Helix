using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    private static float z;
    private static Color currentColor;
    private MeshRenderer _meshRenderer;

    private float height = 0.58f;
    private Vector3 newPosition;
    [SerializeField] private float speed = 6;

    private bool move;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        move = false;
    }

    void Update()
    {
        if (Touch.IsPressing()) { move = true; }
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        z = transform.position.z;
        if (move)
        {
            transform.position = newPosition;
            //BallHandler.z += speed * Time.deltaTime;
        }

        //transform.position = new Vector3(0, height, BallHandler.z);
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit"))
        {
            Debug.Log("Broke the wall");
        }

        if (other.CompareTag("FinishLine"))
        {
            Debug.Log("Next Level");
        }

        if (other.CompareTag("Fail"))
        {
            Debug.Log("Failed");
        }
    }

    public static float GetZ()
    {
        return BallHandler.z;
    }

    private void UpdateColor()
    {
        _meshRenderer.material.color = currentColor;
    }

    public static Color SetColor(Color color)
    {
        return currentColor = color;
    }

    public static Color GetColor()
    {
        return currentColor;
    }
}
