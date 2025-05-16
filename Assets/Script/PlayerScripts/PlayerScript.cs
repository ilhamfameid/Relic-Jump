using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public Transform spawnPoint;

    [Header("Sprite Renderers")]
    public PlayerSpriteRenderer idleRenderer;
    public PlayerSpriteRenderer walkRenderer;
    public PlayerSpriteRenderer jumpRenderer;
    // public PlayerSpriteRenderer slideRenderer;

    public bool dead => deathAnimation != null && deathAnimation.enabled;
    public bool starpower { get; private set; }

    private void Awake() {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
    }

    private void Update() {
        if (dead) return;

        // Ganti renderer aktif berdasarkan status gerakan (sesuai nama di PlayerMovement)
        if (movement.jumping) {
            SetActiveRenderer(jumpRenderer);
            // } else if (movement.sliding) {
            //     SetActiveRenderer(slideRenderer);
        } else if (movement.running) {
            SetActiveRenderer(walkRenderer);
        } else {
            SetActiveRenderer(idleRenderer);
        }
    }

    private void SetActiveRenderer(PlayerSpriteRenderer activeRenderer) {
        idleRenderer.enabled = activeRenderer == idleRenderer;
        walkRenderer.enabled = activeRenderer == walkRenderer;
        jumpRenderer.enabled = activeRenderer == jumpRenderer;
        // slideRenderer.enabled = activeRenderer == slideRenderer;
    }

    public void Hit() {
        if (!dead && !starpower) {
            Death();
        }
    }

    public void Death() {
        SetActiveRenderer(null);
        deathAnimation.enabled = true;
        StartCoroutine(RespawnAfterDelay(3f));
    }

    private IEnumerator RespawnAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        deathAnimation.enabled = false;
        transform.position = spawnPoint.position;
        SetActiveRenderer(idleRenderer);
    }


    public void Starpower() {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation() {
        starpower = true;
        float elapsed = 0f, duration = 10f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0) {
                Color rand = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                // warnai renderer yang sedang aktif
                if (idleRenderer.enabled) idleRenderer.spriteRenderer.color = rand;
                if (walkRenderer.enabled) walkRenderer.spriteRenderer.color = rand;
                if (jumpRenderer.enabled) jumpRenderer.spriteRenderer.color = rand;
                // if (slideRenderer.enabled) slideRenderer.spriteRenderer.color = rand;
            }
            yield return null;
        }

        // reset warna
        idleRenderer.spriteRenderer.color = Color.white;
        walkRenderer.spriteRenderer.color = Color.white;
        jumpRenderer.spriteRenderer.color = Color.white;
        // slideRenderer.spriteRenderer.color = Color.white;

        starpower = false;
    }
}
