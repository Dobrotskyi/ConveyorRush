using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasFollowTarget : MonoBehaviour
{
    public bool SetHeightAsOffsetY;

    [SerializeField] private Vector3 _offset = new(0, 2.7f, 0);
    [SerializeField] private Transform _targetTransform;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void LateUpdate()
    {
        Vector3 newPos = _targetTransform.position + _offset;
        if (SetHeightAsOffsetY)
            newPos.y = _offset.y;
        _canvas.transform.position = newPos;
    }
}
