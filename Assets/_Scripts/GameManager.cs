using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class GameManager : MonoBehaviour
{
    //static
    public static GameManager instance;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private PlayerController player;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateHealthUI() {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;
    }
}
