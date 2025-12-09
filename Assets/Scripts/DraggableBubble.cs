using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableBubble : Button
{
    public OptionData data;
    public float lerpSpeed;

    private RectTransform _rect;
    private Vector3 _originalScale;
    
    private TextMeshProUGUI _text;
    private bool _dragging;
    private Canvas _canvas;
    private Vector2 _dragOffset = Vector2.zero;

    private Sequence s;

    protected override void Awake()
    {
        base.Awake();
        _rect = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _canvas = GetComponentInParent<Canvas>();
        
        _originalScale = _rect.localScale;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _dragging = true;

        SetDragOffset(eventData.position);
        
        Squeeze();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _dragging = false;
        
        Release();
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

    public void SetUp(OptionData Data)
    {
        data = Data;
        _text.text = data.text;
    }

    private void Update()
    {
        if (_dragging)
            HandleDrag();
    }

    private void HandleDrag()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 localPointerPosition;

        if (_canvas != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rect.parent as RectTransform,
                mouseScreenPos,
                _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
                out localPointerPosition);

            Vector2 targetPos = localPointerPosition + _dragOffset;
            _rect.localPosition = Vector3.Lerp(_rect.localPosition, targetPos, Time.deltaTime * lerpSpeed);
        }
    }
    
    private void SetDragOffset(Vector2 screenPoint)
    {
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rect.parent as RectTransform,
            screenPoint,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out localPointerPosition);

        _dragOffset = _rect.localPosition - (Vector3)localPointerPosition;
    }

    private void Squeeze()
    {
        s.Kill();
        s = DOTween.Sequence();
        
        Ease ease = UIManager.Instance.Settings.squeezeEase;
        float speed = UIManager.Instance.Settings.squeezeSpeed;
        float scaleMultiplier = (100 - UIManager.Instance.Settings.squeezePercent) / 100f;
        
        Vector3 squeezeScale = _rect.localScale * scaleMultiplier;
        s.Append(_rect.DOScale(squeezeScale, speed).SetEase(ease));
        s.Play();
    }
    
    private void Release()
    {
        s.Kill();
        s = DOTween.Sequence();
        
        Ease ease = UIManager.Instance.Settings.squeezeEase;
        float speed = UIManager.Instance.Settings.squeezeSpeed;
        
        s.Append(_rect.DOScale(_originalScale, speed).SetEase(ease));
        s.Play();
    }
}