using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public List<EnemyClass> enemyList;
    public List<GameObject> MonsterPrefabList;
    public int MonsterNumber;
    // Start is called before the first frame update
    void Start()
    {
        CreateTree();
        CreateMonster();
    }
    public void CreateTree()
    {
        TreeNumber = Random.Range(1, 6);
        for (int i = 0; i < TreeNumber; i++)
        {
            float x1 = Random.Range(-12.5f, 12.5f);
            float y1 = Random.Range(-8.5f, 8.2f);
            foreach (Vector2 prepos in CreatePos)
            {
                Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                while ((x1 <= prepos.x + 1.0f && x1 >= prepos.x - 1.0f) && (y1 <= prepos.y + 1.0f && y1 >= prepos.y - 1.0f) && (x1 <= playerPos.x + 3f && x1 >= playerPos.x - 3f) && (y1 <= playerPos.y + 3f && y1 >= playerPos.y - 3f))
                {
                    x1 = Random.Range(-12.5f, 12.5f);
                    y1 = Random.Range(-8.5f, 8.2f);
                }
            }
            GameObject go = GameObject.Instantiate(treePrefab, new Vector3(x1, y1, 0), Quaternion.identity);
            treeList.Add(go.GetComponent<Tree>());
            CreatePos.Add(new Vector2(x1, y1));
        }
    }

    public void CreateMonster()
    {
        for (int i = 0; i < 3; i++)
        {
            float x1 = 0; float y1 = 0;
            MonsterNumber = Random.Range(2, 8);
            for (int j = 0; j < MonsterNumber; j++)
            {
                int LeftOrRight, UpOrDown;
                int LeftRightUpDown = Random.Range(0, 4);//左右上下
                LeftOrRight = Random.Range(0, 2);
                UpOrDown = Random.Range(0, 2);
                switch (LeftRightUpDown)
                {
                    case 0: x1 = GameObject.FindWithTag("Player").transform.position.x - 9.5f; y1 = Random.Range(-8.5f, +8.5f); break;
                    case 1: x1 = GameObject.FindWithTag("Player").transform.position.x + 9.5f; y1 = Random.Range(-8.5f, +8.5f); break;
                    case 2: y1 = GameObject.FindWithTag("Player").transform.position.y + 5.5f; x1 = Random.Range(-10.0f, +10.0f); break;
                    case 3: y1 = GameObject.FindWithTag("Player").transform.position.y - 5.5f; x1 = Random.Range(-10.0f, +10.0f); break;
                }
                if (i != 1)
                {
                    foreach (Vector2 prepos in CreatePos)
                    {
                        Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                        while (((x1 <= prepos.x + 0.5f && x1 >= prepos.x - 0.5f) && (y1 <= prepos.y + 2.0f && y1 >= prepos.y - 2.0f)) || ((x1 <= playerPos.x + 2.0f && x1 >= playerPos.x - 2.0f) && (y1 <= playerPos.y + 2.0f && y1 >= playerPos.y - 2.0f)))
                        {
                            switch (LeftRightUpDown)
                            {
                                case 0: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 1: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 2: x1 = Random.Range(-10.0f, +10.0f); break;
                                case 3: x1 = Random.Range(-10.0f, +10.0f); break;
                            }
                        }
                    }
                }
                else if (i == 1)//远程怪一定在视野范围内
                {
                    foreach (Vector2 prepos in CreatePos)
                    {
                        Vector2 playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y);
                        while (((x1 <= prepos.x + 0.5f && x1 >= prepos.x - 0.5f) && (y1 <= prepos.y + 2.0f && y1 >= prepos.y - 2.0f)) && !((x1 >= playerPos.x + 4.0f || x1 <= playerPos.x - 4.0f) && (y1 >= playerPos.y + 4.0f || y1 <= playerPos.y - 4.0f)))
                        {
                            switch (LeftRightUpDown)
                            {
                                case 0: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 1: y1 = Random.Range(-8.5f, +8.5f); break;
                                case 2: x1 = Random.Range(-10.0f, +10.0f); break;
                                case 3: x1 = Random.Range(-10.0f, +10.0f); break;
                            }
                        }
                    }
                }
                GameObject go = GameObject.Instantiate(MonsterPrefabList[i], new Vector3(x1, y1, 0), Quaternion.identity);
                enemyList.Add(go.GetComponent<EnemyClass>());
                CreatePos.Add(new Vector2(x1, y1));
            }
        }
    }
}
