using System.Collections;
using UnityEngine;

public enum Modules
{
    boost,
    move,
    stabilizer,
    shooter,
    tempControl,
    communicator,
    afterburner,
    hyperspace,
    navigationSystem,
    hyperFuel
}

[RequireComponent(typeof(PlayerBoost), typeof(PlayerMove), typeof(PlayerTemperature))]
[RequireComponent(typeof(PlayerShoot), typeof(BlackHoleDetector))]
public class ModulePanel : MonoBehaviour
{
    // Cached references
    GameMemory memory;
    PlayerBoost boost;
    PlayerMove move;
    PlayerTemperature tempControl;
    PlayerShoot shooter;
    
    [Header("Values to set for modules")]
    [Tooltip("Acceleration for unstable player movement")]
    [SerializeField] float unstableAccel = 3f;
    [Tooltip("Acceleration for stable player movement")]
    [SerializeField] float stableAccel = 8f;
    [Tooltip("Max speed without afterburner")]
    [SerializeField] float lowSpeed = 5f;
    [Tooltip("Max speed with afterburner")]
    [SerializeField] float highSpeed = 7f;
    float accel;

    // References to sprite renderers of each ship part
    SpriteRenderer leftWingSprite, rightWingSprite, engineSprite, communicatorSprite, gunSprite, hyperspaceSprite;
    // References to UI icons
    GameObject hyperspaceIcon, navigationSystemIcon, hyperFuelIcon, afterburnerIcon;

    void Start()
    {
        string prefix = "/Player/Body/";
        // Cache references
        memory = FindObjectOfType<GameMemory>();
        boost = GetComponent<PlayerBoost>();
        move = GetComponent<PlayerMove>();
        tempControl = GetComponent<PlayerTemperature>();
        shooter = GetComponent<PlayerShoot>();
        leftWingSprite = GameObject.Find(prefix+"Left Wing").GetComponent<SpriteRenderer>();
        rightWingSprite = GameObject.Find(prefix+"Right Wing").GetComponent<SpriteRenderer>();
        engineSprite = GameObject.Find(prefix+"Engine").GetComponent<SpriteRenderer>();
        communicatorSprite = GameObject.Find(prefix+"Communicator").GetComponent<SpriteRenderer>();
        gunSprite = GameObject.Find(prefix+"Gun").GetComponent<SpriteRenderer>();
        hyperspaceSprite = GameObject.Find(prefix+"Hyperspace").GetComponent<SpriteRenderer>();
        hyperspaceIcon = GameObject.Find("/UI Canvas/Hyperspace");
        navigationSystemIcon = GameObject.Find("/UI Canvas/Navigation System");
        hyperFuelIcon = GameObject.Find("/UI Canvas/Hyper Fuel");
        afterburnerIcon = GameObject.Find("/UI Canvas/Afterburner");

        if (memory.cometHit) { GetAllComponentStates(); }
    }

    private void GetAllComponentStates()
    {
        if (memory.cometHit)
        {
            SetAllComponentStates();
            return;
        }

        bool state = memory.move;
            move.enabled = state;
            engineSprite.enabled = state;
        state = memory.stabilizer;
            accel = (state) ? stableAccel : unstableAccel;
            leftWingSprite.enabled = state;
            rightWingSprite.enabled = state;
        state = memory.afterburner;
            move.SetMaxSpeed( (state) ? highSpeed : lowSpeed );
            afterburnerIcon.SetActive(state);
        tempControl.SetTempControlActive(memory.tempControl);
        state = memory.shooter;
            shooter.enabled = state;
            gunSprite.enabled = state;
        state = memory.communicator;
            // TODO: Communicator
            communicatorSprite.enabled = state;
        state = memory.hyperspace;
            // TODO: Hyperspace
            hyperspaceSprite.enabled = state;
            hyperspaceIcon.SetActive(state);
        state = memory.hyperFuel;
            // TODO: hyperfuel
            hyperFuelIcon.SetActive(state);
        state = memory.navigationSystem;
            // TODO: navigation --> On Memory???
            navigationSystemIcon.SetActive(state);
    }

    /// <summary>
    /// Save all states to game memory
    /// </summary>
    public void SetAllComponentStates()
    {
        memory.move = move.enabled;
        memory.stabilizer = leftWingSprite.enabled;
        memory.afterburner = afterburnerIcon.activeSelf;
        memory.tempControl = tempControl.GetTempControlActive();
        memory.shooter = shooter.enabled;
        memory.communicator = communicatorSprite.enabled;
        memory.hyperspace = hyperspaceSprite.enabled && !memory.cometHit;
        memory.hyperFuel = hyperFuelIcon.activeSelf && !memory.cometHit;
        memory.navigationSystem = navigationSystemIcon.activeSelf && !memory.cometHit;
    }

    public void Upgrade(Modules module)
    {
        Debug.Log("Got upgrade " + module.ToString());
        int moduleIndex= (int)module;
        switch (module)
        {
            case Modules.boost:
                memory.cometHit = true;
                break;

            case Modules.move:
                memory.move = true;
                move.enabled = true;
                move.SetAcceleration(accel);
                engineSprite.enabled = true;
                break;

            case Modules.stabilizer:
                memory.stabilizer = true;
                accel = stableAccel;
                move.SetAcceleration(accel);
                leftWingSprite.enabled = true;
                rightWingSprite.enabled = true;
                break;

            case Modules.shooter:
                memory.shooter = true;
                shooter.enabled = true;
                gunSprite.enabled = true;
                break;

            case Modules.tempControl:
                memory.tempControl = true;
                tempControl.SetTempControlActive(true);
                break;

            case Modules.communicator:
                memory.communicator = true;
                // TODO: Flag checkpoints enabled
                communicatorSprite.enabled = true;
                break;

            case Modules.afterburner:
                memory.afterburner = true;
                move.SetMaxSpeed(highSpeed);
                afterburnerIcon.SetActive(true);
                break;

            case Modules.hyperspace:
                memory.hyperspace = true;
                // TODO: Clear condition flag
                hyperspaceSprite.enabled = true;
                hyperspaceIcon.SetActive(true);
                break;

            case Modules.navigationSystem:
                memory.navigationSystem = true;
                // TODO: Clear condition flag
                navigationSystemIcon.SetActive(true);
                break;

            case Modules.hyperFuel:
                memory.hyperFuel = true;
                // TODO: Clear condition flag
                hyperFuelIcon.SetActive(true);
                break;

            default: break;
        }
    }

    public void DowngradeToBase()
    {
        // Ship parts and icons disabled
        leftWingSprite.enabled = false;
        rightWingSprite.enabled = false;
        engineSprite.enabled = false;
        accel = unstableAccel;
        move.SetAcceleration(accel);
        move.SetMaxSpeed(lowSpeed);
        communicatorSprite.enabled = false;
        gunSprite.enabled = false;
        hyperspaceSprite.enabled = false;
        hyperspaceIcon.SetActive(false);
        navigationSystemIcon.SetActive(false);
        hyperFuelIcon.SetActive(false);
        afterburnerIcon.SetActive(false);
        // Modules disabled
        boost.enabled = true;
        move.enabled = false;
        accel = unstableAccel;
        move.SetMaxSpeed(lowSpeed);
        tempControl.enabled = true;
        shooter.enabled = false;
        // Change game memory state!
        memory.cometHit = true;
    }
}
