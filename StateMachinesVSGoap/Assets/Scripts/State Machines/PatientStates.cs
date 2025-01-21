using System.Collections;
using UnityEngine;

public class StateGoToHospital : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        const string tag = "Door";
        target = GameObject.FindWithTag(tag);
        nextState = gameObject.AddComponent<StateGoRegister>();
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if(invoked == false && target != null)
        {
            agent.transform.position = target.transform.position;
            Invoke("ChangeState", 0.01f);
            invoked = true;
        }
    }
}

public class StateGoRegister : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        const string tag = "Reception";
        target = GameObject.FindWithTag(tag);
        nextState = gameObject.AddComponent<StateGoToWaitingRoom>();
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if (invoked == false && target != null)
        {
            agent.transform.position = target.transform.position;
            Invoke("ChangeState", 0.01f);
            invoked = true;
        }
    }
}

public class StateGoToWaitingRoom : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        const string tag = "WaitingArea";
        target = GameObject.FindWithTag(tag);
        nextState = gameObject.AddComponent<StateGetTreated>();
        agent.transform.position = target.transform.position;
        GWorld.Instance.AddPatient(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
        StartCoroutine(LeavesAngry());
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    public IEnumerator LeavesAngry()
    {
        yield return new WaitForSeconds(0.25f);
        GWorld.Instance.RemovePatient();
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        nextState = gameObject.AddComponent<StateLeaveAngry>();
        invoked = true;
        agent.ChangeState();
    }

    void LateUpdate()
    {
        if (agent.canGetTreated == false)
            return;

        if (invoked == false && target != null)
        {
            Invoke("ChangeState", 0.01f);
            invoked = true;
        }
    }
}

public class StateGetTreated : State
{
    bool invoked = false;
    public override void OnStateEnter()
    {
        target = inventory.FindItemWithTag("Cubicle");
        nextState = gameObject.AddComponent<StateGoHome>();
    }

    void ChangeState()
    {
        agent.ChangeState();
        invoked = false;
    }

    void LateUpdate()
    {
        if(invoked == false && target != null)
        {
            agent.transform.position = target.transform.position;
            Invoke("ChangeState", 0.01f);
            invoked = true;
        }
    }

    public override void OnStateExit()
    {
        GWorld.Instance.GetWorld().ModifyState("Treated", 1);
        GWorld.Instance.GetWorld().PatientTreatedCount++;
        inventory.RemoveItem(target);
    }
}

public class StateGoHome : State
{
    public override void OnStateEnter()
    {
        const string tag = "Home";
        target = GameObject.FindWithTag(tag);
        agent.transform.position = target.transform.position;
        Invoke("DestroyAgent", 0.01f);
    }

    void DestroyAgent()
    {
        Destroy(this.gameObject);
    }
}

public class StateLeaveAngry : State
{
    public override void OnStateEnter()
    {
        const string tag = "Home";
        target = GameObject.FindWithTag(tag);
        agent.transform.position = target.transform.position;
        GWorld.Instance.GetWorld().ModifyState("Angry", 1);
        Invoke("DestroyAgent", 0.01f);
    }

    void DestroyAgent()
    {
        GWorld.Instance.GetWorld().PatientTreatedCount++;
        Destroy(this.gameObject);
    }
}