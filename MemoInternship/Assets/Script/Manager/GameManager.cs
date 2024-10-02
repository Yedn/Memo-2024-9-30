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
        //    //��Ҫ��ʾ���б�
        //    List<Vector2> showlist = new List<Vector2>();
        //    //�����������
        //    Rect playerRect = new Rect(player.transform.position.x, player.transform.position.z, PlayerWH, PlayerWH);
        //    //��ȡ�������
        //    int x = (int)(player.transform.position.x / TerrainWH);
        //    int z = (int)(player.transform.position.z / TerrainWH);
        //    showlist.Add(new Vector2(x, z));
        //    //��
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, z * TerrainWH, TerrainWH, TerrainWH)));
        //    {
        //        showlist.Add(new Vector2(x + 1, z));
        //    }
        //    //��
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, z * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z));
        //    }
        //    //ǰ
        //    if (IsLap(playerRect, new Rect(x * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x, z + 1));
        //    }
        //    //��
        //    if (IsLap(playerRect, new Rect(x * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x, z - 1));
        //    }
        //    //��ǰ
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x + 1, z + 1));
        //    }
        //    //��ǰ
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, (z + 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z + 1));
        //    }
        //    //�Һ�
        //    if (IsLap(playerRect, new Rect((x + 1) * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x + 1, z - 1));
        //    }
        //    //���
        //    if (IsLap(playerRect, new Rect((x - 1) * TerrainWH, (z - 1) * TerrainWH, TerrainWH, TerrainWH))) ;
        //    {
        //        showlist.Add(new Vector2(x - 1, z - 1));
        //    }
        //    //��Ҫɾ���ļ���
        //    List<Vector2> deslist = new List<Vector2>();
        //    //��������ʾ�������ҵ�����Ҫ��ʾ��
        //    foreach (var item in showDic.Keys)
        //    {
        //        if (!showlist.Contains(item))
        //        {
        //            //���ز���������
        //            showDic[item].SetActive(false);
        //            pool.Enqueue(showDic[item]);
        //            deslist.Add(item);
        //        }
        //    }
        //    //���ֵ���ɾ��
        //    foreach (var item in deslist)
        //    {
        //        showDic.Remove(item);
        //    }
        //    //�ҵ���Ҫ��ʾ��û��ʾ��
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
