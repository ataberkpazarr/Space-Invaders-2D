using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Invader : MonoBehaviour
{
    public static Action sideBorderTriggered;
    public static Action invaderKilled;

    [SerializeField] private Sprite[] animationSprites;

    
    [SerializeField] private float animationTime;

    private SpriteRenderer spriteRenderer_;
    private int animationFrame_;


    private void Awake()
    {
        spriteRenderer_ = GetComponent<SpriteRenderer>();


    }


    private void Start()
    {
        InvokeRepeating(nameof(AnimateTheSprite), animationTime,animationTime);
    }

    private void AnimateTheSprite()
    {
        animationFrame_++; // go to next frame

        if (animationFrame_ >= animationSprites.Length)
        {
            animationFrame_ = 0;
        }


        spriteRenderer_.sprite = animationSprites[animationFrame_];

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RightBorder") && this.gameObject.activeInHierarchy) //when invaders reach to right border of screen then below action fired for making them to change their directions
        {
        
            sideBorderTriggered?.Invoke();
        }

        else if (collision.CompareTag("LeftBorder") && this.gameObject.activeInHierarchy)//when invaders reach to left border of screen then below action fired for making them to change their directions
        {
            Debug.Log("ee");

            sideBorderTriggered?.Invoke();
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) //if laser hits an invader then it should be destroyed and invaders class should be informed by the below action invoke
        {
            gameObject.SetActive(false);
            invaderKilled.Invoke();
        }

    }

}
