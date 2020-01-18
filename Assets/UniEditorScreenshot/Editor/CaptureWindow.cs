using UnityEngine;
using UnityEditor;
using System;

public class CaptureWindow : EditorWindow
{
    private string saveFileName = string.Empty;
    private string saveDirPath = string.Empty;

    [MenuItem("Window/Screenshot Capture")]
    private static void Capture()
    {
        EditorWindow.GetWindow(typeof(CaptureWindow)).Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Output Folder Path : ");
        EditorGUILayout.LabelField(saveDirPath + "/");

        if (string.IsNullOrEmpty(saveDirPath))
        {
            saveDirPath = Application.dataPath + "/..";
        }

        if (GUILayout.Button("Select output directory"))
        {
            string path = EditorUtility.OpenFolderPanel("select directory", saveDirPath, Application.dataPath);
            if (!string.IsNullOrEmpty(path))
            {
                saveDirPath = path;
            }
        }

        if (GUILayout.Button("Open output directory"))
        {
            System.Diagnostics.Process.Start(saveDirPath);
        }

        // insert blank line
        GUILayout.Label("");

        if (GUILayout.Button("Take screenshot"))
        {
            var resolution = GetMainGameViewSize();
            int x = (int)resolution.x;
            int y = (int)resolution.y;
            var outputPath = saveDirPath + "/" + DateTime.Now.ToString($"{x}x{y}_yyyy_MM_dd_HH_mm_ss") + ".png";
            ScreenCapture.CaptureScreenshot(outputPath);
            Debug.Log("Export scrennshot at " + outputPath);
        }
    }

    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }
}