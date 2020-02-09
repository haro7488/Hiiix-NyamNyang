﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public event Action<int> EventIncomeAmChanged = n => { };
    public event Action<int> EventIncomePmChanged = n => { };

    public static GoldManager instance;

    [Header("버프 지속 시간")]
    public float BuffDuration;
    private float _buffDuration;
    private float buffDuration {
        get { return _buffDuration; }
        set {
            _buffDuration = value;
            PlayerPrefs.SetFloat("BuffDuration", value);
        }
    }

    private bool isBuff;
    public bool IsBuff {
        get { return isBuff; }
        set {
            isBuff = value;
            if (isBuff) {
                UIManager.instance.BuffOn();
                NyangManager.Instance.BuffNyang(true);
                MasterNyang.instance.Buff(true);
            }
            else {
                UIManager.instance.BuffOff();
                NyangManager.Instance.BuffNyang(false);
                MasterNyang.instance.Buff(false);
            }
        }
    }

    private int _incomeAmAm;
    public int IncomeAm {
        get { return _incomeAmAm; }
        set {
            _incomeAmAm = value;
            Debug.Log("incomeAM " + value);
            EventIncomeAmChanged(_incomeAmAm);
        }
    }
    private int _incomePm;
    public int IncomePm {
        get { return _incomePm; }
        set {
            _incomePm = value;
            Debug.Log("incomePM " + value);
            EventIncomePmChanged(_incomePm);
        }
    }

    private int _currentGold;
    public int CurrentGold {
        get { return _currentGold; }
        set {
            _currentGold = value;
            Debug.Log("CurrentGold " + value);
            FindObjectOfType<UIManager>().Money.transform.GetChild(1).GetComponent<Text>().text = value.ToString();
            PlayerPrefs.SetInt("PlayerMoney", value);
            if (value >= 10000) achievementManager.Achievement_MoneySwag(10000);
            if (value >= 100000) achievementManager.Achievement_MoneySwag(100000);
            if (value >= 500000) achievementManager.Achievement_MoneySwag(500000);
        }
    }

    // 각종 버프/디버프로 인한 최종 가격 계수.
    public float watasinopointowa { get; set; }
    [Header("냥이들에게 얼마나 높은 가격으로 후려칠 것인가?")]
    // 냥이들에게 얼마나 높은 가격으로 후려칠 것인가?
    public float conscienceOfSeller;
    [Header("버프중일 때 보너스")]
    public float buffBonus;
    // 썬탠 미니게임으로 인한 보너스.
    private float tanningBonus;
    public float TanningBonus {
        get { return tanningBonus; }
        set {
            tanningBonus = value;
            PlayerPrefs.SetFloat("TanningBonus", value);
        }
    }

    private AchievementManager achievementManager;

    void Awake() {

        if (!instance) instance = this;

        achievementManager = FindObjectOfType<AchievementManager>();

        CurrentGold = PlayerPrefs.GetInt("PlayerMoney");
        IncomeAm = PlayerPrefs.GetInt("IncomeAM");
        IncomePm = PlayerPrefs.GetInt("IncomeMinus");
        TanningBonus = PlayerPrefs.GetFloat("TanningBonus");
    }

    void Start() {
        BuffDuration = 60;
        buffDuration = PlayerPrefs.GetFloat("BuffDuration");
        if (buffDuration > 0) IsBuff = true;
    }

    void Update() {
        BuffDurationDown();
    }

    public float getFactor() {
        // 선탠 게임 보너스.
        //float TanningBonus = (TimeManager.Instance.timeType > TimeType.PMOpenTime) ? this.TanningBonus : 1;
        // 양심 보너스.
        float conscienceBonus = (CookManager.instance.cookFood.isRecipe) ? conscienceOfSeller : 0;
        // 버프보너스.
        float BuffBonus = (GoldManager.instance.IsBuff) ? buffBonus : 1;
        watasinopointowa = conscienceBonus * BuffBonus * TanningBonus;
        return watasinopointowa;
    }



    private void BuffDurationDown() {
        if (!IsBuff) return;
        if(buffDuration < BuffDuration) {
            buffDuration += Time.deltaTime;
            return;
        }

        IsBuff = false;

        buffDuration = 0;
    }
    

}