using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Profile")]
    public PlayerAnim playerAnim;

    public int health;

    public float moveSpeed;

    [Header("Component")]
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteCharacter;

    private Vector2 movement;


    public Item currentItem;

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

        if(movement.y > 0)
        {
            playerAnim.SetSpriteUp();
        }else if (movement.y < 0)
        {
            playerAnim.SetSpriteDown();
        }
        else
        {
            playerAnim.SetSpriteSide();
        }

        //Get Item
        if(currentItem != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                currentItem.GetItem();
                currentItem = null;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

}
