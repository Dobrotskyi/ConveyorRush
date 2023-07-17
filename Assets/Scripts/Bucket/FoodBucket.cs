using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodBucket : MonoBehaviourSingleton<FoodBucket>
{
    public static event Action ItemStored;

    [SerializeField] private List<Transform> _foodPlacementPoints;
    [SerializeField] private GameObject _invisibleLid;
    private int _itemsInside = 0;
    private GameObject _itemToStore;

    public void WaitForObject(GameObject gameObject) => _itemToStore = gameObject;

    private void StoreItem()
    {
        if (_itemsInside >= _foodPlacementPoints.Count)
            return;

        _itemToStore.transform.SetParent(_foodPlacementPoints[_itemsInside]);
        _itemToStore.transform.localPosition = Vector3.zero;
        _itemToStore.transform.localRotation = Quaternion.identity;
        _itemToStore.transform.localScale /= 2;
        Physics.IgnoreCollision(_invisibleLid.GetComponent<Collider>(), _itemToStore.GetComponent<Collider>(), false);
        _itemsInside++;
        _itemToStore = null;
        ItemStored?.Invoke();
    }

    private void DisableRigidbody()
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
