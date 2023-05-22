using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UITransitionScripts : MonoBehaviour
{
    [SerializeField] private float TransitionDuration;
    [SerializeField] private EasingMode Easing;
    private WaitForSeconds seconds; 
    private List<TimeValue> DurationValues;
    private StyleList<EasingFunction> EasingValues;
    private VisualElement Root;

    private void Awake()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;

        DurationValues = new List<TimeValue>() { new TimeValue(TransitionDuration, TimeUnit.Second) };
        EasingValues = new StyleList<EasingFunction>(new List<EasingFunction> { new EasingFunction(Easing) });

        seconds = new(TransitionDuration);

        Root.style.transitionDuration = DurationValues;
        Root.style.transitionTimingFunction = EasingValues;
    }

    private void Start()
    {
        TurnOff();
    }

    private void OnEnable()
    {
        UITransitionChannel.UITransitionOnEvent += TurnOn;
        UITransitionChannel.UITransitionOffEvent += TurnOff;
        UITransitionChannel.UITransitionBlinkEvent += Blink;
    }

    private void OnDisable()
    {
        UITransitionChannel.UITransitionOnEvent -= TurnOn;
        UITransitionChannel.UITransitionOffEvent -= TurnOff;
        UITransitionChannel.UITransitionBlinkEvent -= Blink;
    }

    private void TurnOn(string NextScene) { StartCoroutine(DisplayOn(NextScene)); }
    private void TurnOff() { StartCoroutine(DisplayOff()); }

    private void Blink() { StartCoroutine(Blinking()); }
        
    private IEnumerator DisplayOn(string NextScene)
    {
        yield return null;
        Root.style.opacity = 100;
        Root.style.display = DisplayStyle.Flex;
        SceneManager.LoadScene(NextScene);
    }

    private IEnumerator DisplayOff()
    {
        yield return null;
        Root.style.opacity = 0;
        Root.style.display = DisplayStyle.None;
    }

    private IEnumerator Blinking()
    {
        Root.style.opacity = 100;
        Root.style.display = DisplayStyle.Flex;
        yield return seconds;
        SwapSkin.Instance.Swap();
        Root.style.opacity = 0;
        Root.style.display = DisplayStyle.None;
    }
}
