    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Invader")) // if the other collided object is at invader layer, then it is an invader and our bunker must be destroyed
        {
            gameObject.SetActive(false);
        }
    }
}
