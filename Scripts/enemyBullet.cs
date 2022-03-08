using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    
    public float dieTime, damage;
    
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Removes health
            //transform.position = respawnPoint;
            healthScript.instance.dmg();
            playerMovement.instance.Bounce();
            Die();
        }
        else{
            Die();
        }
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);

        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
