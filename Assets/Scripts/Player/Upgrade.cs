using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Cached references
    Animator animator;

    [Tooltip("Module this upgrade gives the player")]
    [SerializeField] Modules module = Modules.move;
    [SerializeField] AudioClip soundEffect = null;

    void Start()
    {
        // Cache references
        animator = GetComponent<Animator>();
    }

    public void setModule(Modules module){
        this.module = module;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Upgrades should only collide with Player layer
        collider.GetComponent<ModulePanel>().Upgrade(module);
        if (soundEffect)
        {
            AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position);
        }
        animator.SetTrigger("gotUpgrade");
    }

    public void SelfDestruct() { Destroy(gameObject); }
}
