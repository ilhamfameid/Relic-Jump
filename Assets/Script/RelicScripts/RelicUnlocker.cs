using UnityEngine;

public class RelicUnlocker : MonoBehaviour {
    public GameObject relicToGuard;

    public void UnlockRelic() {
        if (relicToGuard != null) {
            Collider2D col = relicToGuard.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;
        }
    }
}
