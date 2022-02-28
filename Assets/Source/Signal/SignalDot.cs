using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SignalDot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetSignalIsActive(bool isActive) {
        spriteRenderer.color = isActive ? Color.green : Color.red;
    }

    public void SetSignalIsVisible(bool isVisible) {
        spriteRenderer.enabled = isVisible;
    }
}
