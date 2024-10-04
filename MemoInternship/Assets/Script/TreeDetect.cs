using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDetect : MonoBehaviour
{
    public GameObject tree;
    public Tree getTree;

    public CircleCollider2D ccollider;
    private void Start()
    {
        ccollider = GetComponent<CircleCollider2D>();
    }
    public void SetRadius(float radius)
    {
        ccollider.radius = radius;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (getTree.treeState == TreeState.Close && collision.tag == "Player")
        {
            getTree.treeState = TreeState.Open;
            tree.GetComponent<Collider2D>().enabled = true;
            getTree.GetComponent<Animator>().SetBool("Open", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (getTree.treeState == TreeState.Open && collision.tag == "Player")
        {
            getTree.treeState = TreeState.Close;
            tree.GetComponent<Collider2D>().enabled = false;
            getTree.GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
