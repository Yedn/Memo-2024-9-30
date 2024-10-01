using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<Tilemap> BackGroundList;
    public float GameTime = 20.0f;
    private void Awake()
    {
        instance = this;
    }

    void CreateBackGround()
    {

    }
    public void Update()
    {

    }
}
