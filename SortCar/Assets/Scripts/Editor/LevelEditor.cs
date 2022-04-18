using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class LevelEditor : EditorWindow 
{
    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        window.Show();
    }

    GUIStyle header;

    LevelSettings.TeamColor[] teamColors;

    LevelSettings levelSettings;

    private void OnEnable()
    {
        header = new GUIStyle();
        header.alignment = TextAnchor.MiddleLeft;
        header.fontStyle = FontStyle.Bold;
        header.normal.textColor = Color.white;

        levelSettings = Resources.Load<LevelSettings>("LevelSettings");
        if (levelSettings != null)
        {
            if (levelSettings.teamColors == null || levelSettings.teamColors.Length == 0)
            {
                Team[] teams = Enum.GetValues(typeof(Team)).Cast<Team>().ToArray();
                levelSettings.teamColors = new LevelSettings.TeamColor[teams.Length - 1];
                for (int i = 1; i <= levelSettings.teamColors.Length; i++)
                {
                    if(teams[i] != Team.None)
                        levelSettings.teamColors[i - 1] = new LevelSettings.TeamColor { team = teams[i], color = Color.white };
                }
            }
            teamColors = levelSettings.teamColors;
        }
    }

    void OnGUI()
    {
        DrawTeamSettings();
    }
    private void DrawTeamSettings()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Teams", header);
        float cLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 50;
        for (int i = 0; i < teamColors.Length; i++)
        {
            if (teamColors[i].team != Team.None)
            {
                EditorGUILayout.BeginHorizontal();
                teamColors[i].color = EditorGUILayout.ColorField(teamColors[i].team.ToString(), teamColors[i].color);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUIUtility.labelWidth = cLabelWidth;
        EditorGUILayout.EndVertical();
        if (EditorGUI.EndChangeCheck())
        {
            ChangedTeamSettings();
        }
    }
    private void ChangedTeamSettings()
    {
        if (levelSettings != null)
        {
            levelSettings.SetTeamColors(teamColors);

            EditorUtility.SetDirty(levelSettings);
            AssetDatabase.SaveAssetIfDirty(levelSettings);
        }
    }
}
