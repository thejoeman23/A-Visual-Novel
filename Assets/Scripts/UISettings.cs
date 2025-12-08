using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "Scriptable Objects/UISettings")]
public class UISettings : ScriptableObject
{
    [Header("Frame Rate")] 
    public float FrameRate;
    
    [Space]
    
    [Header("Line Settings")]
    public float LineWidth;
    
    [Space]
    
    [Header("Draggable Bubble Tween Settings")]
    public float tweenInTime;
    public Ease tweenInEase;
    
    public float tweenOutTime;
    public Ease tweenOutEase;
}
