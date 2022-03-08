using UnityEngine;
using System.Collections;

public class powerUpObject : MonoBehaviour
{
    #region Variables
    public float duration;
    powerUpSlot slot;
    public enum PowerUpType {DoubleJump, HealthBoost, SpeedBoost};
    public PowerUpType powerUp;
    bool inPlayer = false;
    
    public float amplitude = 0.3f;
    public float frequency = 1f;
 
    Vector2 posOffset = new Vector2 ();
    Vector2 tempPos = new Vector2 ();
    SpriteRenderer rend;
    Collider2D coll;
    playerMovement player;
    #endregion

    void Start(){
        slot = FindObjectOfType<powerUpSlot>();
        posOffset = transform.position;
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        player = FindObjectOfType<playerMovement>();

    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            inPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            inPlayer = false;
        }
    }
    void Update(){
        if (inPlayer && Input.GetKeyDown(KeyCode.F)){
            if (slot.UpdateObject(this)){
                // play animation???
                rend.enabled = false;
                coll.enabled = false;
                
            }
        } 

         // Float up/down with a Sin()
        tempPos = posOffset;

        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
        
    }
}
