using UnityEngine;

public class GetPatient : GAction
{
    GameObject resource;
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target == null )
        {
            GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
            return false;
        }

        resource = GWorld.Instance.RemoveCubicle();
        if (resource != null)
            inventory.AddItem(resource);
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;
            GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
            return false;
        }

        target.GetComponent<GAgent>().InTreatment = true;
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }

    public override bool PostPerform()
    {
        if(target)
            target.GetComponent<GAgent>().inventory.AddItem(resource);
        GWorld.Instance.RemovePatient();
        return true;
    }
}
