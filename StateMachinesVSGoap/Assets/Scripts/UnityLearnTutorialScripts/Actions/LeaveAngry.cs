using UnityEngine;

public class LeaveAngry : GAction
{
    public override bool PrePerform()
    {   
      return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
