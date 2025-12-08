using UnityEngine;

public class OptionData : ScriptableObject
{
    [Tooltip("The panel that will apear if this bubble is selected.")]
    public GameObject connectedPanel;
    public string text;
}