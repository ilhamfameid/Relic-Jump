using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteRenderer : MonoBehaviour
{
    private PlayerMovement movement;
    public SpriteRenderer spriteRenderer { get; private set; }
    public AnimatedSprite idle;
    public AnimatedSprite jump;
    //public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        if (movement.jumping) {
            jump.enabled = true;
            idle.enabled = false;
            run.enabled = false;
        //} else if (movement.sliding) {
        //    spriteRenderer.sprite = slide;
        //    idle.enabled = false;
        //    run.enabled = false;
        } 
        else if (movement.running) {
            idle.enabled = false;
            run.enabled = true;
            jump.enabled = false;
        } else {
            idle.enabled = true;
            run.enabled = false;
            jump.enabled = false;
        }
    }


    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

}
