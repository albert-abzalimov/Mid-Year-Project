using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class powerUpSlot : MonoBehaviour
{
    #region Variables
    #endregion
    public powerUpObject curPowerup;
    float duration;
    playerStats stats;
    Image slotImage; 
    TextMeshProUGUI countDownTimer;
    bool usingPowerup = false;
    bool addedDoublejump = false;
    private Coroutine coroutine;
    
    // get the stats
    void Start(){
        stats = FindObjectOfType<playerStats>();
        slotImage = GetComponent<Image>();
        slotImage.enabled = false;
        countDownTimer = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update(){
        if (!usingPowerup){
            slotImage.enabled = false;
            countDownTimer.gameObject.SetActive(false);
        }
        if (addedDoublejump){
            if (stats.doulbleJumps == 0){
                StopCoroutine(coroutine);
                stopPowerUp();
            }
        }
    }

    // change the current powerUp that we are holding
    public bool UpdateObject(powerUpObject newPow){
        if (usingPowerup){
            return false;
        }
        slotImage.enabled = true;
        curPowerup = newPow;
        duration = curPowerup.duration;
        slotImage.sprite = curPowerup.gameObject.GetComponent<SpriteRenderer>().sprite;
        UsePowerUp();
        return true;
    }
    public void UsePowerUp(){
        if (curPowerup == null){
            Debug.Log("PowerUp is null");
            usingPowerup = false;
            return;
        }
        usingPowerup = true;
        if (curPowerup.powerUp == powerUpObject.PowerUpType.DoubleJump){
            coroutine = StartCoroutine(addDoubleJump());
        }
        else if (curPowerup.powerUp == powerUpObject.PowerUpType.HealthBoost){
            healthScript.instance.addHealth();
            stopPowerUp();
            return;
        }
        else if (curPowerup.powerUp == powerUpObject.PowerUpType.SpeedBoost){
            StartCoroutine(boostSpeed());
        }
        
    }

    void stopPowerUp(){
        curPowerup = null;
        usingPowerup = false;
        countDownTimer.gameObject.SetActive(false);
        slotImage.enabled = false;
        addedDoublejump = false;
    }


    IEnumerator addDoubleJump(){
        countDownTimer.gameObject.SetActive(true);
        stats.doulbleJumps++;
        addedDoublejump = true;
        while (duration > 0){
            countDownTimer.text = duration.ToString();
            yield return new WaitForSeconds(1f);
            duration--;
        }
        stats.doulbleJumps = 0;
        stopPowerUp();

    }
    IEnumerator boostSpeed(){
        countDownTimer.gameObject.SetActive(true);
        stats.speed *= 1.75f;
        while (duration > 0){
            countDownTimer.text = duration.ToString();
            yield return new WaitForSeconds(1f);
            duration--;
        }
        stats.speed /= 1.75f;
        stopPowerUp();
        // add effect?
    }
}
