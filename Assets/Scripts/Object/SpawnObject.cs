using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour, ITriggerable
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPosition;
    
    public void Trigger()
    {
        Instantiate(prefab, spawnPosition.position, Quaternion.identity);
    }
}
