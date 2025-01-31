using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float actionCost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float actionDuration;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public GAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public GInventory inventory;
    public WorldStates beliefs;

    public bool actionRunning = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<GAgent>();
        if (preConditions != null)
        {
            foreach(WorldState worldStates in preConditions)
            {
                preconditions.Add(worldStates.key, worldStates.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (WorldState worldStates in afterEffects)
            {
                effects.Add(worldStates.key, worldStates.value);
            }
        }

        inventory = this.GetComponent<GAgent>().inventory;
        beliefs = this.GetComponent<GAgent>().beliefs;
    }

    public bool IsAchievalbe()
    { 
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if(!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
