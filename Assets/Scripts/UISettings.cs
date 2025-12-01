using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "Scriptable Objects/UISettings")]
public class UISettings : ScriptableObject
{
    [Header("Frame Rate")] 
    public float FrameRate;
    
    [Header("Line Settings")]
    public float LineWidth;
}
