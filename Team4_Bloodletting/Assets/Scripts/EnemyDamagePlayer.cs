using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    public int damageAmount = 15;
    private bool hasDamagedPlayer = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasDamagedPlayer && collision.gameObject.CompareTag("Player"))
        {
            GameHandler handler = FindObjectOfType<GameHandler>();
            if (handler != null)
            {
                handler.changeHealth(-damageAmount, false);
                hasDamagedPlayer = true;
                Destroy(gameObject); // Optional: remove enemy
            }
        }
    }
}


