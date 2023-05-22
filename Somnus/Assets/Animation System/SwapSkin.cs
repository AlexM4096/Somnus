using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapSkin : MonoBehaviour
{
    static public SwapSkin Instance;

    [SerializeField] private List<Sprite> Skins;
    private int SkinIndex = 0;
    private SpriteRenderer Renderer;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        SetSkin();
        Instance = this;
    }

    public void Swap()
    {
        SkinIndex++;
        SkinIndex %= Skins.Count;

        SetSkin();
    }

    private void SetSkin()
    {
        Renderer.sprite = Skins[SkinIndex];
    }
}
