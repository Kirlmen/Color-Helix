using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public bool perfectStar;

    private static float z;
    private static Color currentColor;
    private MeshRenderer _meshRenderer;
    [SerializeField] private SpriteRenderer _splash;
    private Animator animator;

    private float height = 0.58f;
    [SerializeField] private float speed = 4;

    private bool move, isRising, gameOver, displayed;
    private float lerpAmount;

    private AudioSource failSound, hitSound, completeSound;

    private void Awake()
    {
        failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        completeSound = GameObject.Find("CompleteSound").GetComponent<AudioSource>();


        _meshRenderer = GetComponent<MeshRenderer>();
        //_splash = transform.GetComponentInChildren<SpriteRenderer>();
        animator = transform.GetComponentInChildren<Animator>();

    }

    void Start()
    {

        move = false;
        SetColor(GameController.Instance.hitColor);
    }

    void Update()
    {
        // z = transform.position.z;
        if (Touch.IsPressing() && !gameOver)
        {
            move = true;
            GetComponent<SphereCollider>().enabled = true;
        }

        //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, z + speed * Time.deltaTime);

        if (move)
        {
            //transform.position = newPosition;
            BallHandler.z += speed * Time.deltaTime;
        }
        displayed = false;
        transform.position = new Vector3(0, height, BallHandler.z);
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Hit"))
        {
            if (perfectStar && !displayed)
            {
                Debug.Log("perfect star true");
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("PERFECT! +" + PlayerPrefs.GetInt("Level") * 2);
            }
            else if (!perfectStar && !displayed)
            {
                Debug.Log("perfect star false");
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("+" + PlayerPrefs.GetInt("Level"));
            }
            hitSound.Play();
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

        if (other.CompareTag("Star"))
        {
            perfectStar = true;

        }
    }


    IEnumerator GameOver()
    {
        failSound.Play();
        gameOver = true;
        _splash.color = currentColor;
        _splash.transform.position = new Vector3(0, 0.7f, BallHandler.z - 0.05f);
        _splash.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        _splash.enabled = true;
        _meshRenderer.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        move = false;
        PlayFlash();
        yield return new WaitForSeconds(1.5f);

        gameOver = false;
        z = 0;
        GameController.Instance.GenerateLevel();
        _splash.enabled = false;
        _meshRenderer.enabled = true;

    }

    IEnumerator PlayNewLevel()
    {
        completeSound.Play();
        Transform mainCamera = Camera.main.transform;
        mainCamera.SetParent(null);
        yield return new WaitForSeconds(1.5f);
        move = false;
        mainCamera.SetParent(this.gameObject.transform);
        mainCamera.transform.localPosition = new Vector3(0, 9.1f, -16.8f);
        PlayFlash();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
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

    private void PlayFlash()
    {
        animator.SetTrigger("Flash");
    }



}
