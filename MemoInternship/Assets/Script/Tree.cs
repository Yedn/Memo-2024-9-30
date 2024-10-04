using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TreeState { Close, Open };

public class Tree : MonoBehaviour
{
    public GameObject tree;
    public GameObject circle;
    public float atk = 0.1f;
    public TreeState treeState;
    public float detectionRadius = 1.5f;

    public float atkTime;
    public float atkDuration = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 0.0f;
        treeState = TreeState.Close;
        circle.GetComponent<TreeDetect>().SetRadius(detectionRadius);
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && treeState == TreeState.Open)
        {
            atkTime = atkDuration;
            AttackPlayer(GameObject.FindWithTag("Player"));
            //Debug.Log("Get Hit Form: Tree");
            //Vector2 collisionPosition = new Vector2(collision.GetComponent<Transform>().position.x, collision.GetComponent<Transform>().position.y);
            //Vector2 RepelledDirection = collisionPosition - new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
            ////collision.GetComponent<PlayerController>().PlayerRepelled(RepelledDirection.normalized);
            //collision.GetComponent<PlayerController>().GetHit(atk);
        }
    }
    public void AttackPlayer(GameObject player)
    {
        atkTime += Time.deltaTime;
        while (atkTime >= atkDuration)
        {
            Debug.Log("Get Hit Form: Tree");
            Vector2 collisionPosition = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y);
            Vector2 RepelledDirection = collisionPosition - new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
            //collision.GetComponent<PlayerController>().PlayerRepelled(RepelledDirection.normalized);
            player.GetComponent<PlayerController>().GetHit(atk);
            atkTime = 0;
        }
    }


}
