using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour
{
    [SerializeField] GameObject cometPrefab = null;
    [SerializeField] Vector3 cometPosition = Vector3.zero;

    // Cached references
    GameObject comet;
    
    public bool cometHit { set; get; }
    public bool move { set; get; }
    public bool stabilizer { set; get; }
    public bool tempControl { set; get; }
    public bool shooter { set; get; }
    public bool communicator { set; get; }
    public bool afterburner { set; get; }
    public bool hyperspace { set; get; }
    public bool hyperFuel { set; get; }
    public bool navigationSystem { set; get; }

    void Awake()
    {
        int checkpoints = FindObjectsOfType<GameMemory>().Length;
        if (checkpoints > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    void Start()
    {
        // Starts at the menu. When first loading the game, will have cometHit set to false
        cometHit = true;
        ClearMemory();
    }

    void Update()
    {
        // Check if has no reference to module panel
        if (!cometHit && !comet)
        {
            // Create comet
            comet = Instantiate(
                cometPrefab,
                cometPosition,
                Quaternion.identity
            );
        }

        // Check win condition
        if (hyperspace && hyperFuel && navigationSystem)
        {
            // WIN THE GAME!!!
            GameObject.Find("/Player/Body").GetComponent<Animator>().SetTrigger("win");
        }
    }

    public void ClearMemory()
    {
        move = stabilizer = tempControl = shooter = communicator =
            afterburner = hyperspace = hyperFuel = navigationSystem = false;
    }
}
