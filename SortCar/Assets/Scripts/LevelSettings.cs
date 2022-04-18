using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSettings : ScriptableObject
{
    public TeamColor[] teamColors;

    public void SetTeamColors(TeamColor[] teamColors)
    {
        this.teamColors = teamColors;
    }
    public Color GetColorByTeam(Team team)
    {
        if (teamColors != null)
        {
            for (int i = 0; i < teamColors.Length; i++)
            {
                if (teamColors[i].team == team)
                {
                    return teamColors[i].color;
                }
            }
        }
        return Color.white;
    }
    [System.Serializable]
    public struct TeamColor
    {
        public Team team;
        public Color color;
    }
}
