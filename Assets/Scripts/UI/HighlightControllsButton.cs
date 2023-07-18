using UnityEngine;
using UnityEngine.UI;

public class HighlightControllsButton : MonoBehaviour
{
    [SerializeField] private Color _highlightedColor = Color.gray;
    [SerializeField] private bool _isMoveToRightButton;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        TouchHandler.ControllButtonReleased += DisableHighlight;
        if (_isMoveToRightButton)
            ControllsCanvas.MoveRightButtonPressed += EnableHighlight;
        else
            ControllsCanvas.MoveLeftButtonPressed += EnableHighlight;
    }

    private void OnDisable()
    {
        TouchHandler.ControllButtonReleased -= DisableHighlight;
        if (_isMoveToRightButton)
            ControllsCanvas.MoveRightButtonPressed -= EnableHighlight;
        else
            ControllsCanvas.MoveLeftButtonPressed -= EnableHighlight;
    }

    private void DisableHighlight()
    {
        if (_button.targetGraphic.color == _highlightedColor)
            _button.targetGraphic.color = Color.white;
    }

    private void EnableHighlight()
    {
        if (_button.targetGraphic.color != _highlightedColor)
            _button.targetGraphic.color = _highlightedColor;
    }
}
