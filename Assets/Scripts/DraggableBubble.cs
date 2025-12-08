using DG.Tweening;
using UnityEngine;

public class DraggableBubble : MonoBehaviour
{
    public OptionData Data;
    
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void TweenOut()
    {
        // Tween out (believe it or not)
        
        Destroy(gameObject);
    }

    public void TweenIn()
    {
        _rect.localScale = Vector3.zero;
        
        // Tween in
    }
    
    public void SetUp(OptionData data)
    {
        Data = data;
    }
}