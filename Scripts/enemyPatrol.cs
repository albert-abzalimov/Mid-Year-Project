using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{

    public float walkSpeed, range, timeBTWShots, shootSpeed;
    private float distToPlayer;
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn, canShoot;
    
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos;
    public GameObject bullet, killBox;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        player = FindObjectOfType<playerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);
    
        if(distToPlayer <= range){
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0){
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if(canShoot)
            StartCoroutine(Shoot());
        }
        else{
            mustPatrol = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Lost life");
            //Player takes damage
            FindObjectOfType<healthScript>().dmg();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }


    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);
        if(walkSpeed < 0){
            newBullet.transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        canShoot = true;
    }
}