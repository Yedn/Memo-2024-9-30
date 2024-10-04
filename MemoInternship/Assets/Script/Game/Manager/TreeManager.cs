using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateManager : MonoBehaviour
{
    public int TreeNumber;
    public GameObject treePrefab;
    public List<Tree> treeList;
    public List<Vector2> CreatePos;

    public Vector3 MapPos;

    public List<GameObject> SameLineMap;     //同一行
    public List<GameObject> SameVerticalMap; //同一列
    public GameObject MainCam;
    public int UnitWidth = 28;
    public int UnitHight = 20;
    public int TotalWidth = 28 * 3;
    public int TotalHeight = 20 * 3;
    public int UnitNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        MapPos = transform.position;
        MainCam = GameObject.FindWithTag("MainCamera");
        CreateTree();
    }
    public void Update()
    {
        MapMove();
    }
    public void CreateTree()
    {
        TreeNumber = Random.Range(1, 6);
        for (int i = 0; i < TreeNumber; i++)
        {
            float x1 = MapPos.x + Random.Range(-12.5f, 12.5f);
            float y1 = MapPos.y + Random.Range(-8.5f, 8.2f);
            foreach (Vector2 prepos in CreatePos)
            {
                Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                while ((new Vector2(x1, y1) - prepos).magnitude < 3.0f || ((new Vector2(x1, y1) - playerPos).magnitude < 3.0f))
                {
                    x1 = MapPos.x + Random.Range(-12.5f, 12.5f);
                    y1 = MapPos.y + Random.Range(-8.5f, 8.2f);
                }
            }
            GameObject go = GameObject.Instantiate(treePrefab, (new Vector3(x1, y1, 0)), Quaternion.identity);
            treeList.Add(go.GetComponent<Tree>());
            CreatePos.Add(new Vector2(x1, y1));
        }
    }

    public void ReCreateTree()
    {
        for (int i = treeList.Count-1; i >=0 ; i--)
        {
            Tree tree = treeList[i];
            treeList.Remove(tree);
            Destroy(tree.gameObject);
        }
        CreatePos.Clear();
        CreateTree();
    }

    public void MapMove()
    {
        Vector3 mapPos = this.transform.position;
        if (MainCam.transform.position.x > this.transform.position.x + TotalWidth / 2)
        {
            foreach (GameObject map in SameVerticalMap)
            {
                Vector3 OtherPos = map.transform.position;
                OtherPos.x += TotalWidth;
                map.transform.position = OtherPos;
                map.GetComponent<CreateManager>().MapPos = map.transform.position;
                map.GetComponent<CreateManager>().ReCreateTree();
            }
        }
        else if (MainCam.transform.position.x < this.transform.position.x - TotalWidth / 2)
        {
            foreach (GameObject map in SameVerticalMap)
            {
                Vector3 OtherPos = map.transform.position;
                OtherPos.x -= TotalWidth;
                map.transform.position = OtherPos;
                map.GetComponent<CreateManager>().MapPos = map.transform.position;
                map.GetComponent<CreateManager>().ReCreateTree();
            }
        }

        if (MainCam.transform.position.y > this.transform.position.y + TotalHeight / 2)
        {
            foreach (GameObject map in SameLineMap)
            {
                Vector3 OtherPos = map.transform.position;
                OtherPos.y += TotalHeight;
                map.transform.position = OtherPos;
                map.GetComponent<CreateManager>().MapPos = map.transform.position;
                map.GetComponent<CreateManager>().ReCreateTree();
            }
        }
        else if (MainCam.transform.position.y < this.transform.position.y - TotalHeight / 2)
        {
            foreach (GameObject map in SameLineMap)
            {
                Vector3 OtherPos = map.transform.position;
                OtherPos.y -= TotalHeight;
                map.transform.position = OtherPos;
                map.GetComponent<CreateManager>().MapPos = map.transform.position;
                map.GetComponent<CreateManager>().ReCreateTree();
            }
        }
    }
}
