using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UISettings Settings;

    [Header("Bubble Option References")] 
    [SerializeField] private GameObject optionBubblePrefab;
    [SerializeField] private GameObject optionBubblePanel;

    public void DrawPanel(OptionData data)
    {
        
    }
    
    public void DisableOptionPanel() // To do 
    {
        optionBubblePanel.SetActive(false);
    }
    
    public void EnableOptionPanel() // To do 
    {
        optionBubblePanel.SetActive(true);
    }

    public bool OptionReferencesExist()
    {
        if (optionBubblePanel == null)
        {
            Debug.LogError("Option Bubble Panel is null");
            return false;
        }

        if (optionBubblePrefab == null)
        {
            Debug.LogError("Missing reference to Option Bubble Prefab");
            return false;
        }
        
        return true;
    }

    public List<DraggableBubble> GetOptionBubbles()
    {
        return optionBubblePanel.GetComponentsInChildren<DraggableBubble>().ToList();
    }
    
    public void SpawnOptionBubble(OptionData data)
    {
        DraggableBubble bubble = Instantiate(optionBubblePrefab, optionBubblePanel.transform).GetComponent<DraggableBubble>();
        bubble.SetUp(data);
        bubble.TweenIn();
    }
}