using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject perfectStar;
    private GameObject wall1, wall2;

    private float rotationZ;
    private float rotationZMax = 180;
    private bool smallWall;

    void Awake()
    {
        wallFragment = Resources.Load("WallFragment") as GameObject;
        perfectStar = Resources.Load("PerfectStar") as GameObject;
    }

    private void Start()
    {
        SpawnWallFragments();
    }


    void Update()
    {

    }



    private void SpawnWallFragments() //makes a ring
    {
        wall1 = new GameObject();
        wall2 = new GameObject();

        wall1.name = "Wall1";
        wall2.name = "Wall2";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);

        wall2.gameObject.tag = "Fail";
        wall2.AddComponent<BoxCollider>();
        wall2.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.85f, 0.2f);
        wall2.GetComponent<BoxCollider>().center = new Vector3(0.46f, 0, 0);

        if (UnityEngine.Random.value <= 0.2 && PlayerPrefs.GetInt("Level") >= 8) smallWall = true;
        if (smallWall)
        {
            rotationZMax = 90;
        }
        else
        {
            rotationZMax = 180;
        }



        for (int i = 0; i < 100; i++)
        {
            GameObject WallF = Instantiate(wallFragment, Vector3.zero, Quaternion.Euler(0, 0, rotationZ));
            rotationZ += 3.6f;

            if (rotationZ <= rotationZMax)
            {
                WallF.transform.SetParent(wall1.transform); //half of the ring. TODO: Half going to be obstacle
                WallF.gameObject.tag = "Hit";
            }
            else { WallF.transform.SetParent(wall2.transform); }
        }
        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);

        if (!smallWall)
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(25).gameObject;
            AddStar(wallFragmentChild);
        }
        else
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(14).gameObject;
            AddStar(wallFragmentChild);
        }


    }

    private void AddStar(GameObject wallFragmentChild)
    {
        GameObject star = Instantiate(perfectStar, transform.position, Quaternion.identity);
        star.transform.SetParent(wallFragmentChild.transform);
        star.transform.localPosition = new Vector3(0.05f, 0.75f, -0.06f);
    }
}
