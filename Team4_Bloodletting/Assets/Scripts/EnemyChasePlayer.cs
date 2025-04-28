// using UnityEngine;

// public class EnemyChasePlayer : MonoBehaviour
// {
//     public float speed = 3f;
//     private Transform player;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//     }

//     void Update()
//     {
//         if (player == null) return;

//         Vector2 direction = (player.position - transform.position).normalized;
//         transform.Translate(direction * speed * Time.deltaTime);
        
//         if (direction.x > 0)
//         {
//             gameObject.GetComponent<SpriteRenderer>().flipX = true;
//         } else if (direction.x < 0)
//         {
//             gameObject.GetComponent<SpriteRenderer>().flipX = false;
//         }
//     }
// }

using UnityEngine;
using UnityEngine.Windows;

public class EnemyChasePlayer : MonoBehaviour
{
    public float baseSpeed = 3f;
    private float speedMultiplier = 1f;
    private Transform player;
    public int spr_dir = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float finalSpeed = baseSpeed * speedMultiplier;

        transform.Translate(direction * finalSpeed * Time.deltaTime);

        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Rat Animator; rats are order 1
        if (GetComponent<SpriteRenderer>().sortingOrder == 1)
        {
            if (direction.x >= Mathf.Abs(direction.y))
            {
                if (spr_dir != 2)
                {
                    Trigger();
                }
                GetComponent<Animator>().SetInteger("spr_dir", 2);
            }
            else if (direction.y > Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    if (spr_dir != 1)
                    {
                        Trigger();
                    }
                    GetComponent<Animator>().SetInteger("spr_dir", 1);
                }
                else
                {
                    if (spr_dir != 3)
                    {
                        Trigger();
                    }
                    GetComponent<Animator>().SetInteger("spr_dir", 3);
                }
            }
        }
    }

    // Called by WaveManager to scale difficulty
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void Trigger()
    {
        GetComponent<Animator>().ResetTrigger("trigger");
        GetComponent<Animator>().SetTrigger("trigger");
    }
}
