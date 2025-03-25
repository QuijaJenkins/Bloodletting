

using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 moveDir;

    void Start()
    {
        // Always move left with a slight Y offset for flavor
        float y = Random.Range(-0.3f, 0.3f);
        moveDir = new Vector2(-1f, y).normalized;
    }

    void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // Bounce off top/bottom to stay within screen vertically
        if (viewportPos.y <= 0f || viewportPos.y >= 1f)
        {
            moveDir.y *= -1f;
        }

        // Optional: destroy if completely off the left edge
        if (viewportPos.x < -0.2f)
        {
            Destroy(gameObject);
        }
    }
}
