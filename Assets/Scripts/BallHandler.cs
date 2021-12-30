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

    private bool move, isRising;
    private float lerpAmount;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        move = false;
        SetColor(GameController.Instance.hitColor);
    }

    void Update()
    {
        // z = transform.position.z;
        if (Touch.IsPressing()) { move = true; }

        //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, z + speed * Time.deltaTime);

        if (move)
        {
            //transform.position = newPosition;
            BallHandler.z += speed * Time.deltaTime;
        }

        transform.position = new Vector3(0, height, BallHandler.z);
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit"))
        {
            Destroy(other.transform.parent.gameObject);
        }
        if (other.tag == "ColorBump")
        {
            lerpAmount = 0;
            isRising = true;
        }

        if (other.CompareTag("FinishLine"))
        {
            StartCoroutine(PlayNewLevel());
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

    IEnumerator PlayNewLevel()
    {
        Transform mainCamera = this.gameObject.transform.GetChild(0);
        mainCamera.SetParent(null);
        yield return new WaitForSeconds(1.5f);
        move = false;
        //Flash
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        mainCamera.SetParent(this.gameObject.transform);
        mainCamera.transform.localPosition = new Vector3(0, 9.1f, -16.8f);
        z = 0;
        GameController.Instance.GenerateLevel();
    }

    public static float GetZ()
    {
        return BallHandler.z;
    }

    private void UpdateColor()
    {
        _meshRenderer.material.color = currentColor;
        if (isRising)
        {
            currentColor = Color.Lerp(_meshRenderer.material.color, GameObject.FindGameObjectWithTag("ColorBump").GetComponent<ColorBump>().GetColor(), lerpAmount);
            lerpAmount += Time.deltaTime;
        }
        if (lerpAmount >= 1)
        {
            isRising = false;
        }
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
