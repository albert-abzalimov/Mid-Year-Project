using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class healthScript : MonoBehaviour
{
    public static healthScript instance;
    public int Nhealth, Mhealth;
    public float lenMark;
    private float countMark;
    private SpriteRenderer Spr;
    private Vector3 respawnPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Nhealth = Mhealth;
        respawnPoint = transform.position;
        Spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(countMark > 0)
        {
            countMark -= Time.deltaTime;

            if(countMark <= 0)
            {
                Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, 1f);

            }
        }
        
    }

    public void dmg()
    {
        if(countMark <= 0)
        {
            Nhealth--;
            if(Nhealth <= 0)
            {
                transform.position = respawnPoint;
                Nhealth += 3;
                //  gameObject.SetActive(false);
            } else
            {
                countMark = lenMark;
                Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, .5f);
                playerMovement.instance.KB();
            }
            Audio.instance.playSounds(0);

            UI.instance.UpdaterH();
        }

    }
    public void addHealth(){
        if (Nhealth < Mhealth){
            Nhealth++;
            UI.instance.UpdaterH();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if player collided with the fall detector
        if(collision.tag == "FallDetector")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}
