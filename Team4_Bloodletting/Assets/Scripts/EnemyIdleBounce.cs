using UnityEngine;

public class EnemyIdleBounce : MonoBehaviour
{
    public float bounceHeight = 0.2f;   // How far they bounce
    public float bounceSpeed = 2f;      // How fast they bounce

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position + new Vector3(0, Random.Range(-0.1f, 0.1f), 0);
    }

    void Update()
    {
        float yOffset = Mathf.Sin((Time.time + transform.position.x) * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
}

