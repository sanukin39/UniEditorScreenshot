using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

public class CaptureWindow : EditorWindow{

    private string saveFileName = string.Empty;
    private string saveDirPath = string.Empty;

    [MenuItem("Window/Capture Editor")]
    private static void Capture() {
        EditorWindow.GetWindow (typeof(CaptureWindow)).Show ();
    }

    void OnGUI() {
        EditorGUILayout.LabelField ("OUTPUT FOLDER PATH:");
        EditorGUILayout.LabelField (saveDirPath + "/");

        if (string.IsNullOrEmpty (saveDirPath)) {
            saveDirPath = Application.dataPath;
        }

        if (GUILayout.Button("Select output directory")) {
            string path = EditorUtility.OpenFolderPanel("select directory", saveDirPath, Application.dataPath);
            if (!string.IsNullOrEmpty(path)) {
                saveDirPath = path;
            }
        }

        if (GUILayout.Button("Open output directory")) {
            System.Diagnostics.Process.Start (saveDirPath);
        }

        // insert blank line
        GUILayout.Label ("");

        if (GUILayout.Button("Take screenshot")) {
            var outputPath = saveDirPath + "/" + DateTime.Now.ToString ("yyyyMMddHHmmss") + ".png";
            ScreenCapture.CaptureScreenshot (outputPath);
            Debug.Log ("Export scrennshot at " + outputPath);
        }
    }
}
