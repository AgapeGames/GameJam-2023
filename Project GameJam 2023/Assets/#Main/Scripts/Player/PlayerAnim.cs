using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    public void AnimUp(bool con)
    {
        anim.SetBool("IsUp", con);
    }
    public void AnimDown(bool con)
    {
        anim.SetBool("IsDown", con);
    }
    public void AnimRight(bool con)
    {
        anim.SetBool("IsRight", con);
    }
    public void AnimLeft(bool con)
    {
        anim.SetBool("IsLeft", con);
    }
}
