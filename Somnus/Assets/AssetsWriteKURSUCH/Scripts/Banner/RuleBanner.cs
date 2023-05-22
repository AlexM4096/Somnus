using UnityEngine;
using UnityEngine.UIElements;

public class RuleBanner : VisualElement
{
    public Button button;

    private Label _RuleText;
    public string ruleText
    {
        get
        {
            return _RuleText.text;
        }
        set
        {
            _RuleText.text = value;
        }
    }

    private ProgressBar _LoadingBar;
    public float loadingBarValue
    {
        get
        {
            return _LoadingBar.value;
        }
        set
        {
            _LoadingBar.value = value;
        }
    }
    public bool loadingBar;

    public RuleBanner(int _width, int _height, string _text, bool _loadingBar)
    {
        style.width = _width;
        style.height = _height;

        _width -= 22;
        _height -= 67;

        VisualElement topLeftCorner = new VisualElement();
        topLeftCorner.AddToClassList("topCorners");
        topLeftCorner.AddToClassList("topLeftCorner");
        Add(topLeftCorner);

        VisualElement topPanel = new VisualElement();
        topPanel.AddToClassList("topPanel");
        topPanel.style.width = _width;

        loadingBar = _loadingBar;
        if (_loadingBar)
        {
            _LoadingBar = new ProgressBar();
            _LoadingBar.value = 100;
            _LoadingBar.style.width = _width - 10;
            _LoadingBar.AddToClassList("loadingBar");

            topPanel.Add(_LoadingBar);
        }

        Add(topPanel);

        VisualElement topRightCorner = new VisualElement();
        topRightCorner.AddToClassList("topCorners");
        topRightCorner.AddToClassList("topRightCorner");
        Add(topRightCorner);

        VisualElement leftPanel = new VisualElement();
        leftPanel.AddToClassList("verticalPanel");
        leftPanel.AddToClassList("leftPanel");
        leftPanel.style.height = _height;
        Add(leftPanel);

        VisualElement centerPanel = new VisualElement();
        centerPanel.AddToClassList("centerPanel");
        centerPanel.style.width = _width;
        centerPanel.style.height = _height;

        _RuleText = new Label();
        _RuleText.style.width = _width - 10;
        _RuleText.style.height = _height - 64;
        _RuleText.text = _text;
        centerPanel.Add(_RuleText);

        Add(centerPanel);

        VisualElement rightPanel = new VisualElement();
        rightPanel.AddToClassList("verticalPanel");
        rightPanel.AddToClassList("rightPanel");
        rightPanel.style.height = _height;
        Add(rightPanel);

        VisualElement bottomLeftCorner = new VisualElement();
        bottomLeftCorner.AddToClassList("bottomCorners");
        bottomLeftCorner.AddToClassList("bottomLeftCorner");
        Add(bottomLeftCorner);

        VisualElement bottomPanel = new VisualElement();
        bottomPanel.AddToClassList("bottomPanel");
        bottomPanel.style.width = _width;
        Add(bottomPanel);

        VisualElement bottomRightCorner = new VisualElement();
        bottomRightCorner.AddToClassList("bottomCorners");
        bottomRightCorner.AddToClassList("bottomRightCorner");
        Add(bottomRightCorner);

        AddToClassList("banner");
    }
}
