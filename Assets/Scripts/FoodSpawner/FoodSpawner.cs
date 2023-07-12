using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _foodPrefabs = new();
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnRateInSeconds = 1.5f;
    [SerializeField] private bool _keepSpawning = false;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    public IEnumerator StartSpawning()
    {
        while (_keepSpawning)
        {
            GameObject food = Instantiate(GetRandomFood());
            food.transform.position = _spawnPoint.transform.position;
            food.transform.rotation = Quaternion.identity;
            yield return new WaitForSeconds(_spawnRateInSeconds);
        }
    }

    private GameObject GetRandomFood() => _foodPrefabs[Random.Range(0, _foodPrefabs.Count)];
}
