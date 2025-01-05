using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateAgent : MonoBehaviour
{
    public State currentState;
    public bool invoked = false;
    public bool nurse;
    public bool canGetTreated = false;
    private bool coffeeBreak = false;
    public bool CoffeeBreak 
    {
        get { return coffeeBreak; }
        set 
        { 
            coffeeBreak = value;
            if (value == false)
                StartCoroutine(CoffeeBreakTimer());
        }
    }
    public GInventory inventory = new GInventory();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!nurse)
            currentState = gameObject.AddComponent<StateGoToHospital>();
        else
        {
            currentState = gameObject.AddComponent<StateGetPatient>();
            CoffeeBreak = false;
        }

        currentState.OnStateEnter();
        if(currentState == null )
            Debug.Log(currentState.ToString());
    }

    public void ChangeState()
    {
        currentState.OnStateExit();
        State NextState = currentState.nextState;
        Destroy( currentState );
        currentState = NextState;
        currentState.OnStateEnter();
        invoked = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public IEnumerator CoffeeBreakTimer()
    {
        yield return new WaitForSeconds(0.25f);
        CoffeeBreak = true;
    }
}
