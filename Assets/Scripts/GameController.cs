using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region  Singleton
    public static GameController Instance;
    #endregion
    [SerializeField] GameObject finishLine;
    private int spawnWallNum = 12;

    private GameObject[] walls2, walls1;
    public int score;
    private int wallsCount = 0;
    private float _z = 7;
    private bool _colorBump;

    public Color[] colors;
    [HideInInspector] public Color hitColor, failColor;

    private void Awake()
    {
        Instance = this;
        GenerateColors();
        PlayerPrefs.SetInt("Level", 1);
    }
    private void Start()
    {
        GenerateLevel();
    }

    private void Update()
    {
        CollectWalls();
    }

    void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];
        while (hitColor == failColor)
        {
            failColor = colors[Random.Range(0, colors.Length)];
        }

        BallHandler.SetColor(hitColor);
    }

    private void CollectWalls()
    {
        walls1 = GameObject.FindGameObjectsWithTag("Wall1");

        if (walls1.Length > wallsCount)
        {
            wallsCount = walls1.Length;
        }
        if (wallsCount > walls1.Length)
        {
            wallsCount = walls1.Length;
            if (GameObject.Find("Ball").GetComponent<BallHandler>().perfectStar)
            {
                GameObject.Find("Ball").GetComponent<BallHandler>().perfectStar = false;

                score += PlayerPrefs.GetInt("Level") * 2;
            }
            else
            {
                score += PlayerPrefs.GetInt("Level");
            }
        }

    }

    public float GetFinishlineDistance()
    {
        return finishLine.transform.position.z;
    }

    private void SpawnWalls()
    {
        for (int i = 0; i < spawnWallNum; i++)
        {
            GameObject wall;
            if (Random.value <= 0.2 && !_colorBump && PlayerPrefs.GetInt("Level") >= 3) // COLORBUMP chance spawn
            {
                _colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else if (Random.value <= 0.2 && PlayerPrefs.GetInt("Level") >= 7) //3 WALL PREFAB SPAWN
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            }
            else if (i >= spawnWallNum - 1 && !_colorBump && PlayerPrefs.GetInt("Level") >= 3) //COLORBUMP hard spawn
            {
                Debug.Log("Hard Spawned");
                _colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }



            wall.transform.SetParent(GameObject.Find("Helix").transform);
            wall.transform.localPosition = new Vector3(0, 0, _z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation)); //random "roll" rotation to the ring
            _z += 7;


            if (i <= spawnWallNum)
            {
                finishLine.transform.position = new Vector3(0, 0.03f, _z); //last ring's front
            }


        }
    }

    public void GenerateLevel()
    {
        GenerateColors();
        if (PlayerPrefs.GetInt("Level") >= 1 && PlayerPrefs.GetInt("Level") <= 4)
        {
            spawnWallNum = 12;
        }
        else if (PlayerPrefs.GetInt("Level") >= 5 && PlayerPrefs.GetInt("Level") <= 10)
        {
            spawnWallNum = 13;
        }
        else
        {
            spawnWallNum = 14;
        }

        _z = 7;
        DeleteWalls();
        _colorBump = false;
        SpawnWalls();
    }

    private void DeleteWalls()
    {
        walls2 = GameObject.FindGameObjectsWithTag("Fail");
        if (walls2.Length > 1)
        {
            for (int i = 0; i < walls2.Length; i++)
            {
                Destroy(walls2[i].transform.parent.gameObject);
            }
        }
        Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
    }
}


