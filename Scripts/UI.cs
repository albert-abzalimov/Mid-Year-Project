using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;

    public Image heart1, heart2, heart3;

    public Sprite full, empty;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdaterH()
    {
        switch(healthScript.instance.Nhealth)
        {
            case 3:
                heart1.sprite = full;
                heart2.sprite = full;
                heart3.sprite = full;

                break;

            case 2:
                heart1.sprite = full;
                heart2.sprite = full;
                heart3.sprite = empty;
                
                break;

            case 1:
                heart1.sprite = full;
                heart2.sprite = empty;
                heart3.sprite = empty;

                break;

            case 0:
                heart1.sprite = empty;
                heart2.sprite = empty;
                heart3.sprite = empty;

                break;
        }
    }
    
}
