using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager main;

    private Label _wordText;
    public string wordText
    {
        get
        {
            return _wordText.text;
        }
        set
        {
            _wordText.text = value;
        }
    }

    public UIDocument word;

    [SerializeField] private float2[] ruleBannerPosistion;
    [SerializeField] private float2 statusBannerPosistion;
    [SerializeField] private BannerList[] bannerPosistion;

    [HideInInspector] public int totalBannerCounts = 0;

    private RuleBanner currentRuleBanner;
    public float loadingBarValue
    {
        set
        {
            if (currentRuleBanner == null) return;
            if (!currentRuleBanner.loadingBar) return;
            currentRuleBanner.loadingBarValue = value;
        }
    }
    public string ruleText
    {
        set
        {
            if (currentRuleBanner == null) return;
            currentRuleBanner.ruleText = value;
        }
    }

    private RuleBanner _statusBanner;
    public string statusText
    {
        set
        {
            _statusBanner.ruleText = value;
        }
    }

    private void Awake()
    {
        main = this;
        _wordText = word.rootVisualElement.Q<Label>("TextArea");
    }

    public void CreateBanner()
    {
        SoundManager.main.error.Play();
        int i;
        do
        {
            i = UnityEngine.Random.Range(0, bannerPosistion.Length);
        } while (bannerPosistion[i].banners.Count == bannerPosistion[i].maxBanners);

        Banner newBanner = BannerSpawner.main.CreateBanner(bannerPosistion[i].position + bannerPosistion[i].nextPosition * bannerPosistion[i].banners.Count);
        bannerPosistion[i].banners.Add(newBanner);

        newBanner.button.clicked += () => RemoveBannerFromList(newBanner, bannerPosistion[i]);
        totalBannerCounts++;
    }

    public void RemoveBannerFromList(Banner banner, BannerList bannerList)
    {
        totalBannerCounts--;
        bannerList.banners.Remove(banner);
    }

    public void RemoveRuleBanner()
    {
        if (currentRuleBanner != null) BannerSpawner.main.RemoveRuleBanner(currentRuleBanner);
    }

    public void CreateRuleBanner(string text)
    {
        SoundManager.main.rule.Play();

        if (currentRuleBanner != null) BannerSpawner.main.RemoveRuleBanner(currentRuleBanner);

        currentRuleBanner = BannerSpawner.main.CreateRuleBanner(ruleBannerPosistion[UnityEngine.Random.Range(0, ruleBannerPosistion.Length)], text, false);
    }

    public void CreateRuleBanner(string text, bool _loadingBar)
    {
        SoundManager.main.rule.Play();

        if (currentRuleBanner != null) BannerSpawner.main.RemoveRuleBanner(currentRuleBanner);

        currentRuleBanner = BannerSpawner.main.CreateRuleBanner(ruleBannerPosistion[UnityEngine.Random.Range(0, ruleBannerPosistion.Length)], text, _loadingBar);
    }

    public void CreateStatusBanner()
    {
        _statusBanner = BannerSpawner.main.CreateStatusBanner(statusBannerPosistion, "");
    }

    public void RemoveStatusBanner()
    {
        BannerSpawner.main.RemoveRuleBanner(_statusBanner);
    }
}

[System.Serializable]
public class BannerList
{
    public float2 position;
    public float2 nextPosition;
    [HideInInspector]public List<Banner> banners = new List<Banner>();
    public int maxBanners;
}