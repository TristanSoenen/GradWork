using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld 
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> patients;
    private static Queue<GameObject> cubicles;

    static GWorld()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach(GameObject c in cubes)
        {
            cubicles.Enqueue(c);
        }

        if (cubes.Length > 0)
            world.ModifyState("FreeCubicle", cubes.Length);
    }

    private GWorld()
    {

    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    { 
        return world; 
    }

    public void AddPatient(GameObject p)
    {
        patients.Enqueue(p);
    }

    public GameObject RemovePatient()
    {
        if (patients.Count == 0)
            return null;

        return patients.Dequeue();
    }

    public void AddCubicle(GameObject c)
    {
        cubicles.Enqueue(c);
    }

    public GameObject RemoveCubicle()
    {
        if (cubicles.Count == 0)
            return null;

        return cubicles.Dequeue();
    }
}
