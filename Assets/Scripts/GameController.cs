using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region  Singleton
    public static GameController Instance;
    #endregion
    [SerializeField] GameObject finishLine;
    [SerializeField] int spawnWallNum = 11;

    private GameObject[] walls2;
    private float _z = 7;
    private bool colorBump;

    public Color[] colors;
    [HideInInspector] public Color hitColor, failColor;

    private void Awake()
    {
        Instance = this;
        GenerateColors();
    }
    private void Start()
    {
        GenerateLevel();
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

    private void SpawnWalls()
    {
        for (int i = 0; i < spawnWallNum; i++)
        {
            GameObject wall;
            if (Random.value <= 0.2 && !colorBump) //chance spawn
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else if (Random.value <= 0.2)
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            }
            else if (i >= GetSpawnNum() && !colorBump) //hard spawn
            {
                colorBump = true;
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
        GetSpawnNum();
        _z = 7;
        DeleteWalls();
        colorBump = false;
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


    private int GetSpawnNum()
    {
        return spawnWallNum;
    }
}
