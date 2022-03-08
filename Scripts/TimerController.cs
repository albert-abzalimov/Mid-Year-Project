using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public TextMeshProUGUI timeCounter;

    public float delayBeforeStart = 0.2f;

    private TimeSpan timePlaying;
    private bool timerGoing;
    [HideInInspector]
    public float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "00:00.00";
        timerGoing = false;
        StartCoroutine(waitTime());
    }

    private IEnumerator waitTime(){
        yield return new WaitForSeconds(delayBeforeStart);
        BeginTimer();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            EndTimer();
        }
    }
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
