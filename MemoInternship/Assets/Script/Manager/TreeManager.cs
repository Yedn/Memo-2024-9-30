using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeManager : MonoBehaviour
{
    public int TreeNumber;
    public GameObject treePrefab;
    public List<Tree> treeList;
    public List<Vector2> treePos;
    // Start is called before the first frame update
    void Start()
    {
        TreeNumber = Random.Range(1, 6);
        CreateTree();
    }
    public void CreateTree()
    {
        for (int i = 0; i < TreeNumber; i++)
        {
            float x1 = Random.Range(-12.5f, 12.5f);
            float y1 = Random.Range(-8.5f, 8.2f);
            foreach (Vector2 prepos in treePos)
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
            treePos.Add(new Vector2(x1, y1));
        }
    }
}
