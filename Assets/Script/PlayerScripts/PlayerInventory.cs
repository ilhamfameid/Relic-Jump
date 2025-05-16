using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public void AddRelic() {
        GameManagerSystem.Instance.AddRelic();
    }
}
