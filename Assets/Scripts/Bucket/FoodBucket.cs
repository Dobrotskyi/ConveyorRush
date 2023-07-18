using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodBucket : MonoBehaviour
{
    public static event Action ItemStored;

    [SerializeField] private List<Transform> _foodPlacementPoints;
    private int _itemsInside = 0;
    private GameObject _itemToStore;

    public void WaitForObject(GameObject gameObject) => _itemToStore = gameObject;

    private void StoreItem(GameObject itemToStore)
    {
        if (_itemsInside >= _foodPlacementPoints.Count)
            return;

        itemToStore.GetComponent<FoodMarker>().BeingStored = true;

        itemToStore.transform.SetParent(_foodPlacementPoints[_itemsInside]);
        itemToStore.transform.localPosition = Vector3.zero;
        itemToStore.transform.localRotation = Quaternion.identity;
        itemToStore.transform.localScale /= 2;
        _itemsInside++;
        itemToStore = null;
        ItemStored?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_itemToStore != null && other.gameObject == _itemToStore)
            StoreItem(_itemToStore);
    }
}
