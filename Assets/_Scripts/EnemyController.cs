using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator enemyAnim;

    //“¯‚¶•û
    [SerializeField]
    private float moveSpeed, waitTime, walkTime;

    private float waitCounter, moveCounter;

    private Vector2 moveDir;

    [SerializeField]
    private BoxCollider2D area;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        waitCounter = waitTime;
    }

    void Update()
    {
        if (waitCounter > 0) {
            waitCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if(waitCounter <= 0) {
                moveCounter = walkTime;
                enemyAnim.SetBool("moving", true);
                moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                moveDir.Normalize();
            }
        } else {
            moveCounter -= Time.deltaTime;
            rb.velocity = moveDir * moveSpeed;
            if(moveCounter <= 0) {
                enemyAnim.SetBool("moving", false);
                waitCounter = waitTime;
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, area.bounds.min.x + 1, area.bounds.max.x - 1)
            ,Mathf.Clamp(transform.position.y, area.bounds.min.y + 1, area.bounds.max.y - 1),transform.position.z);
    }
}
