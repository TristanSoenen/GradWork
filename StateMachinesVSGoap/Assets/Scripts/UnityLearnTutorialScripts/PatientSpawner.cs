using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;
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
        Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        Invoke("SpawnPatient", spawnTime);
    }
}
