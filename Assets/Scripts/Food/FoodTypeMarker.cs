using UnityEngine;

public class FoodTypeMarker : MonoBehaviour
{
    [SerializeField] private FoodTypes _type;
    public FoodTypes Type => _type;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            Destroy(gameObject);
    }
}
