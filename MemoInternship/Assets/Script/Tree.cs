using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TreeState { Close, Open };

public class Tree : MonoBehaviour
{
    public GameObject tree;
    public GameObject circle;
    public float atk = 0.5f;
    public TreeState treeState;
    public float detectionRadius = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        treeState = TreeState.Close;
        circle.GetComponent<TreeDetect>().SetRadius(detectionRadius);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && treeState == TreeState.Open)
        {
            collision.GetComponent<PlayerController>().GetHit(atk);
        }
    }


}
