using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class SpriteUtility
{
    public static List<Sprite> GetSpritesFromTexture(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        return AssetDatabase.LoadAllAssetsAtPath(path)
            .OfType<Sprite>()
            .ToList();
    }
}

