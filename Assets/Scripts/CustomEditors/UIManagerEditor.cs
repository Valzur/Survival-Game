using UnityEngine;
using UnityEditor;

namespace  Valzuroid.SurvivalGame.CustomEditors
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        SerializedProperty healthBar;
        SerializedProperty xpBar;
        SerializedProperty levelText;
        bool playerPropFold = false;

        SerializedProperty buildingsRoot;
        SerializedProperty buildingsParent;
        SerializedProperty buildingPanelItemPrefab;
        bool buildingPropFold;

        SerializedProperty coinsText;
        SerializedProperty workersText;
        bool otherPropFold;

        SerializedProperty cycleText;
        SerializedProperty timeText;
        bool dayNightCyclePropFold;

        void OnEnable()
        {
            healthBar = serializedObject.FindProperty("healthBar");
            xpBar = serializedObject.FindProperty("xpBar");
            levelText = serializedObject.FindProperty("levelText");
            buildingsRoot = serializedObject.FindProperty("buildingsRoot");
            buildingsParent = serializedObject.FindProperty("buildingsParent");
            coinsText = serializedObject.FindProperty("coinsText");
            buildingPanelItemPrefab = serializedObject.FindProperty("buildingPanelItemPrefab");
            cycleText = serializedObject.FindProperty("cycleText");
            timeText = serializedObject.FindProperty("timeText");
            workersText = serializedObject.FindProperty("workersText");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            playerPropFold = EditorGUILayout.BeginFoldoutHeaderGroup(playerPropFold, "Player Properties");
            if(playerPropFold)
            {
                EditorGUILayout.PropertyField(healthBar);
                EditorGUILayout.PropertyField(xpBar);
                EditorGUILayout.PropertyField(levelText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            buildingPropFold = EditorGUILayout.BeginFoldoutHeaderGroup(buildingPropFold, "Building References");
            if(buildingPropFold)
            {
                EditorGUILayout.PropertyField(buildingsRoot);
                EditorGUILayout.PropertyField(buildingsParent);
                EditorGUILayout.PropertyField(buildingPanelItemPrefab);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            dayNightCyclePropFold = EditorGUILayout.BeginFoldoutHeaderGroup(dayNightCyclePropFold, "Day/Night Cycle References");
            if(dayNightCyclePropFold)
            {
                EditorGUILayout.PropertyField(cycleText);
                EditorGUILayout.PropertyField(timeText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            otherPropFold = EditorGUILayout.BeginFoldoutHeaderGroup(otherPropFold, "Other References");
            if(otherPropFold)
            {
                EditorGUILayout.PropertyField(coinsText);
                EditorGUILayout.PropertyField(workersText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
}