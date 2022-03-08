using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompyWompy : MonoBehaviour
{
    public GameObject deathAni;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            //deactivates the parent of the killbox, so the enemy's invisible ghost does not attack the player
            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathAni, other.transform.position, other.transform.rotation);
            Audio.instance.playSounds(0);

            playerMovement.instance.Bounce();
        }
    }
}
