using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BannerSpawner : MonoBehaviour
{
    public static BannerSpawner main;

    public UIDocument RuleDocument;
    public UIDocument BannerDocument;

    int width;
    int height;

    BannerImage[] bannerImages;
    BannerImage bannerImage;

    //[SerializeField] bool randomButtonPlacement = false;

    private void Awake()
    {
        main = this;

        bannerImages = Resources.LoadAll<BannerImage>("Banner Images");
    }
    public Banner CreateBanner(float2 position)
    {
        bannerImage = bannerImages[Random.Range(0, bannerImages.Length)];

        width = 100 * bannerImage.aspectRatio.x;
        height = 100 * bannerImage.aspectRatio.y;

        Banner banner = new Banner(width, height, bannerImage.sprite);

        banner.style.left = position.x * Screen.width;
        banner.style.top = position.y * Screen.height;

        banner.button.clicked += () => RemoveBanner(banner);

        /*if (randomButtonPlacement)
        {
            banner.button.style.left = Random.Range(0, width);
            banner.button.style.top = Random.Range(0, height);
        }*/

        BannerDocument.rootVisualElement.Add(banner);

        return banner;
    }
    public RuleBanner CreateRuleBanner(float2 position, string _text, bool _loadingBar)
    {
        width = 100 * 5;
        height = 100 * 2;

        RuleBanner banner = new RuleBanner(width, height, _text, _loadingBar);

        banner.style.left = position.x * Screen.width;
        banner.style.top = position.y * Screen.height;

        RuleDocument.rootVisualElement.Add(banner);

        return banner;
    }

    public RuleBanner CreateStatusBanner(float2 position, string _text)
    {
        width = 100 * 3;
        height = 100 * 2;

        RuleBanner banner = new RuleBanner(width, height, _text, false);

        banner.style.left = position.x * Screen.width;
        banner.style.top = position.y * Screen.height;

        RuleDocument.rootVisualElement.Add(banner);

        return banner;
    }

    public void RemoveBanner(Banner banner)
    {
        BannerDocument.rootVisualElement.Remove(banner);
    }

    public void RemoveRuleBanner(RuleBanner banner)
    {
        RuleDocument.rootVisualElement.Remove(banner);
    }
}

