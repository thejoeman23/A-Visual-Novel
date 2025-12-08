using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic behind the Options Panel
/// </summary>
public class BubbleManager : Singleton<BubbleManager>
{
    private List<BubbleSlot> _bubbleSlots;
    private BubbleSlot _activeBubbleSlot;

    private void Update()
    {
        UpdateBubbleSlots();
    }
    
    private void UpdateBubbleSlots()
    {
        // Get all bubble slots in scene
        _bubbleSlots = new List<BubbleSlot>
            (FindObjectsByType<BubbleSlot>(FindObjectsSortMode.InstanceID));
        
        // Save reference to previous one. Will check for difference later
        var previousActiveBubbleSlot = _activeBubbleSlot;
        
        foreach (BubbleSlot bubbleHolder in _bubbleSlots)
        {
            // If the slot is empty, then set it as the one that is active, since the other one's are filled and cant be changed (inavtive)
            if (bubbleHolder.Content == null)
            {
                _activeBubbleSlot = bubbleHolder;
                break;
            }
        }
        
        // If active bubble slot has changed, run the function
        if (previousActiveBubbleSlot != _activeBubbleSlot)
            OnActiveBubbleSlotChanged();
    } 

    private void OnActiveBubbleSlotChanged()
    {
        ClearOldOptionBubbles();
        
        if (_activeBubbleSlot != null)
            SpawnNewOptionBubbles();
        else
            Debug.LogError("No active bubble holder found");
    }

    private void SpawnNewOptionBubbles()
    {
        if (MissingReferences())
            return;
        
        foreach (var option in _activeBubbleSlot.GetOptions())
        {
            SpawnOptionBubble(option);
        }
    }
    
    private void ClearOldOptionBubbles()
    {
        foreach (var bubble in GetOptionBubbles())
        {
            bubble.TweenOut();
        }
    }
    
    // Gets option bubbles inside of the option bubble panel
    private List<DraggableBubble> GetOptionBubbles() => UIManager.Instance.GetOptionBubbles();
    
    // Spawns option bubble INSIDE of the option bubble panel
    private void SpawnOptionBubble(OptionData data) => UIManager.Instance.SpawnOptionBubble(data);
    
    // Check for any missing references
    private bool MissingReferences() => UIManager.Instance.OptionReferencesExist();
}