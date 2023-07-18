using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    [SerializeField] private Color _highlightedColor = Color.gray;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        PointerEventData pointerEventData = new(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.CompareTag("ControllsButton"))
            {
                Button button = raycastResults[i].gameObject.GetComponent<Button>();
                if (button.Equals(_button))
                {
                    if (button.targetGraphic.color != _highlightedColor)
                        button.targetGraphic.color = _highlightedColor;
                    return;
                }
            }
        }
        if (_button.targetGraphic.color == _highlightedColor)
            _button.targetGraphic.color = Color.white;
    }
}
