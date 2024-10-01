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
        if (collision.tag == "Player")
        {
            Vector2 collisionPosition = new Vector2(collision.GetComponent<Transform>().position.x, collision.GetComponent<Transform>().position.y);
            Vector2 RepelledDirection = collisionPosition - new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
            //collision.GetComponent<PlayerController>().PlayerRepelled(RepelledDirection.normalized);
            collision.GetComponent<PlayerController>().GetHit(atk);
        }
    }


}
