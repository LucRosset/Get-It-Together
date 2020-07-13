using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroySequence : DestroySequence
{
    // Cached references
    ModulePanel panel;
    Collider2D myCollider;
    GameLoader loader;

    void Start()
    {
        // Cache references
        panel = GetComponent<ModulePanel>();
        myCollider = GetComponent<Collider2D>();
        loader = FindObjectOfType<GameLoader>();
    }

    public override void Destroyed()
    {
        myCollider.enabled = false;
        panel.SetAllComponentStates();
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        Time.timeScale = .5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        loader.Restart(3f);
        base.Destroyed();
    }
}
