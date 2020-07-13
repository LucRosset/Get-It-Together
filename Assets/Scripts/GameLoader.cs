using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen = null;

    // Cached references
    GameMemory memory;

    void Start()
    {
        // Cache references
        memory = FindObjectOfType<GameMemory>();
    }

    public void Startgame()
    {
        memory.cometHit = false;
        memory.ClearMemory();
        StartCoroutine(WaitToLoad(3f, "Space"));
    }

    public void Restart(float delay)
    {
        StartCoroutine(WaitToLoad(delay, "Space"));
    }

    public void LoadMenu()
    {
        StartCoroutine(WaitToLoad(5f, "Menu"));
        memory.ClearMemory();
    }

    IEnumerator WaitToLoad(float delay, string scene)
    {
        Instantiate(
            fadeScreen,
            Camera.main.transform.position + Vector3.forward,
            Quaternion.identity
        );
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
