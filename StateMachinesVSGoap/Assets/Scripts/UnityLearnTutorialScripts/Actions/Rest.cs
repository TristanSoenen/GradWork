using UnityEngine;

public class Rest : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("Exhausted");
        return true;
    }
}
