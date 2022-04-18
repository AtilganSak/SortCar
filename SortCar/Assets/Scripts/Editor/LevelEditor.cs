using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using DG.Tweening;
using UnityEditor.Presets;

public class LevelEditor : EditorWindow 
{
    GUIStyle header;

    LevelSettings _levelSettings;
    LevelSettings levelSettings { get
        {
            if (_levelSettings == null)
                _levelSettings = Resources.Load<LevelSettings>("LevelSettings");
            return _levelSettings;
        } }

    private void OnLostFocus()
    {
        SaveSettings();
    }
    private void OnDisable()
    {
        SaveSettings();
    }
    private void OnEnable()
    {
        header = new GUIStyle();
        header.alignment = TextAnchor.MiddleLeft;
        header.fontStyle = FontStyle.Bold;
        header.normal.textColor = Color.white;

        if (levelSettings.teamColors == null || levelSettings.teamColors.Length == 0)
        {
            levelSettings.ResetTeamColors();
        }
    }

    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        window.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        DrawTeamSettings();
        DrawCarSettings();
        DrawDoorSettings();
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Reset"))
        {
            if (EditorUtility.DisplayDialog("", "Are you sure to reset!", "Yes", "No"))
            {
                ResetSettings();
            }
        }
        GUI.enabled = false;
        if (GUILayout.Button(new GUIContent("Save Preset", "Add-on feature")))
        {
            //Save current setting as a preset.
        }
        if (GUILayout.Button(new GUIContent("Load Preset","Add-on feature")))
        {
            //Load a preset saved before.
        }
        GUI.enabled = true;
        EditorGUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
    private void DrawTeamSettings()
    {
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(150));
        EditorGUILayout.LabelField("Teams", header);
        float cLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 50;
        for (int i = 0; i < levelSettings.teamColors.Length; i++)
        {
            if (levelSettings.teamColors[i].team != Team.None)
            {
                EditorGUILayout.BeginHorizontal();
                levelSettings.teamColors[i].color = EditorGUILayout.ColorField(levelSettings.teamColors[i].team.ToString(), levelSettings.teamColors[i].color);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUIUtility.labelWidth = cLabelWidth;
        EditorGUILayout.EndVertical();
    }
    private void DrawCarSettings()
    {
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(150));
        EditorGUILayout.LabelField("Cars", header);
        float cLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.BeginHorizontal();
        levelSettings.carDuration = EditorGUILayout.FloatField("Duration", levelSettings.carDuration);
        if (levelSettings.carDuration < 0)
            levelSettings.carDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        levelSettings.carAnimation = (Ease)EditorGUILayout.EnumPopup("Animation", levelSettings.carAnimation);
        if (levelSettings.doorDuration < 0)
            levelSettings.doorDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        levelSettings.startPointDuration = EditorGUILayout.FloatField("Start Duration", levelSettings.startPointDuration);
        if (levelSettings.doorDuration < 0)
            levelSettings.doorDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        levelSettings.mixOnStart = EditorGUILayout.Toggle("Mix On Start", levelSettings.mixOnStart);
        if (levelSettings.doorDuration < 0)
            levelSettings.doorDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = cLabelWidth;
        EditorGUILayout.EndVertical();
    }
    private void DrawDoorSettings()
    {
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(150));
        EditorGUILayout.LabelField("Door", header);
        float cLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 100;
        EditorGUILayout.BeginHorizontal();
        levelSettings.doorDuration = EditorGUILayout.FloatField("Duration", levelSettings.doorDuration);
        if (levelSettings.carDuration < 0)
            levelSettings.carDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        levelSettings.doorAnimation = (Ease)EditorGUILayout.EnumPopup("Animation", levelSettings.doorAnimation);
        if (levelSettings.doorDuration < 0)
            levelSettings.doorDuration = 0;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = cLabelWidth;
        EditorGUILayout.EndVertical();
    }
    private void SaveSettings()
    {
        if (levelSettings != null)
        {
            EditorUtility.SetDirty(levelSettings);
            AssetDatabase.SaveAssetIfDirty(levelSettings);
        }
    }
    private void ResetSettings()
    {
        _levelSettings.Reset();
    }
}
