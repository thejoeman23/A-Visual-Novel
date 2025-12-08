using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [Tooltip("The the panel that contains the text bubble options.")]
    [SerializeField] private GameObject optionBubblePanel;
    [SerializeField] private GameObject optionBubblePrefab;
    
    private List<BubbleSlot> _bubbleSlots;
    private BubbleSlot _activeBubbleSlot;

    private void Start()
    {
        UpdateBubbeHolders();
    }

    private void SpawnOptionBubbles()
    {
        if (_activeBubbleSlot == null)
        {
            Debug.LogError("No active bubble holder found");
            return;
        }

        if (optionBubblePanel == null || optionBubblePrefab == null)
        {
            Debug.LogError("Mandatory references not set.");
            return;
        }
        
        ClearOptionBubbles();

        foreach (var option in _activeBubbleSlot.GetPossibleBubbles())
        {
            SpawnBubble(option);
        }
    }

    private void SpawnBubble(OptionData data)
    {
        DraggableBubble bubble = Instantiate(optionBubblePrefab, optionBubblePanel.transform).GetComponent<DraggableBubble>();
        bubble.SetUp(data);
        bubble.TweenIn();
    }

    private void ClearOptionBubbles()
    {
        if (optionBubblePanel == null)
        {
            Debug.LogError("Missing reference to OptionBubblePanel.");
            return;
        }

        foreach (var bubble in optionBubblePanel.GetComponentsInChildren<DraggableBubble>())
        {
            bubble.TweenOut();
        }
    }
    
    private void UpdateBubbeHolders()
    {
        _bubbleSlots = new List<BubbleSlot>
            (FindObjectsByType<BubbleSlot>(FindObjectsSortMode.InstanceID));

        foreach (BubbleSlot bubbleHolder in _bubbleSlots)
        {
            // If the slot is empty, then set it as the one that is active, since the other one's are filled and cant be changed (inavtive)
            if (bubbleHolder.Content == null)
            {
                _activeBubbleSlot = bubbleHolder;
                break;
            }
        }
    } 
}