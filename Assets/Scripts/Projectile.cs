using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    public static Action projectileDestroyed; 

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) // when projectile collides something, destroy projectile
    {
        Destroy(gameObject);
        projectileDestroyed?.Invoke();
    }
}
