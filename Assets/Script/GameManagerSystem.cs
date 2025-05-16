// Di GameManagerSystem.cs
using UnityEngine;
using System;

public class GameManagerSystem : MonoBehaviour {
    public static GameManagerSystem Instance;

    public int totalRelics = 2;
    public int collectedRelics = 0;

    public int totalEnemies = 2;
    public int defeatedEnemies = 0;

    public static event Action<int, int> OnRelicCollected;
    public static event Action<int, int> OnEnemyDefeated;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddRelic() {
        collectedRelics++;
        Debug.Log($"Relic Collected: {collectedRelics}/{totalRelics}");
        OnRelicCollected?.Invoke(collectedRelics, totalRelics);
        CheckWinCondition();
    }

    public void EnemyDefeated() {
        defeatedEnemies++;
        Debug.Log($"Enemy Defeated: {defeatedEnemies}/{totalEnemies}");
        OnEnemyDefeated?.Invoke(defeatedEnemies, totalEnemies);
        CheckWinCondition();
    }

    void CheckWinCondition() {
        if (collectedRelics >= totalRelics && defeatedEnemies >= totalEnemies) {
            Debug.Log("YOU WIN!");
            
        }
    }

}
