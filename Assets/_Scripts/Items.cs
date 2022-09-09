using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class Items : MonoBehaviour
{
    public int helthItemRecoveryValue;

    [SerializeField]
    private float lifeTime;

    public float waitTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if(waitTime > 0) {
            waitTime -= Time.deltaTime;
        }
    }
}
