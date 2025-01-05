using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Text;

public class PatientSpawner : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;
    private int _maxPatients = 100000;
    private int _currentAmountSpawned;
    public float spawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        }

        Invoke("SpawnPatient", spawnTime);
    }

    void SpawnPatient()
    {

        for (int i = 0; i < numPatients; i++)
        {
                Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
                ++_currentAmountSpawned;
        }
        if(_currentAmountSpawned < _maxPatients)
            Invoke("SpawnPatient", spawnTime);
    }
}
