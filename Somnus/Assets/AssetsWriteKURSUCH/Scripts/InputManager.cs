using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static string chars;
    private static string languageConvertString;

    public static void InputSet(string _chars)
    {
        chars = _chars;
    }

    public static void InputUpdate()
    {
        if (!Input.anyKeyDown || Input.inputString.Length == 0) return;

        char inputStr = LanguageConvert(Input.inputString[0]);
        if (!char.IsWhiteSpace(inputStr))
        {
            foreach(char sym in chars)
            {
                if(inputStr == sym)
                {
                    GameSessionManager.main.InputEnter();
                    return;
                }
            }
            GameSessionManager.main.InputError();
        }
    }

    public static void SetLanguageConvert(string _languageConvertString)
    {
        languageConvertString = _languageConvertString;
    }

    private static char LanguageConvert(char c)
    {
        string englishChars = "qwertyuiop[]asdfghjkl;'zxcvbnm,./";
        string russianChars = languageConvertString;

        for (int i = 0; i < englishChars.Length; i++)
        {
            if (c == englishChars[i]) return russianChars[i];
        }

        return c;
    }
}
