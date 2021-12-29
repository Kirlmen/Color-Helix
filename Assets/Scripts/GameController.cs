using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] int spawnWallNum = 11;
    private float _z = 7;
    [SerializeField] GameObject finishLine;

    public Color[] colors;
    [HideInInspector] public Color hitColor, failColor;

    private void Awake()
    {
        Instance = this;
        GenerateColors();
    }
    private void Start()
    {
        SpawnWalls();
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
            wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
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
}
