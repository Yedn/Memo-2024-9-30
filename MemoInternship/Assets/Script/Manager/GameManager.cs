using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GameResult { Unknow,Win,Lose}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public List<Tilemap> BackGroundList;
    public float GameTime = 20.0f;
    public GameResult gameResult = GameResult.Unknow;
    public int totalTime = 20*60;
    public int restTime = 0;
    public TextMeshProUGUI WinText;

    public GameObject player;
    private Vector3 playerpos;
    //public float PlayerWH;
    //public float TerrainWH;
    public GameObject MapPrefab;

    //private Dictionary<Vector2, GameObject> showDic = new Dictionary<Vector2, GameObject>();
    //private Queue<GameObject> pool = new Queue<GameObject>();


    private void Awake()
    {
        instance = this;
        restTime = totalTime;
        player = GameObject.FindWithTag("Player");
        playerpos = player.transform.position;
    }

    void CreateBackGround()
    {

        //if (playerpos != player.transform.position)
        //{
        //    //需要显示的列表
        //    List<Vector2> showlist = new List<Vector2>();
        //    //创建玩家区域
        //    Rect playerRect = new Rect(player.transform.position.x, player.transform.position.z, PlayerWH, PlayerWH);
        //    //获取玩家所在
        //    int x = (int)(player.transform.position.x / TerrainWH);
        //    int z = (int)(player.transform.position.z / TerrainWH);
        //    showlist.Add(new Vector2(x, z));
        //    //右
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, z * TerrainWH, TerrainWH, TerrainWH)));
        //    {
        //        showlist.Add(new Vector2(x + 1, z));
        //    }
        //    //左
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, z * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z));
        //    }
        //    //前
        //    if (IsLap(playerRect, new Rect(x * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x, z + 1));
        //    }
        //    //后
        //    if (IsLap(playerRect, new Rect(x * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x, z - 1));
        //    }
        //    //右前
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x + 1, z + 1));
        //    }
        //    //左前
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z + 1));
        //    }
        //    //右后
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x + 1, z - 1));
        //    }
        //    //左后
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z - 1));
        //    }
        //    //需要删掉的集合
        //    List<Vector2> deslist = new List<Vector2>();
        //    //从正在显示的里面找到不需要显示的
        //    foreach (var item in showDic.Keys)
        //    {
        //        if (!showlist.Contains(item))
        //        {
        //            //隐藏并存入对象池
        //            showDic[item].SetActive(false);
        //            pool.Enqueue(showDic[item]);
        //            deslist.Add(item);
        //        }
        //    }
        //    //从字典中删除
        //    foreach (var item in deslist)
        //    {
        //        showDic.Remove(item);
        //    }
        //    //找到需要显示但没显示的
        //    foreach (var item in showlist)
        //    {
        //        if (!showDic.ContainsKey(item))
        //        {
        //            GameObject terrain;
        //            if (pool.Count > 0)
        //            {
        //                terrain = pool.Dequeue();
        //                terrain.SetActive(true);
        //            }
        //            else
        //            {
        //                terrain = Instantiate(MapPrefab);
        //            }

        //            terrain.transform.position = new Vector3(item.x * TerrainWH, 0, item.y * TerrainWH);
        //            showDic.Add(item, terrain);
        //        }
        //    }
        //}
        //playerpos = player.transform.position;
    }

    //public bool IsLap(Rect a, Rect b)
    //{
    //    float aMinX = a.x - a.width / 2;
    //    float aMaxX = a.x + a.width / 2;
    //    float aMinZ = a.y - a.height / 2;
    //    float aMaxZ = a.y + a.height / 2;

    //    float bMinX = b.x - b.width / 2;
    //    float bMaxX = b.x + b.width / 2;
    //    float bMinZ = b.y - b.height / 2;
    //    float bMaxZ = b.y + b.height / 2;
    //    if (aMinX < bMaxX &&
    //        bMinX < aMaxX &&
    //        aMinZ < bMaxZ &&
    //        bMinZ < aMaxZ)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    public void GameWin()
    {
        UIManager.Instance.ShowWinText();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerstate = Playerstate.pause;
    }

    public void GameLose()
    {
        UIManager.Instance.ShowLoseText();
    }
    public void Update()
    {
        CreateBackGround();
    }
}
