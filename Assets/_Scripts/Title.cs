using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class Title : MonoBehaviour
{
    public void GameStart() {
        SceneManager.LoadScene("Main");
        SoundManager.instance.PlaySE(4);
    }
}
