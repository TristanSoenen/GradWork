using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null)
            return false;
        agent.InTreatment = true;
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Treated", 1);
        GWorld.Instance.GetWorld().PatientTreatedCount += 1;
        beliefs.ModifyState("isCured", 1);
        inventory.RemoveItem(target);
        return true;
    }
}
