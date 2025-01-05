using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour
{
    public GameObject target;
    public StateAgent agent;
    public State nextState;
    public GInventory inventory;
    public WorldStates worldStates;
    public WorldStates beliefs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Awake()
    {
        agent = GetComponent<StateAgent>();
        inventory = agent.inventory;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }
}
