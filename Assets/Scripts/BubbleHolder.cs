using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleHolder : MonoBehaviour
{
    // The bubble currently inside this {holder. Null is default (empty).
    [SerializeField] private OptionBubble currentBubble = null;

    // List of possible bubbles that could be placed in this holder.
    [SerializeField] private List<OptionBubble> possibleBubbles = new List<OptionBubble>();

    private Vector2 _startingSize;
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _startingSize = _rect.localScale;
    }

    public void MatchSize()
    {
        Debug.Log("Not implemented yet");
        return;
    }
    
    public List<OptionBubble> GetPossibleBubbles() => possibleBubbles;

    public OptionBubble CurrentBubble
    {
        get => currentBubble;
        set => currentBubble = value;
    }
}