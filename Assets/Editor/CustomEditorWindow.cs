using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow {

    GameObject Wheel;
    int myInt;
    GameObject narrator;
    GameObject gameController;
    [MenuItem("vr-software/CustomWindow")]
    private static void ShowWindow() {
        var window = GetWindow<CustomEditorWindow>();
        window.titleContent = new GUIContent("CustomWindow");
        window.Show();
    }
    private void OnGUI() {
        Wheel = GameObject.Find("Antennae Wheel");
        myInt = EditorGUILayout.IntField ("TextField", myInt);
        if (GUILayout.Button("Rotate Wheel")) {
            Wheel.GetComponent<RotateWheel>().RotateTo(myInt);
        }
        narrator = GameObject.Find("Narrator");
        if (GUILayout.Button("Play sound")) {
            narrator.GetComponent<NarratorController>().PlayStartingTalk();
        }
        gameController = GameObject.Find("GameController");
        if (GUILayout.Button("start game")) {
            gameController.GetComponent<WaveGameController>().startGame();
        }
        
    }
}
