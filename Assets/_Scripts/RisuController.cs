using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class RisuController : MonoBehaviour {
    [SerializeField, Tooltip("移動スピード")]
    private int moveSpeed;
    [SerializeField]
    private Animator playerAnim;
    //[SerializeField]
    //private Animator weaponAnim;
    public Rigidbody2D rb;

    //[System.NonSerialized]//他の関数からは参照するがインスペクタには表示したくない場合
    //public int currentHealth;
    //public int maxHealth;

    //private bool isknockingback;
    //private Vector2 knockDir;

    //[SerializeField]
    //private float knockbackTime, knockbackForce;
    //private float knockbackCounter;

    //[SerializeField]
    //private float invincibilityTime;
    //private float invincibilityCounter;

    public float totalStamina, recoverySpeed;
    [System.NonSerialized]
    public float currentStamina;//現在のスタミナ

    [SerializeField]
    private float dashSpeed, dashLength, dashCost;//スピード、距離、コスト

    private float dashCounter, activeMoveSpeed;//何秒走れるか

    //private Flash flash;

    void Start() {
        //currentHealth = maxHealth;

        //GameManager.instance.UpdateHealthUI();

        activeMoveSpeed = moveSpeed;

        currentStamina = totalStamina;

        GameManager.instance.UpdateStaminaUI();

        //flash = GetComponent<Flash>();
    }

    void Update() {
        //ポーズ中は入力無効
        if (Time.timeScale ==0) {
            return;
        }

        /*if (GameManager.instance.statusPanal.activeInHierarchy) {
            return;
        }

        //無敵時間中かどうか
        if (invincibilityTime > 0) {
            invincibilityCounter -= Time.deltaTime;
        }

        //ノックバック中かどうか
        if (isknockingback) {
            knockbackCounter -= Time.deltaTime;
            rb.velocity = knockDir * knockbackForce;

            if (knockbackCounter <= 0) {
                isknockingback = false;
            } else {
                return;//ノックバック中なら、これ以降は処理しない
            }
        }*/


        //.normalizedでベクトルの大きさを正規化し、斜め移動のスピードを上下左右の移動スピードに合わせる
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * activeMoveSpeed;

        if (rb.velocity != Vector2.zero) {//移動スピードがゼロではない時
            playerAnim.SetBool("moving", true);
            if (Input.GetAxisRaw("Horizontal") != 0) {//左右の入力がゼロではない時              
                if (Input.GetAxisRaw("Horizontal") > 0) {//右に移動しているとき
                    playerAnim.SetFloat("X", 1f);//アニメーターのパラメータXを１に
                    playerAnim.SetFloat("Y", 0);

                    /*
                    weaponAnim.SetFloat("X", 1f);//アニメーターのパラメータXを１に
                    weaponAnim.SetFloat("Y", 0);
                    */

                } else {
                    playerAnim.SetFloat("X", -1f);//アニメーターのパラメータXを‐１に
                    playerAnim.SetFloat("Y", 0);

                    /*
                    weaponAnim.SetFloat("X", -1f);//アニメーターのパラメータXを‐１に
                    weaponAnim.SetFloat("Y", 0);
                    */

                }
            } else if (Input.GetAxisRaw("Vertical") > 0) {//上方向への入力があるとき
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);//アニメーターのパラメータYを１に

                /*
                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", 1);//アニメーターのパラメータYを１に
                */

            } else {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);//アニメーターのパラメータYを-１に

                /*
                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", -1);//アニメーターのパラメータYを-１に
                */
            }
        } else {
            playerAnim.SetBool("moving", false);
        }
        /*if (Input.GetMouseButtonDown(0)) {
            weaponAnim.SetTrigger("Attack");
        }*/

        if (dashCounter <= 0) {
            //ローマ字入力モードだと上手くダッシュが機能しないことがあった
            if (Input.GetKeyDown(KeyCode.Space) && currentStamina > dashCost) {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                currentStamina -= dashCost;
                GameManager.instance.UpdateStaminaUI();
            }
        } else {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0) {
                activeMoveSpeed = moveSpeed;
            }
        }
        currentStamina = Mathf.Clamp(currentStamina + recoverySpeed * Time.deltaTime, 0, totalStamina);
        GameManager.instance.UpdateStaminaUI();
    }
    /// <summary>
    /// 吹き飛ばし用の関数
    /// </summary>
    /// <param name="position"></param>

    /*
    public void KnockBack(Vector3 position) {
        knockbackCounter = knockbackTime;
        isknockingback = true;

        knockDir = transform.position - position;
        knockDir.Normalize();
    }*/

    public void DamagePlayer() {
        //ダメージを食らって、HPが0以下にならないようにClampで調整
        //if (invincibilityCounter <= 0) {
            //flash.PlayFeedback();

            //currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            //invincibilityCounter = invincibilityTime;

            SoundManager.instance.PlaySE(2);

            //if (currentHealth == 0) {
                gameObject.SetActive(false);
                SoundManager.instance.PlaySE(0);
                GameManager.instance.Load();
            //}
        //}
        //GameManager.instance.UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "donguri" && GameManager.instance.nowDonguri < 5) {
            //Items items = collision.GetComponent<Items>();
            SoundManager.instance.PlaySE(1);
            //currentHealth = Mathf.Clamp(currentHealth + items.helthItemRecoveryValue, 0, maxHealth);
            GameManager.instance.nowDonguri++;
            GameManager.instance.UpdateDonguriUI();
            Destroy(collision.gameObject);
        }

        if(collision.tag == "House") {
            GameManager.instance.totalDonguri += GameManager.instance.nowDonguri;
            GameManager.instance.nowDonguri = 0;
            GameManager.instance.UpdateDonguriUI();
        }
    }
}
