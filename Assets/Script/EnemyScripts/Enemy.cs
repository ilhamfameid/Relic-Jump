using UnityEngine;
using System;

public class Enemy : MonoBehaviour {
    //public static event Action<Enemy> OnEnemyDefeated;

    public float moveSpeed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    //public GameObject relicPrefab;
    public GameObject guardedRelic;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private int hitCount = 0;
    public int maxHit = 3;

    public Transform wallCheck; // assign dari Inspector
    public float wallCheckDistance = 0.1f; // jarak deteksi dinding


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Patrol();
    }

    void Patrol() {
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1.5f, groundLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, wallCheckDistance, groundLayer);

        if (!groundInfo.collider || wallInfo.collider) {
            Flip();
        }
    }


    void Flip() {
        movingRight = !movingRight;
        transform.eulerAngles = new Vector3(0, movingRight ? 0 : 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 center = GetComponent<Collider2D>().bounds.center;

            // Cek apakah ditabrak dari atas
            if (contactPoint.y > center.y) {
                hitCount++;
                if (hitCount >= maxHit) {
                    Defeat();
                }
            } else {
                // (Optional) Damage ke player jika dari samping
                collision.collider.GetComponent<PlayerScript>()?.Hit();
            }
        }
    }

    void Defeat() {
        // Aktifkan relic yang dijaga
        if (guardedRelic != null) {
            RelicPickup relicScript = guardedRelic.GetComponent<RelicPickup>();
            if (relicScript != null) relicScript.canBePicked = true;

            Collider2D col = guardedRelic.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }

        GameManagerSystem.Instance.EnemyDefeated();
        Destroy(gameObject);
    }


}
