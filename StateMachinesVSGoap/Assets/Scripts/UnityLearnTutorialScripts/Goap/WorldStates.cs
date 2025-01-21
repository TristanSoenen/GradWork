using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public string key;
    public int value;
}

public class WorldStates
{
    public Dictionary<string, int> states;
    private int patientsTreatedCount;
    private int max = 100000;
    public int PatientTreatedCount
    {
        get { return patientsTreatedCount; }
        set 
        { 
            if(patientsTreatedCount < max)
                patientsTreatedCount = value;
 
            if(patientsTreatedCount >= max)
            {
                WriteTextFile();
            }
        }
    }

    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    public bool HasState(string key)
    { 
        return states.ContainsKey(key);
    }

    void AddState(string key, int value)
    {
        if (patientsTreatedCount >= max)
            return;
        states.Add(key, value);
    }

    public void ModifyState(string key, int value)
    {
        if (patientsTreatedCount >= max)
            return;

        if (states.ContainsKey(key))
        {
            states[key] += value;
            if (states[key] <=  0) 
               RemoveState(key);
        }
        else
            states.Add(key, value);
    }

    public void RemoveState(string key)
    {
        if(states.ContainsKey(key))
            states.Remove(key);
    }

    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
            states[key] = value;
        else
            states.Add(key, value);
    }

    public Dictionary<string, int> GetStates()
    { 
        return states;
    }

    void WriteTextFile()
    {
        //Chat gpt helped with the writing to file
        string directory = Directory.GetParent(Application.dataPath).FullName;
        string fileName = "Data.txt";
        string filePath = Path.Combine(directory, fileName);
        StringBuilder data = new StringBuilder();

        foreach (var state in states)
        {
            if (state.Key == "CoffeeBreak" || state.Key == "Treated" || state.Key == "Angry")
                data.AppendLine($"{state.Key}: {state.Value}");
        }

        File.WriteAllText(filePath, data.ToString());
        Application.Quit();
    }
}
