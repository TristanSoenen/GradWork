using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public GInventory inventory = new GInventory(); 
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public WorldStates beliefs = new WorldStates();
    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;
    public bool startTimer = false;
    public bool InTreatment = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction act in acts) 
            actions.Add(act);
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.actionRunning = false;
        currentAction.PostPerform();
        invoked = false;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (startTimer)
            Invoke("GetAngry", 60.0f);

        if (currentAction != null && currentAction.actionRunning)
        {
            if (currentAction.target != null)
            {
                if (!invoked)
                {
                    this.transform.position = currentAction.target.transform.position;
                    Invoke("CompleteAction", currentAction.actionDuration);
                    invoked = true;
                }
            }
            return;
        }

        //Agent has no plan to work on
        if (planner ==  null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }
        
        if(actionQueue != null && actionQueue.Count == 0)
        {
            if(currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if(actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if(currentAction.PrePerform())
            {
                if(currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                if(currentAction.target != null)
                {
                    currentAction.actionRunning = true;
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }

    void GetAngry()
    {
        if (InTreatment)
            return;
        GWorld.Instance.GetWorld().ModifyState("Angry", 1);
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        GWorld.Instance.RemovePatient();
        GWorld.Instance.GetWorld().PatientTreatedCount += 1;
        Destroy(this.gameObject);
    }
}
