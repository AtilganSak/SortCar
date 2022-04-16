using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine;

public class OpenAndStartScenesOnEditor
{
    //START SCENES
    [MenuItem("Tools/Start PreGameScene #h", priority = 1)]
    public static void StartGameScene() => StartGame("GameScene");

    //OPEN SCENES
    [MenuItem("Tools/Open GameScene #1", priority = 1)]
    public static void OpenGameScene() => OpenScene("GameScene");

    //FUNCTIONS
    private static bool OpenScene(string scene)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + scene + ".unity");
            return true;
        }
        else
            return false;
    }
    public static void StartGame(string name)
    {
        if (OpenScene(name))
            EditorApplication.isPlaying = true;
    }
}
