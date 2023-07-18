using UnityEngine;

public class FoodBucketPopUpShower : MonoBehaviour
{
    [SerializeField] private Canvas _canvasPrefab;
    [SerializeField] private Transform _spawnPoint;

    private void OnEnable()
    {
        FoodBucket.ItemStored += DisplayPopUp;
    }

    private void OnDisable()
    {
        FoodBucket.ItemStored -= DisplayPopUp;
    }

    public void DisplayPopUp()
    {
        Canvas popUpText = Instantiate(_canvasPrefab, _spawnPoint.position, Quaternion.identity);
    }
}
