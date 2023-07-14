using UnityEngine;

public class FoodTypeMarker : MonoBehaviour
{
    [SerializeField] private FoodTypes _type;
    public FoodTypes Type => _type;
}
