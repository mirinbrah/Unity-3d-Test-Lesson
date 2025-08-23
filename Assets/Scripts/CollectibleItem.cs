using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnItemCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}