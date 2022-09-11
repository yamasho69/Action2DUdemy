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

    /*[SerializeField]
    private Slider hpSlider;*/

    [SerializeField]
    private RisuController player;

    [SerializeField]
    private Slider staminaSlider;




    /*public GameObject dialogBox;
    public Text dialogText;

    private string[] dialogLines;
    private int currretLine;
    private bool justStarted;

    public GameObject statusPanal;

    [SerializeField]
    private Text hptext, stText, atText;
    [SerializeField]
    private Weapon weapon;

    private int totalEXP, currentLV;

    [SerializeField,Tooltip("レベルアップに必要な経験値")]
    private int[] requiredExp;

    [SerializeField]
    private GameObject levelUpText;

    [SerializeField]
    private Canvas canvas;*/

    public int totalDonguri = 0,nowDonguri= 0;
    public Text totalDonguriText;
    public GameObject [] nowDonguris;
    private float totalDays = 90;
    private float limitDay;
    public Text DaysText;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        limitDay = totalDays;
    }

    void Update() {
        if (limitDay <= 0) {
            // 0秒になったときの処理
            DaysText.text = "冬到来！！";
            return;
        }

        limitDay -= Time.deltaTime;
        int i = (int)limitDay;
        DaysText.text = "冬まであと"+ i.ToString("d2") +"日";
    }
    /*private void Start() {
        if (PlayerPrefs.HasKey("MaxHP")) {
            LoadStatuse();
        }
    }
    void Update()
    {
        if (dialogBox.activeInHierarchy) {
            if (Input.GetMouseButtonUp(1)) {
                SoundManager.instance.PlaySE(4);
                if (!justStarted) {
                    currretLine++;
                    if(currretLine >= dialogLines.Length) {
                        dialogBox.SetActive(false);
                    } else {
                        dialogText.text = dialogLines[currretLine];
                    }
                } else {
                    justStarted = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            ShowStatusPanel();
        }
    }*/

    /*public void UpdateHealthUI() {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;
    }*/

    public void UpdateStaminaUI() {
        staminaSlider.maxValue = player.totalStamina;
        staminaSlider.value = player.currentStamina;
    }

    public void UpdateDonguriUI() {
        totalDonguriText.text = "集めたドングリ：" + totalDonguri;
        if(nowDonguri == 0) {
            for(int i = 0; i < nowDonguris.Length; i++) {
                nowDonguris[i].SetActive(false);
            }
        } else {
            nowDonguris[nowDonguri-1].SetActive(true);
        }
    }

    /*public void ShowDialog(string[] lines) {
        dialogLines = lines;

        currretLine = 0;

        dialogText.text = dialogLines[currretLine];
        dialogBox.SetActive(true);
        justStarted = true;
    }

    public void ShowDialogChange(bool x) {
        dialogBox.SetActive(x);
    }*/

    public void Load() {
        SceneManager.LoadScene("Main");
    }

    public void LoadTitle() {
        SceneManager.LoadScene("Title");
    }

    /*public void ShowStatusPanel() {
        statusPanal.SetActive(true);
        Time.timeScale = 0;
        StatusUpdate();
    }

    public void StatusUpdate() {
        hptext.text = "体力：" + player.maxHealth;
        stText.text = "スタミナ:" + player.totalStamina;
        atText.text = "攻撃力:" + weapon.attackDamage;
    }

    public void CloseStatusPanel() {
        statusPanal.SetActive(false);
        Time.timeScale = 1;
    }

    public void AddExp(int exp) {
        if(requiredExp.Length <= currentLV) {
            return;
        }

        totalEXP += exp;

        if (totalEXP >= requiredExp[currentLV]) {
            currentLV++;

            player.maxHealth += 5;
            player.totalStamina += 5;
            weapon.attackDamage += 2;

            GameObject levelUp = Instantiate(levelUpText,player.transform.position,Quaternion.identity);
            levelUp.transform.SetParent(player.transform);
            //levelUp.transform.localPosition = player.transform.position + new Vector3(0, 10, 0);
        }
    }

    public void SaveStatuse() {
        PlayerPrefs.SetInt("MaxHP", player.maxHealth);
        PlayerPrefs.SetFloat("MaxSt", player.totalStamina);
        PlayerPrefs.SetInt("At", weapon.attackDamage);
        PlayerPrefs.SetInt("Level", currentLV);
        PlayerPrefs.SetInt("Exp", totalEXP);
    }

    public void LoadStatuse() {
        player.maxHealth = PlayerPrefs.GetInt("MaxHP");
        player.totalStamina = PlayerPrefs.GetFloat("MaxSt");
        weapon.attackDamage = PlayerPrefs.GetInt("At");
        currentLV = PlayerPrefs.GetInt("Level");
        totalEXP = PlayerPrefs.GetInt("Exp");
    }*/
}
