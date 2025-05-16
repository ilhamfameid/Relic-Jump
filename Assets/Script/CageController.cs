using UnityEngine;

public class CageController : MonoBehaviour {
    public int requiredEnemyDefeated = 1;

    private void OnEnable() {
        GameManagerSystem.OnEnemyDefeated += CheckUnlock;
    }

    private void OnDisable() {
        GameManagerSystem.OnEnemyDefeated -= CheckUnlock;
    }

    void CheckUnlock(int defeated, int total) {
        if (defeated >= requiredEnemyDefeated) {
            Unlock();
        }
    }

    void Unlock() {
        // Bisa animasi juga di sini sebelum hilang
        Debug.Log("Kurungan terbuka!");
        Destroy(gameObject); // hilangkan kurungan
    }
}
