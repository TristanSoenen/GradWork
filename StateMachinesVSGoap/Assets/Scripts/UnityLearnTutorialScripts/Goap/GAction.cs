using System.Collections.Generic;
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
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool actionRunning = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
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
