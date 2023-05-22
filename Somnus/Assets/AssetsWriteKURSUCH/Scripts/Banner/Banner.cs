using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Banner : VisualElement
{
    public Button button;

    public Banner(int width, int height, Sprite sprite)
    {
        style.width = width;
        style.height = height;

        width -= 22;
        height -= 67;

        VisualElement topLeftCorner = new VisualElement();
        topLeftCorner.AddToClassList("topCorners");
        topLeftCorner.AddToClassList("topLeftCorner");
        Add(topLeftCorner);

        VisualElement topPanel = new VisualElement();
        topPanel.AddToClassList("topPanel");
        topPanel.style.width = width;

        VisualElement buttonHolder = new VisualElement();
        buttonHolder.style.left = width - 117;
        buttonHolder.AddToClassList("buttonHolder");

        VisualElement button1 = new VisualElement();
        button1.AddToClassList("button");
        button1.AddToClassList("button1");
        buttonHolder.Add(button1);

        VisualElement button2 = new VisualElement();
        button2.AddToClassList("button");
        button2.AddToClassList("button2");
        buttonHolder.Add(button2);

        VisualElement button3 = new VisualElement();
        button3.AddToClassList("button");
        button3.AddToClassList("button3");
        buttonHolder.Add(button3);

        topPanel.Add(buttonHolder);

        Add(topPanel);

        VisualElement topRightCorner = new VisualElement();
        topRightCorner.AddToClassList("topCorners");
        topRightCorner.AddToClassList("topRightCorner");
        Add(topRightCorner);

        VisualElement leftPanel = new VisualElement();
        leftPanel.AddToClassList("verticalPanel");
        leftPanel.AddToClassList("leftPanel");
        leftPanel.style.height = height;
        Add(leftPanel);

        VisualElement Image = new VisualElement();
        Image.style.backgroundImage = sprite.texture;
        Image.style.width = width;
        Image.style.height = height;
        Add(Image);

        VisualElement rightPanel = new VisualElement();
        rightPanel.AddToClassList("verticalPanel");
        rightPanel.AddToClassList("rightPanel");
        rightPanel.style.height = height;
        Add(rightPanel);

        VisualElement bottomLeftCorner = new VisualElement();
        bottomLeftCorner.AddToClassList("bottomCorners");
        bottomLeftCorner.AddToClassList("bottomLeftCorner");
        Add(bottomLeftCorner);

        VisualElement bottomPanel = new VisualElement();
        bottomPanel.AddToClassList("bottomPanel");
        bottomPanel.style.width = width;
        Add(bottomPanel);

        VisualElement bottomRightCorner = new VisualElement();
        bottomRightCorner.AddToClassList("bottomCorners");
        bottomRightCorner.AddToClassList("bottomRightCorner");
        Add(bottomRightCorner);

        button = new Button();
        button.style.left = width - 22;
        button.style.top = 12;
        button.AddToClassList("button");
        button.AddToClassList("exitButton");
        Add(button);

        AddToClassList("banner");
    }
}