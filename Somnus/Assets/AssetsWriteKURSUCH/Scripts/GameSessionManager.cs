using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager main;
    public Level currentLevel;
    public TextInputs inputs;
    private int currentInput;

    public Color unprintedTextColor;
    public int unprintedTextCount = 7;
    [HideInInspector]public int currentTextIndex = 0;

    private int currentCombo = 0;

    private float gameSessionProgress
    {
        get
        {
            return currentTextIndex / currentLevel.levelText.Length;
        }
    }

    private bool loadingbar;
    private float loadingBarTimer;
    private float loadingBarCurrentTime;

    private float timer = 0;
    private bool timerStop = false;

    public int maxBannerCounts = 15;

    private void Awake()
    {
        main = this;
        InputManager.SetLanguageConvert(inputs.languageConvert);
    }

    private void Start()
    {
        UserInterfaceManager.main.wordText = "<color=#" + ConvertColorToHexadecimal(unprintedTextColor) + ">";
        UserInterfaceManager.main.wordText = UnprintedTextAdd(UserInterfaceManager.main.wordText);

        UserInterfaceManager.main.CreateStatusBanner();

        StartCoroutine(SpawnBanners());

        NewRule();
    }

    private void Update()
    {
        if (!timerStop)
        {
            InputManager.InputUpdate();
            timer += Time.deltaTime;

            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            UserInterfaceManager.main.statusText = currentTextIndex.ToString() + "/" + currentLevel.levelText.Length.ToString() + "\n" + timeSpan.ToString(@"m\:ss");

            if (loadingbar) LoadingBarUpdate();
        }
    }

    void LoadingBarUpdate()
    {
        loadingBarCurrentTime -= Time.deltaTime;
        if(loadingBarCurrentTime <= 0)
        {
            loadingbar = false;
            NewRule();
            return;
        }
        UserInterfaceManager.main.loadingBarValue = loadingBarCurrentTime * 100 / loadingBarTimer;
    }

    public void InputEnter()
    {
        SoundManager.main.klawa.Play();

        if(currentTextIndex + unprintedTextCount + 1 >= currentLevel.levelText.Length)
        {
            Win();
            return;
        }

        UserInterfaceManager.main.wordText = UserInterfaceManager.main.wordText.Remove(UserInterfaceManager.main.wordText.Length - 15 - unprintedTextCount);
        UserInterfaceManager.main.wordText = UnprintedTextAdd(UserInterfaceManager.main.wordText);
        UserInterfaceManager.main.wordText += "<color=#" + ConvertColorToHexadecimal(unprintedTextColor) + ">";
        currentTextIndex += unprintedTextCount;
        UserInterfaceManager.main.wordText = UnprintedTextAdd(UserInterfaceManager.main.wordText);

        if (loadingbar) return;

        if(currentCombo == 1)
        {
            NewRule();
            return;
        }

        currentCombo--;
        UserInterfaceManager.main.ruleText = inputs.strings[currentInput].text + " (x" + currentCombo + ")";
    }

    public void InputError()
    {
        SoundManager.main.error.Play();

        if (currentTextIndex < unprintedTextCount) return;
        UserInterfaceManager.main.wordText = UserInterfaceManager.main.wordText.Remove(UserInterfaceManager.main.wordText.Length - 15 - unprintedTextCount * 2);
        UserInterfaceManager.main.wordText += "<color=#" + ConvertColorToHexadecimal(unprintedTextColor) + ">";
        currentTextIndex -= unprintedTextCount;
        UserInterfaceManager.main.wordText = UnprintedTextAdd(UserInterfaceManager.main.wordText);

        NewRule();
    }

    private void Win()
    {
        SoundManager.main.win.Play();

        UserInterfaceManager.main.wordText = currentLevel.levelText;

        timerStop = true;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        UserInterfaceManager.main.CreateRuleBanner("Результат: " + timeSpan.ToString(@"m\:ss"));
        UITransitionChannel.TurnOn("Office1");
        Debug.Log("Win!");
    }

    private void NewRule()
    {
        currentInput = UnityEngine.Random.Range(0, inputs.strings.Length);
        InputManager.InputSet(inputs.strings[currentInput].chars);
        string ruleText = inputs.strings[currentInput].text;

        bool isLoadingBar = IsLoadingBar();

        if (isLoadingBar)
        {
            loadingbar = true;
            loadingBarTimer = UnityEngine.Random.Range(currentLevel.difficulty.minLoadingBarTimeRange.Evaluate(gameSessionProgress), currentLevel.difficulty.maxLoadingBarTimeRange.Evaluate(gameSessionProgress));
            loadingBarCurrentTime = loadingBarTimer;

            UserInterfaceManager.main.CreateRuleBanner(ruleText, true);
            return;
        }

        loadingbar = false;
        currentCombo = UnityEngine.Random.Range(Mathf.RoundToInt(currentLevel.difficulty.minComboRange.Evaluate(gameSessionProgress)), Mathf.RoundToInt(currentLevel.difficulty.maxComboRange.Evaluate(gameSessionProgress)));

        UserInterfaceManager.main.CreateRuleBanner(ruleText + " (x" + currentCombo + ")");
    }

    private bool IsLoadingBar()
    {
        int rnd = UnityEngine.Random.Range(0, 100);
        if (rnd > currentLevel.difficulty.loadingBarChance.Evaluate(gameSessionProgress) * 100) return false;
        return true;
    }

    private string UnprintedTextAdd(string _text)
    {        
        for (int i = 0; i < unprintedTextCount; i++)
        {
            int ind = i + currentTextIndex;
            if (ind >= currentLevel.levelText.Length) break;
            _text += currentLevel.levelText[ind];
        }
        return _text;
    }

    IEnumerator SpawnBanners()
    {
        while (!timerStop)
        {
            if(UserInterfaceManager.main.totalBannerCounts < maxBannerCounts) StartCoroutine(SpawnBannersCount(UnityEngine.Random.Range(Mathf.RoundToInt(currentLevel.difficulty.minBannerCountSpawn.Evaluate(gameSessionProgress)), Mathf.RoundToInt(currentLevel.difficulty.maxBannerCountSpawn.Evaluate(gameSessionProgress)))));
            yield return new WaitForSeconds(UnityEngine.Random.Range(currentLevel.difficulty.minBannerSpeedSpawn.Evaluate(gameSessionProgress), currentLevel.difficulty.maxBannerSpeedSpawn.Evaluate(gameSessionProgress)));
        }
    }

    IEnumerator SpawnBannersCount(int count)
    {
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            UserInterfaceManager.main.CreateBanner();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private string ConvertColorToHexadecimal(Color color)
    {
        string hexadecimal = "";

        hexadecimal += System.Convert.ToString(Mathf.RoundToInt(color.r * 255), 16);
        hexadecimal += System.Convert.ToString(Mathf.RoundToInt(color.g * 255), 16);
        hexadecimal += System.Convert.ToString(Mathf.RoundToInt(color.b * 255), 16);

        return hexadecimal;
    }
}
