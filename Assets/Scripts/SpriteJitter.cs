using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class SpriteJitter : MonoBehaviour
{
    [SerializeField] private Texture2D texture;

    private Image _image;
    private List<Sprite> _sprites;

    private void Start()
    {
        _image = GetComponent<Image>();

        GetSprites();
        
        // Only animate if playing
        if (Application.isPlaying)
        {
            StartJitter();
        }
    }

    private void GetSprites()
    {
        // Warn if no texture is assigned
        if (texture == null)
        {
            Debug.LogWarning("SpriteShifter: No Texture2D assigned.");
            _sprites = new List<Sprite>();
            return;
        }

        // Get sprite list
        _sprites = SpriteUtility.GetSpritesFromTexture(texture);

        // Warn if nothing was sliced
        if (_sprites == null || _sprites.Count == 0)
        {
            Debug.LogWarning($"SpriteShifter: Texture '{texture.name}' contains no sliced sprites.");
            _sprites = new List<Sprite>();
            return;
        }
        
        // Saves time of having to set it ourselves in the inspector
        _image.sprite = _sprites[0];
    }

    private void StartJitter()
    {
        // Not enough frames to animate
        if (_sprites == null || _sprites.Count <= 1)
        {
            Debug.LogWarning($"{texture?.name ?? "Texture"} does not contain enough sprites to jitter.");
            return;
        }
        
        // Begin Shifting
        StartCoroutine(Jitter());
    }

    private IEnumerator Jitter()
    {
        int index = 1;
        bool countUp = true;

        while (true)
        {
            UpdateCountDirection(ref countUp, index);
            
            // Delay
            float delay = 1 / Mathf.Max(1, UIManager.Instance.Settings.FrameRate);
            yield return new WaitForSeconds(delay);

            // Just in case sprites list gets cleared externally
            if (_sprites == null || _sprites.Count == 0)
            {
                Debug.LogWarning("SpriteShifter: Sprite list became empty during animation.");
                yield break;
            }

            // Set sprite
            _image.sprite = _sprites[index];

            // Progress
            if (countUp) index++;
            else index--;
        }
    }

    private void UpdateCountDirection(ref bool countUp, int index)
    {
        if (index + 1 == _sprites.Count)
            countUp = false;
        else if (index == 0)
            countUp = true;
    }
}
