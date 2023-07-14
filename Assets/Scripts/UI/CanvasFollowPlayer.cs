using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new(0, 2.7f, 0);
    private Transform _playerTransform;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        _canvas.transform.position = _playerTransform.position + _offset;
    }
}
