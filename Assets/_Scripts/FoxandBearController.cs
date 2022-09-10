using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class FoxandBearController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator enemyAnim;

    //同じ型の変数ならカンマで区切って連続で宣言できる
    [SerializeField]
    private float moveSpeed, waitTime, walkTime;

    private float waitCounter, moveCounter;

    private Vector2 moveDir;

    [SerializeField]
    private BoxCollider2D area;

    [SerializeField, Tooltip("プレイヤーを追いかける")]
    private bool chase;

    [HideInInspector]
    public bool isChaseing;

    [SerializeField]
    private float chaseSpeed, rangeToChase;
    private Transform target;

    [SerializeField]
    private float waitAfterHitting;

    [SerializeField]
    private GameObject right, left, front, back;

    /*[SerializeField]
    private int attaackDamage;

    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    private bool isKnockingBack;

    [SerializeField]
    private float knockBackTime, knockBackForce;

    private float knockBackCounter;

    private Vector2 knockDir;

    [SerializeField]
    private GameObject portion;

    [SerializeField]
    private float healthDropChance;

    [SerializeField]
    private GameObject blood;

    [SerializeField]
    private int exp;

    [SerializeField]
    private Image hpImage;

    private Flash flash;*/

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        waitCounter = waitTime;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        /*currentHealth = maxHealth;
        UpdateHealthImage();
        flash = GetComponent<Flash>();*/
    }

    void Update() {
        /*if (isKnockingBack) {
            if (knockBackCounter > 0) {
                knockBackCounter -= Time.deltaTime;
                rb.velocity = knockDir * knockBackForce;
            } else {
                rb.velocity = Vector2.zero;
                isKnockingBack = false;
            }
            return;
        }*/

        if (!isChaseing) {//追いかけてないときは彷徨う
            if (waitCounter > 0) {
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;
                if (waitCounter <= 0) {
                    moveCounter = walkTime;
                    //enemyAnim.SetBool("moving", true);
                    moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    moveDir.Normalize();
                    MoveAnimation();    
                }
            } else {
                moveCounter -= Time.deltaTime;
                rb.velocity = moveDir * moveSpeed;
                MoveAnimation();
                if (moveCounter <= 0) {
                    //enemyAnim.SetBool("moving", false);
                    waitCounter = waitTime;
                }
            }

            /*if (chase) {
                if (Vector3.Distance(transform.position, target.transform.position) < rangeToChase) {//敵とプレイヤーの距離がrangeToChaseより小さい時
                    isChaseing = true;
                }
            }*/

        } else {

            /*if (waitCounter > 0) {
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;

                if (waitCounter <= 0) {
                    //enemyAnim.SetBool("moving", true);
                }
            } else {*/
                moveDir = target.transform.position - transform.position;
                moveDir.Normalize();
                rb.velocity = moveDir * chaseSpeed;
                MoveAnimation();
            //}
            //waitCounter = waitTime;
            /*if (Vector3.Distance(transform.position, target.transform.position) > rangeToChase) {//敵とプレイヤーの距離がrangeToChaseより大きい時
                isChaseing = false;
                //enemyAnim.SetBool("moving", false);
            }*/
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, area.bounds.min.x + 1, area.bounds.max.x - 1)
            , Mathf.Clamp(transform.position.y, area.bounds.min.y + 1, area.bounds.max.y - 1), transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (isChaseing) {
                RisuController player = collision.gameObject.GetComponent<RisuController>();
                //player.KnockBack(transform.position);
                player.DamagePlayer();

                waitCounter = waitAfterHitting;

                //enemyAnim.SetBool("moving", false);
            }
        }
    }

    private void MoveAnimation() {
        if (Mathf.Abs(moveDir.x) >= Mathf.Abs(moveDir.y)) {//Mathf.Abs(x)でxの絶対値を算出。Vector2のxとyの絶対値が大きいほう、
                                                           //つまりxが大きいなら横方向のアニメを再生する。
            if (moveDir.x >= 0) {
                enemyAnim.SetFloat("X", 1f);//アニメーターのパラメータXを１に
                enemyAnim.SetFloat("Y", 0);
                left.SetActive(false);
                right.SetActive(true);
                front.SetActive(false);
                back.SetActive(false);
            } else {
                enemyAnim.SetFloat("X", -1f);//アニメーターのパラメータXを‐１に
                enemyAnim.SetFloat("Y", 0);
                left.SetActive(true);
                right.SetActive(false);
                front.SetActive(false);
                back.SetActive(false);
            }
        } else {
            if (moveDir.y >= 0) {
                enemyAnim.SetFloat("X", 0);
                enemyAnim.SetFloat("Y", 1);//アニメーターのパラメータYを１に
                left.SetActive(false);
                right.SetActive(false);
                front.SetActive(false);
                back.SetActive(true);
            } else {
                enemyAnim.SetFloat("X", 0);
                enemyAnim.SetFloat("Y", -1);//アニメーターのパラメータYを-１に
                left.SetActive(false);
                right.SetActive(false);
                front.SetActive(true);
                back.SetActive(false);
            }
        }
    }

    /*public void KnockBack(Vector3 position) {
        isKnockingBack = true;
        knockBackCounter = knockBackTime;
        knockDir = transform.position - position;
        knockDir.Normalize();

        enemyAnim.SetBool("moving", false);
    }

    public void TakeDamage(int damage, Vector3 position) {
        currentHealth -= damage;
        UpdateHealthImage();
        flash.PlayFeedback();
        if (currentHealth <= 0) {

            Instantiate(blood, transform.position, transform.rotation);

            GameManager.instance.AddExp(exp);

            if (currentHealth <= 0) {
                if (Random.Range(0, 100) < healthDropChance && portion != null) {
                    Instantiate(portion, transform.position, transform.rotation);
                }
            }

            Destroy(gameObject);
        }
        KnockBack(position);
    }

    private void UpdateHealthImage() {
        hpImage.fillAmount = currentHealth / maxHealth;
    }*/
}
