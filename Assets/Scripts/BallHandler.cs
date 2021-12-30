using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    private static float z;
    private static Color currentColor;
    private MeshRenderer _meshRenderer;

    private float height = 0.58f;
    [SerializeField] private float speed = 4;

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
        // z = transform.position.z;
        if (Touch.IsPressing()) { move = true; }

        //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, z + speed * Time.deltaTime);

        if (move)
        {
            //transform.position = newPosition;
            BallHandler.z += speed * 0.025f;
        }

        transform.position = new Vector3(0, height, BallHandler.z);
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
            StartCoroutine(GameOver());
        }
    }


    IEnumerator GameOver()
    {
        GameController.Instance.GenerateLevel();
        BallHandler.z = 0;
        Vector3 startPos = Vector3.zero;
        move = false;
        yield break;
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
