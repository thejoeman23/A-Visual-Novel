using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableBubble : Button
{
    public OptionData Data { get; private set; }

    private RectTransform _rect;
    private TextMeshProUGUI _text;
    private bool _dragging;
    private Canvas _canvas;
    private Vector2 _dragOffset = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        _rect = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _dragging = true;

        // Convert mouse position to local point in parent rect (UI space)
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rect.parent as RectTransform,
            eventData.position,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out localPointerPosition);

        // The offset is where you clicked minus the bubble's current localPosition
        _dragOffset = _rect.localPosition - (Vector3)localPointerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _dragging = false;
    }

    private void Update()
    {
        if (_dragging)
        {
            float lerpSpeed = UIManager.Instance.Settings.followLerpSpeed;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 localPointerPosition;
            if (_canvas != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rect.parent as RectTransform,
                    mouseScreenPos,
                    _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
                    out localPointerPosition);

                // Apply offset so the gripped spot follows the cursor
                Vector2 targetPos = localPointerPosition + _dragOffset;
                _rect.localPosition = Vector3.Lerp(_rect.localPosition, targetPos, Time.deltaTime * lerpSpeed);
            }
        }
    }

    public void TweenOut()
    {
        Destroy(gameObject);
    }

    public void TweenIn()
    {
        _rect.localScale = Vector3.zero;
        // Optional: animate in
    }

    public void SetUp(OptionData data)
    {
        Data = data;
        _text.text = data.text;
    }
}