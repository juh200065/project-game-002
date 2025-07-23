using System;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;

    public BoxCollider2D PlayerCollider;
    
    private float SuperJumpMultiplier = 1.3f;
    public bool isSuperJump = false;
    private bool canSuperJump = false;
    private bool isGrounded = true;

    private bool isInvincible = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isSuperJump)
        {
            canSuperJump = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            float jumpPower = JumpForce;

            if (canSuperJump)
            {
                jumpPower *= SuperJumpMultiplier;
                canSuperJump = false;
                isSuperJump = false;
            }

            PlayerRigidBody.AddForceY(jumpPower, ForceMode2D.Impulse);


            // PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }

    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives -= 1;
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(2, GameManager.Instance.Lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 3f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            if (!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();
            }


        }
        else if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);
            GameManager.Instance.FoodScore += 10;
            //Heal();
        }
        else if (collider.gameObject.tag == "beer")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.tag == "superfood")
        {
            Destroy(collider.gameObject);
            isSuperJump = true;
        }
    }
}
