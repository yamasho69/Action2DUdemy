using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class RisuController : MonoBehaviour {
    [SerializeField, Tooltip("�ړ��X�s�[�h")]
    private int moveSpeed;
    [SerializeField]
    private Animator playerAnim;
    //[SerializeField]
    //private Animator weaponAnim;
    public Rigidbody2D rb;

    //[System.NonSerialized]//���̊֐�����͎Q�Ƃ��邪�C���X�y�N�^�ɂ͕\���������Ȃ��ꍇ
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
    public float currentStamina;//���݂̃X�^�~�i

    [SerializeField]
    private float dashSpeed, dashLength, dashCost;//�X�s�[�h�A�����A�R�X�g

    private float dashCounter, activeMoveSpeed;//���b����邩

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
        //�|�[�Y���͓��͖���
        if (Time.timeScale ==0) {
            return;
        }

        /*if (GameManager.instance.statusPanal.activeInHierarchy) {
            return;
        }

        //���G���Ԓ����ǂ���
        if (invincibilityTime > 0) {
            invincibilityCounter -= Time.deltaTime;
        }

        //�m�b�N�o�b�N�����ǂ���
        if (isknockingback) {
            knockbackCounter -= Time.deltaTime;
            rb.velocity = knockDir * knockbackForce;

            if (knockbackCounter <= 0) {
                isknockingback = false;
            } else {
                return;//�m�b�N�o�b�N���Ȃ�A����ȍ~�͏������Ȃ�
            }
        }*/


        //.normalized�Ńx�N�g���̑傫���𐳋K�����A�΂߈ړ��̃X�s�[�h���㉺���E�̈ړ��X�s�[�h�ɍ��킹��
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * activeMoveSpeed;

        if (rb.velocity != Vector2.zero) {//�ړ��X�s�[�h���[���ł͂Ȃ���
            playerAnim.SetBool("moving", true);
            if (Input.GetAxisRaw("Horizontal") != 0) {//���E�̓��͂��[���ł͂Ȃ���              
                if (Input.GetAxisRaw("Horizontal") > 0) {//�E�Ɉړ����Ă���Ƃ�
                    playerAnim.SetFloat("X", 1f);//�A�j���[�^�[�̃p�����[�^X���P��
                    playerAnim.SetFloat("Y", 0);

                    /*
                    weaponAnim.SetFloat("X", 1f);//�A�j���[�^�[�̃p�����[�^X���P��
                    weaponAnim.SetFloat("Y", 0);
                    */

                } else {
                    playerAnim.SetFloat("X", -1f);//�A�j���[�^�[�̃p�����[�^X���]�P��
                    playerAnim.SetFloat("Y", 0);

                    /*
                    weaponAnim.SetFloat("X", -1f);//�A�j���[�^�[�̃p�����[�^X���]�P��
                    weaponAnim.SetFloat("Y", 0);
                    */

                }
            } else if (Input.GetAxisRaw("Vertical") > 0) {//������ւ̓��͂�����Ƃ�
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);//�A�j���[�^�[�̃p�����[�^Y���P��

                /*
                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", 1);//�A�j���[�^�[�̃p�����[�^Y���P��
                */

            } else {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);//�A�j���[�^�[�̃p�����[�^Y��-�P��

                /*
                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", -1);//�A�j���[�^�[�̃p�����[�^Y��-�P��
                */
            }
        } else {
            playerAnim.SetBool("moving", false);
        }
        /*if (Input.GetMouseButtonDown(0)) {
            weaponAnim.SetTrigger("Attack");
        }*/

        if (dashCounter <= 0) {
            //���[�}�����̓��[�h���Ə�肭�_�b�V�����@�\���Ȃ����Ƃ�������
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
    /// ������΂��p�̊֐�
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
        //�_���[�W��H����āAHP��0�ȉ��ɂȂ�Ȃ��悤��Clamp�Œ���
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
