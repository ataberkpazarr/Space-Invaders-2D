using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Vector2 mouseDrag, mousePrePos;
    [SerializeField] private float speed;
    [SerializeField] private Projectile laserPrefab;
    [SerializeField] private GameObject sadEmoji;
    private bool isLaserActive=false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) // if space pressed,then shoot
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(0)) // movement is based on left clicking and sliding 
        {
            mousePrePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mouseDrag = (Vector2)Input.mousePosition - mousePrePos;
            mousePrePos = Input.mousePosition;

            var newPlayerPos = transform.localPosition;
            float newX = Mathf.Clamp(newPlayerPos.x + mouseDrag.x, -8.8f, 8.8f);
            transform.DOMoveX(newX, 0.2f);

        }
        else
        {
            mouseDrag = Vector2.zero;
        }


    }


    private void Shoot()
    {
        if (!isLaserActive) // if there exist a laser which already shooted and not collided to somewhere yet
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            isLaserActive = true;
        }
    }

    private void OnEnable()
    {
        Projectile.projectileDestroyed += MakeReadyForShoot;
    }

    private void OnDisable()
    {
        Projectile.projectileDestroyed -= MakeReadyForShoot;
    }

    private void MakeReadyForShoot()
    {
        isLaserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Missile")) // if other object of collision is at invader or missile layer then  game over
        {
            sadEmoji.SetActive(true);
            StartCoroutine(ReloadScene());
        }
    }


    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
