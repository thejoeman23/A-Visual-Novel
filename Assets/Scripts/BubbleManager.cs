using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [Tooltip("The the panel that contains the text bubble options.")]
    [SerializeField] private GameObject OptionBubblePanel;
    [SerializeField] private GameObject DraggableBubblePrefab;
    
    private List<BubbleHolder> _bubbleHolders;
    private BubbleHolder _activeBubbleHolder;

    private void Start()
    {
        UpdateBubbeHolders();
        
    }

    private void SpawnOptionBubbles()
    {
        if (_activeBubbleHolder == null)
        {
            Debug.LogError("No active bubble holder found");
            return;
        }

        if (OptionBubblePanel == null || DraggableBubblePrefab == null)
        {
            Debug.LogError("Mandatory references not set.");
            return;
        }

        foreach (var option in _activeBubbleHolder.GetPossibleBubbles())
        {
            SpawnBubble(option);
        }
    }

    private void SpawnBubble(OptionBubble data)
    {
        GameObject obj = Instantiate(DraggableBubblePrefab, OptionBubblePanel.transform);
        TextMeshProUGUI textObj = obj.GetComponentInChildren<TextMeshProUGUI>();
        textObj.text = data.text;
    }

    private void ClearOptionBubbles()
    {
        if (OptionBubblePanel == null)
        {
            Debug.LogError("Missing reference to OptionBubblePanel.");
            return;
        }

        foreach (var DraggableBubble in )
        {
            
        }
    }
    
    private void UpdateBubbeHolders()
    {
        _bubbleHolders = new List<BubbleHolder>
            (FindObjectsByType<BubbleHolder>(FindObjectsSortMode.InstanceID));

        foreach (BubbleHolder bubbleHolder in _bubbleHolders)
        {
            if (bubbleHolder.CurrentBubble == null)
            {
                _activeBubbleHolder = bubbleHolder;
                break;
            }
        }
    } 
}