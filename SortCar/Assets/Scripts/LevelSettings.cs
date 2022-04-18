using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSettings : ScriptableObject
{
    public TeamColor[] teamColors;

    public float carDuration = 2;
    public Ease carAnimation = Ease.InOutSine;
    public float startPointDuration = 1;
    public bool mixOnStart;
    
    public float doorDuration = 0.3F;
    public Ease doorAnimation = Ease.Linear;

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
    public void Reset()
    {
        ResetTeamColors();
        carDuration = 2;
        carAnimation = Ease.InOutSine;
        startPointDuration = 1;
        doorDuration = 0.3F;
        doorAnimation = Ease.Linear;
    }
    public void ResetTeamColors()
    {
        Team[] teams = Enum.GetValues(typeof(Team)).Cast<Team>().ToArray();
        teamColors = new LevelSettings.TeamColor[teams.Length - 1];
        for (int i = 1; i <= teamColors.Length; i++)
        {
            if (teams[i] != Team.None)
                teamColors[i - 1] = new LevelSettings.TeamColor { team = teams[i], color = Color.white };
        }
    }
    [System.Serializable]
    public struct TeamColor
    {
        public Team team;
        public Color color;
    }
}
