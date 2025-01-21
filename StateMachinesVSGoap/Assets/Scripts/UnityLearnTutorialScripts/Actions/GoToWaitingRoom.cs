using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
        GWorld.Instance.AddPatient(this.gameObject);
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("atHospital", 1);
        agent.startTimer = true;
        return true;
    }
}
