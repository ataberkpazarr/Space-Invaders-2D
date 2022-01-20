using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Invaders : MonoBehaviour
{
    [SerializeField] GameObject happyEmoji;
    [SerializeField] GameObject sadEmoji;

    [SerializeField] Invader[] prefabs; 
    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private AnimationCurve speed;
    [SerializeField] private Projectile missilePrefab;

    private float missileAttackRate=1;


    private int totalKilledInvaders = 0;
     private int totalInvaders => rows * columns;
    private int totalAliveInvaders => totalInvaders - totalKilledInvaders;
    public float percentageOfKilled => (float)totalKilledInvaders / (float)totalInvaders;
    

    private Vector3 _direction = Vector2.right;

    private void Awake()
    {
        for (int row = 0; row < rows; row++) //create the invaders 
        {
            float width = 2 * (columns - 1);
            float height = 2 * (rows - 1);
            Vector2 centering = new Vector2(-width/2,-height/2);
            Vector3 rowPosition = new Vector3(centering.x,centering.y+(row *2),0);
            for (int c = 0; c < columns; c++)
            {
                Invader inv=Instantiate(prefabs[row],transform);
                Vector3 pos = rowPosition;
                pos.x = pos.x + c * 2f;
                inv.transform.localPosition = pos;
            }
        }
    }
    void Start()
    {
        InvokeRepeating(nameof(MissileAttack),missileAttackRate,missileAttackRate); // do an missile attack to player continously
    }

    void Update()
    {
        transform.position += _direction * speed.Evaluate(percentageOfKilled) * Time.deltaTime; //when the total alive amount of invaders are being decreased, then their speed are being increased
 
    }

    private void OnEnable()
    {
        Invader.sideBorderTriggered += ChangeDirection;
        Invader.invaderKilled += HandleKilledInvader;
    }



    
    private void OnDisable()
    {
        Invader.sideBorderTriggered -= ChangeDirection;
        Invader.invaderKilled -= HandleKilledInvader;
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            
            if (Random.value <(1f/(float)totalAliveInvaders ))
            {
                Instantiate(missilePrefab,invader.position,Quaternion.identity);
                break; // for making only one missile active at a time
            }
        }
    }
    private void ChangeDirection()
    {
        _direction.x *= -1f;
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y - 0.35f;
        transform.position = currentPos;
    }

    private void HandleKilledInvader()
    {

        totalKilledInvaders++;
        if (totalKilledInvaders>=totalInvaders)//win condition
        {
            happyEmoji.SetActive(true);
            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
