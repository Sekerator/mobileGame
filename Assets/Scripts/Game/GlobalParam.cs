﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GlobalParam : MonoBehaviour
{
    public Text materials, money, marketing, research, time;
    private string[] season = {"Зима","Весна","Лето","Осень"};
    private string year = "1900";
    private int thisSeason = 0;
    private float timeRemaining = 5f;
    private string url = "http://mopsnet.tk/game.php";
    private bool allReady = false;
    public GameObject readyPanel, buttons;
    
    private void Awake()
    {
        StartCoroutine(createReady());
        materials.text = "0";
        money.text = "100000";
        marketing.text = "1";
        research.text = "0";
        time.text = season[thisSeason] + " " + year;
    }

    private void nextSeason()
    {
        if (thisSeason == 3)
        {
            thisSeason = 0;
            year = (Convert.ToInt32(year) + 1).ToString();
        }
        else
            thisSeason++;
    }
    
    private void nextSeason_butt()
    {
        gameObject.GetComponent<Bank>().nextSeason();
        gameObject.GetComponent<Marketing>().nextSeason();
        gameObject.GetComponent<Components>().nextSeason();
        gameObject.GetComponent<Assembly>().nextSeason();
        buttons.SetActive(true);
        
        time.text = season[thisSeason] + " " + year;
    }

    IEnumerator createReady()
    {
        WWWForm add = new WWWForm();
        add.AddField("request", "createReady");
        add.AddField("nickname", PlayerPrefs.GetString("nickname"));
        add.AddField("room_number", PlayerPrefs.GetString("room_number"));
        WWW a = new WWW(url, add);
        yield return a;
        
        if(a.text == "Error")
            Debug.Log(a.text);
    }

    IEnumerator readyNextSeason()
    {
        nextSeason();
        WWWForm add = new WWWForm();
        add.AddField("request", "nextSeason");
        add.AddField("nickname", PlayerPrefs.GetString("nickname"));
        add.AddField("room_number", PlayerPrefs.GetString("room_number"));
        add.AddField("this_season", season[thisSeason]);
        WWW a = new WWW(url, add);
        yield return a;

        if (a.text == "Good")
        {
            readyPanel.SetActive(false);
            nextSeason_butt();
        }
        else
        {
            Debug.Log(a.text);
        }
    }

    private void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            if (readyPanel.activeSelf == true)
            {
                StartCoroutine(readyNextSeason());
            }
            timeRemaining = 5f;
        }
    }
}