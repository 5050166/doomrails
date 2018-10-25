using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{

    public static string GetNewIndex(int index)
    {
        char c = (char)(97 + index / 26);
        char c2 = (char)(97 + index % 26);
        return c.ToString() + c2.ToString();
    }

    public static string ToShortString(this double value)
    {
        if (value < 1.0)
        {
            return "0";
        }
        string text = value.ToString("#");
        if (text == "Infinity")
        {
            return "Inf";
        }
        int length = text.Length;
        if (text.Length > 3)
        {
            string text2 = string.Empty;
            text2 = text.Substring(0, 3);
            int num = length % 3;
            if (num > 0)
            {
                text2 = text2.Insert(num, ".");
            }
            int num2 = (length - 1) / 3;
            string str = string.Empty;
            if (num2 < Extension. StringUnits.Length)
            {
                str = Extension. StringUnits[num2];
            }
            else
            {
                str = Extension.GetNewIndex(num2 - StringUnits.Length);
            }
            return text2 + str;
        }
        return text;
    }

    public static string[] StringUnits = new string[]
    {
        string.Empty,
        "k",
        "m",
        "b",
        "t",
        "q",
        "Q",
        "u",
        "U",
        "s",
        "S",
        "p",
        "P",
        "o",
        "O",
        "n",
        "N",
        "d",
        "D",
        "g",
        "G",
        "h",
        "H",
        "l",
        "L",
        "i",
        "I",
        "j",
        "J",
        "n",
        "N",
        "c",
        "C",
        "x",
        "X",
        "w",
        "W",
        "y",
        "Y",
        "z",
        "Z"
    };
}
