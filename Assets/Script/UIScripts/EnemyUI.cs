using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour {
    public TextMeshProUGUI enemyText;

    private void OnEnable() {
        GameManagerSystem.OnEnemyDefeated += UpdateEnemyUI;
    }

    private void OnDisable() {
        GameManagerSystem.OnEnemyDefeated -= UpdateEnemyUI;
    }

    void UpdateEnemyUI(int current, int total) {
        enemyText.text = $"Enemies: {current}/{total}";
    }
}
