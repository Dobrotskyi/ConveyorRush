using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodBucket : MonoBehaviourSingleton<FoodBucket>
{
    public static event Action ItemStored;

    [SerializeField] private List<Transform> _foodPlacementPoints;
    private int _itemsInside = 0;
    private GameObject _itemToStore;

    public void WaitForObject(GameObject gameObject) => _itemToStore = gameObject;

    private void StoreItem()
    {
        if (_itemsInside >= _foodPlacementPoints.Count)
            return;

        TempFuncDisableRigidbody();
        _itemToStore.transform.SetParent(_foodPlacementPoints[_itemsInside]);
        _itemToStore.transform.localPosition = Vector3.zero;
        _itemToStore.transform.localScale /= 2;
        _itemsInside++;
        ItemStored?.Invoke();
        _itemToStore = null;
    }

    private void TempFuncDisableRigidbody()
    {
        Rigidbody rb = _itemToStore.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_itemToStore != null && other.gameObject == _itemToStore)
            StoreItem();
    }
}
