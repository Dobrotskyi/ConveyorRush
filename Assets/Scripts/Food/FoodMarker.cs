using UnityEngine;

public class FoodMarker : MonoBehaviour
{
    public bool OnConveyor { get; private set; } = true;
    public bool BeingStored = false;

    [SerializeField] private FoodTypes _type;
    public FoodTypes Type => _type;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            Destroy(gameObject);
        else if (collision.gameObject.CompareTag("Conveyor"))
            OnConveyor = true;

        else if (BeingStored)
        {
            if (collision.gameObject.CompareTag("FoodBucket") && !collision.collider.isTrigger)
                MakeKinematic();

            else if (collision.transform.TryGetComponent<FoodMarker>(out FoodMarker food) && food.BeingStored)
                MakeKinematic();
        }
    }

    private void MakeKinematic()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
            OnConveyor = false;
    }

    private void OnEnable()
    {
        SingletonTask.Instance.TaskCompleted += DestroyIfNotInStored;
    }

    private void OnDisable()
    {
        SingletonTask.Instance.TaskCompleted -= DestroyIfNotInStored;
    }

    private void DestroyIfNotInStored()
    {
        if (BeingStored)
            return;
        Destroy(gameObject);
    }
}
