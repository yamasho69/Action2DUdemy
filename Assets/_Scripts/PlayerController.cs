using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Tooltip("移動スピード")]
    private int moveSpeed;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private Animator weaponAnim;
    public Rigidbody2D rb;

    [System.NonSerialized]//他の関数からは参照するがインスペクタには表示したくない場合
    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;

        GameManager.instance.UpdateHealthUI();
    }

    void Update()
    {
        //.normalizedでベクトルの大きさを正規化し、斜め移動のスピードを上下左右の移動スピードに合わせる
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) .normalized * moveSpeed;

        if(rb.velocity != Vector2.zero) {//移動スピードがゼロではない時
            if (Input.GetAxisRaw("Horizontal") != 0) {//左右の入力がゼロではない時
                if(Input.GetAxisRaw("Horizontal") > 0) {//右に移動しているとき
                    playerAnim.SetFloat("X", 1f);//アニメーターのパラメータXを１に
                    playerAnim.SetFloat("Y", 0);

                    weaponAnim.SetFloat("X", 1f);//アニメーターのパラメータXを１に
                    weaponAnim.SetFloat("Y", 0);

                } else {
                    playerAnim.SetFloat("X", -1f);//アニメーターのパラメータXを‐１に
                    playerAnim.SetFloat("Y", 0);

                    weaponAnim.SetFloat("X", -1f);//アニメーターのパラメータXを‐１に
                    weaponAnim.SetFloat("Y", 0);
                }
            }else if (Input.GetAxisRaw("Vertical")> 0){//上方向への入力があるとき
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);//アニメーターのパラメータYを１に

                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", 1);//アニメーターのパラメータYを１に
            } else {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);//アニメーターのパラメータYを-１に

                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", -1);//アニメーターのパラメータYを-１に
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            weaponAnim.SetTrigger("Attack");
        }

    }
}
