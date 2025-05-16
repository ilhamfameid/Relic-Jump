using UnityEngine;

public class RelicPickup : MonoBehaviour {
    public bool canBePicked = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!canBePicked) return; // Jangan bisa diambil jika belum diaktifkan

        if (other.CompareTag("Player")) {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null) {
                playerInventory.AddRelic();
                Destroy(gameObject); // Hapus relic dari scene
            }
        }
    }
}
