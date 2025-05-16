using UnityEngine;
using TMPro;

public class RelicUI : MonoBehaviour {
    public TextMeshProUGUI relicText;
    public GameManager gameManager;

    private void OnEnable() {
        GameManagerSystem.OnRelicCollected += UpdateRelicUI;
    }

    private void OnDisable() {
        GameManagerSystem.OnRelicCollected -= UpdateRelicUI;
    }

    void UpdateRelicUI(int current, int total) {
        relicText.text = $"Relics: {current}/{total}";
    }
}
