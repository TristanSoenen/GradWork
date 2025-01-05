using UnityEngine;

public class StateGetPatient : State
{
    GameObject resource;
    bool invoked = false;
    public override void OnStateEnter()
    {
        nextState = gameObject.AddComponent<StateGoToCubicle>();
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if (invoked)
            return;

        target = GWorld.Instance.RemovePatient();
        if (target == null)
            return;

        resource = GWorld.Instance.RemoveCubicle();
        if (resource != null)
            inventory.AddItem(resource);
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;
            return;
        }

        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        if(invoked == false && target != null)
        {
            agent.transform.position = target.transform.position;
            Invoke("ChangeState", 0.01f);
            invoked = true;
        }
    }

    public override void OnStateExit()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if(target )
        {
            target.GetComponent<StateAgent>().inventory.AddItem(resource);
            target.GetComponent<StateAgent>().canGetTreated = true;
        }
    }
}

public class StateGoToCubicle : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        target = inventory.FindItemWithTag("Cubicle");
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if (invoked || !target) return;

        agent.transform.position = target.transform.position;
        Invoke("ChangeState", 0.01f);
        invoked = true;
    }

    public override void OnStateExit()
    {
        GWorld.Instance.AddCubicle(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        if (agent.CoffeeBreak)
            nextState = gameObject.AddComponent<StateCoffeeBreak>();
        else
            nextState = gameObject.AddComponent<StateGetPatient>();
    }
}

public class StateCoffeeBreak : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        GWorld.Instance.GetWorld().ModifyState("CoffeeBreak", 1);
        const string tag = "Lounge";
        target = GameObject.FindWithTag(tag);
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if(invoked || !target) return;

        agent.transform.position = target.transform.position;
        Invoke("ChangeState", 0.01f);
        invoked = true;
    }

    public override void OnStateExit()
    {
        agent.CoffeeBreak = false;
        nextState = gameObject.AddComponent<StateGetPatient>();
    }
}
