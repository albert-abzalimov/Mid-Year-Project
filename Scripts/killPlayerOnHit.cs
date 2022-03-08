using UnityEngine;

public class killPlayerOnHit : MonoBehaviour
{
    #region Variables
    #endregion

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            healthScript.instance.dmg();
        }
    }
}
