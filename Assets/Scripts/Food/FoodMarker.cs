using UnityEngine;

public class FoodMarker : MonoBehaviour
{
    public bool OnConveyor { get; private set; } = true;

    [SerializeField] private FoodTypes _type;
    public FoodTypes Type => _type;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            Destroy(gameObject);
        else if (collision.gameObject.CompareTag("Conveyor"))
        {
            Debug.Log("On Conveyor");
            OnConveyor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor"))
        {
            Debug.Log("Off Conveyor");
            OnConveyor = false;
        }
    }
}
