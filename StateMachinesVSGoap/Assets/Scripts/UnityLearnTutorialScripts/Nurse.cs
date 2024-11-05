using UnityEngine;

public class Nurse : GAgent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("treatPatient", 1, true);
        goals.Add(s1, 3);
    }
}
