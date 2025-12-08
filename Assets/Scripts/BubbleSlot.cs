using System.Collections.Generic;
using UnityEngine;

public class BubbleSlot : MonoBehaviour
{
    // The bubble currently inside this {holder. Null is default (empty).
    [SerializeField] private DraggableBubble content = null;

    // List of possible bubbles that could be placed in this holder.
    [SerializeField] private List<OptionData> possibleBubbles = new List<OptionData>();

    private Vector2 _startingSize;
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _startingSize = _rect.localScale;
    }

    public void Fill(DraggableBubble buble)
    {
        content = buble;
        UIManager.Instance.DrawPanel(content.Data);
    }

    public void MatchSize()
    {
        Debug.Log("Not implemented yet");
        return;
    }
    
    public List<OptionData> GetOptions() => possibleBubbles;

    public DraggableBubble Content
    {
        get => content;
        set => content = value;
    }
}