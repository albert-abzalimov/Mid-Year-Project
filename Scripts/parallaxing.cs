using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour

{
    
    private float posA;
    public Transform backbackground, foreGround;
    // Start is called before the first frame update
    void Start()
    {
        posA = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveA = transform.position.x - posA;

        backbackground.position = backbackground.position + new Vector3(moveA,0f,0f);
        foreGround.position += new Vector3(moveA * .5f,0f,0f);
        posA = transform.position.x;

    }
}
