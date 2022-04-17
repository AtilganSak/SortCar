using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Color GetColorByTeam(Team team)
    {
        switch (team)
        {
            case Team.Purple:
                return HexToColor("B149EA");
            case Team.Yellow:
                return HexToColor("F1CB21");
            default:
                return Color.white;
        }
    }
    public static Color HexToColor(string hexValue)
    {
        Color newCol = Color.white;
        ColorUtility.TryParseHtmlString("#"+hexValue, out newCol);
        return newCol;
    }
}
