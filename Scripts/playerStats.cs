using UnityEngine;

public class playerStats : MonoBehaviour
{
    #region Variables
    public float health = 3f;
    public float speed;
    [Header("Jump")]
    public float jumpForce;
    public float doubleJumpForce;
    public int doulbleJumps = 0;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpDelay = 0.2f;
    [Header("Dash")]
    public float dashDelay = 2f;
    public float dashSpeed = 5f;
    public float dashTime = 2f;
    [Header("Bools")]
    public bool canMove = true;
    public bool canJump = true;
    public bool canDash = true;
    #endregion
}
