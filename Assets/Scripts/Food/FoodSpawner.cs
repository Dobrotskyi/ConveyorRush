using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _foodPrefabs = new();
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnRateInSeconds = 1.5f;
    [SerializeField] private bool _keepSpawning = false;

    private void OnEnable()
    {
        MainMenu.Instance.StartGame += StartSpawning;
    }

    private void OnDisable()
    {
        MainMenu.Instance.StartGame -= StartSpawning;
    }

    private void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_keepSpawning)
        {
            GameObject food = Instantiate(GetRandomFood(), _spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_spawnRateInSeconds);
        }
    }

    private GameObject GetRandomFood() => _foodPrefabs[Random.Range(0, _foodPrefabs.Count)];
}
