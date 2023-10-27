using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Profile")]
    public int health;

    public float moveSpeed;

    [Header("Component")]
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteCharacter;

    private Vector2 movement;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0)
        {
            spriteCharacter.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteCharacter.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

}
