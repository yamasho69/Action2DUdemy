using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    //�V���O���g����
    public static SoundManager instance;

    public AudioSource[] se;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }else if(instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// SE��炷(0:�Q�[���I�[�o�[�A1:�񕜁A2:��e�A3:�U���A4:UI�A5:�R�C��)
    /// </summary>
    /// <param name="x"></param>

    public void PlaySE(int x) {
        se[x].Stop();
        se[x].Play();
    }
}
