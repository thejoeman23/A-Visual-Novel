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
    
    [Space]
    
    public float tweenOutTime;
    public Ease tweenOutEase;
    
    [Space]

    public float followLerpSpeed = 15;

    [Space] 
    
    public Ease squeezeEase;
    public float squeezeSpeed;
    [Range(0, 100)] public float squeezePercent;
}
