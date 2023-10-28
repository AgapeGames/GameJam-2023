using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public SpriteRenderer spriteRendereer;

    public Sprite spriteSide;
    public Sprite spriteUp;
    public Sprite spriteDown;
    void Start()
    {
        
    }

    public void SetSpriteSide()
    {
        spriteRendereer.sprite = spriteSide;
    }
    public void SetSpriteUp()
    {
        spriteRendereer.sprite = spriteUp;
    }
    public void SetSpriteDown()
    {
        spriteRendereer.sprite = spriteDown;
    }
}
