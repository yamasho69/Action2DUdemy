using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;
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

    public GameObject [] donguriGra;

    private bool resultOn;


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

    [SerializeField,Tooltip("���x���A�b�v�ɕK�v�Ȍo���l")]
    private int[] requiredExp;

    [SerializeField]
    private GameObject levelUpText;

    [SerializeField]
    private Canvas canvas;*/

    public int totalDonguri = 0,nowDonguri= 0;
    public Text totalDonguriText;
    public GameObject [] nowDonguris;
    public float totalDays;
    private float limitDay;
    public Text DaysText;

    public GameObject stones1;
    public GameObject stones2;

    public GameObject forest1;
    public GameObject forest2;

    public GameObject donguriWave2;

    public GameObject resultPanel;
    public Text resultText;
    public Text resultDonguri;
    public GameObject [] resultSprites;
    public GameObject risu;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        limitDay = totalDays;
    }

    void Update() {
        if (limitDay <= 0 && !resultOn) {
            // 0�b�ɂȂ����Ƃ��̏���
            DaysText.text = "�~�����I�I";
            if (totalDonguri > 35) {
                resultSprites[0].SetActive(true);
                resultText.text = "���N�̓~���͍������I";
            }else if (totalDonguri>25) {
                resultSprites[1].SetActive(true);
                resultText.text = "�]�T�������ē~���z�������I";
            } else if (totalDonguri>15) {
                resultSprites[2].SetActive(true);
                resultText.text = "������ƂЂ������c�c�B";
            } else {
                resultSprites[3].SetActive(true);
                resultText.text = "����Ȕ��~�ő��v���H";
            }
            resultDonguri.text = "�W�߂��h���O���F" + totalDonguri+"��";
            resultPanel.SetActive(true);
            risu.SetActive(false);
            // Type == Number �̏ꍇ
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(totalDonguri);
            resultOn = true;
            return;
        }

        if (limitDay < 40) {
            stones1.SetActive(false);
            stones2.SetActive(true);
            forest1.SetActive(false);
            forest2.SetActive(true);
            donguriWave2.SetActive(true);
        }
        limitDay -= Time.deltaTime;
        int i = (int)limitDay;
        DaysText.text = "�~�܂ł���"+ i.ToString("d2") +"��";
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
        totalDonguriText.text = "�W�߂��h���O���F" + totalDonguri;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void LoadTitle() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    /*public void ShowStatusPanel() {
        statusPanal.SetActive(true);
        Time.timeScale = 0;
        StatusUpdate();
    }

    public void StatusUpdate() {
        hptext.text = "�̗́F" + player.maxHealth;
        stText.text = "�X�^�~�i:" + player.totalStamina;
        atText.text = "�U����:" + weapon.attackDamage;
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
