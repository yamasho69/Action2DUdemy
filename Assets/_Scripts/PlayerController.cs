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
    [SerializeField,Tooltip("�ړ��X�s�[�h")]
    private int moveSpeed;
    [SerializeField]
    private Animator playerAnim;
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        //.normalized�Ńx�N�g���̑傫���𐳋K�����A�΂߈ړ��̃X�s�[�h���㉺���E�̈ړ��X�s�[�h�ɍ��킹��
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) .normalized * moveSpeed;

        if(rb.velocity != Vector2.zero) {//�ړ��X�s�[�h���[���ł͂Ȃ���
            if (Input.GetAxisRaw("Horizontal") != 0) {//���E�̓��͂��[���ł͂Ȃ���
                if(Input.GetAxisRaw("Horizontal") > 0) {//�E�Ɉړ����Ă���Ƃ�
                    playerAnim.SetFloat("X", 1f);//�A�j���[�^�[�̃p�����[�^X���P��
                    playerAnim.SetFloat("Y", 0);
                } else {
                    playerAnim.SetFloat("X", -1f);//�A�j���[�^�[�̃p�����[�^X���]�P��
                    playerAnim.SetFloat("Y", 0);
                }
            }else if (Input.GetAxisRaw("Vertical")> 0){//������ւ̓��͂�����Ƃ�
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);//�A�j���[�^�[�̃p�����[�^Y���P��
            } else {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);//�A�j���[�^�[�̃p�����[�^Y��-�P��
            }
        }
    }
}
