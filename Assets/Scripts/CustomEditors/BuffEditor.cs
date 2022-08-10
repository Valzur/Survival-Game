using UnityEditor;
using UnityEngine;

namespace Valzuroid.SurvivalGame.CustomEditors
{
    [CustomEditor(typeof(Buff))]
    public class BuffEditor : Editor
    {
        #region Serialized References
        SerializedProperty baseEffectRange;
        Category category;
        //Tower stuff
        SerializedProperty towerRangeMultiplier;
        SerializedProperty towerRangeFlat;
        SerializedProperty towerDamageMultiplier;
        SerializedProperty towerDamageFlat;
        SerializedProperty towerHealthMultiplier;
        SerializedProperty towerHealthFlat;

        //Player
        SerializedProperty playerRangeMultiplier;
        SerializedProperty playerRangeFlat;
        SerializedProperty playerDamageMultiplier;
        SerializedProperty playerDamageFlat;
        SerializedProperty playerHealthMultiplier;
        SerializedProperty playerHealthFlat;
        SerializedProperty playerSpeedMultiplier;
        SerializedProperty playerSpeedFlat;
        SerializedProperty playerXPMultiplier;

        //Enemy
        SerializedProperty enemyRangeMultiplier;
        SerializedProperty enemyRangeFlat;
        SerializedProperty enemyDamageMultiplier;
        SerializedProperty enemyDamageFlat;
        SerializedProperty enemyHealthMultiplier;
        SerializedProperty enemyHealthFlat;
        SerializedProperty enemySpeedMultiplier;
        SerializedProperty enemySpeedFlat;
        #endregion
        void OnEnable()
        {
            baseEffectRange = serializedObject.FindProperty("baseEffectRange");
            //Tower stuff
            towerRangeMultiplier = serializedObject.FindProperty("towerRangeMultiplier");
            towerRangeFlat = serializedObject.FindProperty("towerRangeFlat");
            towerDamageMultiplier = serializedObject.FindProperty("towerDamageMultiplier");
            towerDamageFlat = serializedObject.FindProperty("towerDamageFlat");
            towerHealthMultiplier = serializedObject.FindProperty("towerHealthMultiplier");
            towerHealthFlat = serializedObject.FindProperty("towerHealthFlat");

            //Player
            playerRangeMultiplier = serializedObject.FindProperty("playerRangeMultiplier");
            playerRangeFlat = serializedObject.FindProperty("playerRangeFlat");
            playerDamageMultiplier = serializedObject.FindProperty("playerDamageMultiplier");
            playerDamageFlat = serializedObject.FindProperty("playerDamageFlat");
            playerHealthMultiplier = serializedObject.FindProperty("playerHealthMultiplier");
            playerHealthFlat = serializedObject.FindProperty("playerHealthFlat");
            playerSpeedMultiplier = serializedObject.FindProperty("playerSpeedMultiplier");
            playerSpeedFlat = serializedObject.FindProperty("playerSpeedFlat");
            playerXPMultiplier = serializedObject.FindProperty("playerXPMultiplier");

            //Enemy
            enemyRangeMultiplier = serializedObject.FindProperty("enemyRangeMultiplier");
            enemyRangeFlat = serializedObject.FindProperty("enemyRangeFlat");
            enemyDamageMultiplier = serializedObject.FindProperty("enemyDamageMultiplier");
            enemyDamageFlat = serializedObject.FindProperty("enemyDamageFlat");
            enemyHealthMultiplier = serializedObject.FindProperty("enemyHealthMultiplier");
            enemyHealthFlat = serializedObject.FindProperty("enemyHealthFlat");
            enemySpeedMultiplier = serializedObject.FindProperty("enemySpeedMultiplier");
            enemySpeedFlat = serializedObject.FindProperty("enemySpeedFlat");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(baseEffectRange);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Effects");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            category = (Category)EditorGUILayout.EnumPopup(category);

            if( category == Category.Tower)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(towerRangeMultiplier);
                EditorGUILayout.PropertyField(towerRangeFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(towerDamageMultiplier);
                EditorGUILayout.PropertyField(towerDamageFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(towerHealthMultiplier);
                EditorGUILayout.PropertyField(towerHealthFlat);
                EditorGUILayout.EndHorizontal();
            }

            if( category == Category.Player)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(playerRangeMultiplier);
                EditorGUILayout.PropertyField(playerRangeFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(playerDamageMultiplier);
                EditorGUILayout.PropertyField(playerDamageFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(playerHealthMultiplier);
                EditorGUILayout.PropertyField(playerHealthFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(playerSpeedMultiplier);
                EditorGUILayout.PropertyField(playerSpeedFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(playerXPMultiplier);
            }

            if( category == Category.Enemy)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(enemyRangeMultiplier);
                EditorGUILayout.PropertyField(enemyRangeFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(enemyDamageMultiplier);
                EditorGUILayout.PropertyField(enemyDamageFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(enemyHealthMultiplier);
                EditorGUILayout.PropertyField(enemyHealthFlat);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(enemySpeedMultiplier);
                EditorGUILayout.PropertyField(enemySpeedFlat);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}