using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnemyPatternData))]
[CanEditMultipleObjects]
public class EnemyPatternEditor : Editor {

	SerializedProperty statusEffect;
	SerializedProperty actionEffect;
	SerializedProperty patternType;
	SerializedProperty pattern;
	SerializedProperty alternativePattern;
	SerializedProperty enemyPattern;
	SerializedProperty enemyTurn;

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		enemyTurn = serializedObject.FindProperty ("enemyTurn");


		EnemyPatternData enemyPatternData = (EnemyPatternData)target;

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.LabelField ("Enemy Pattern System", EditorStyles.boldLabel, GUILayout.MaxWidth(150));
		enemyTurn.arraySize = EditorGUILayout.IntField (enemyTurn.arraySize, GUILayout.MaxWidth(40));
		if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (enemyTurn.arraySize, enemyTurn);
		if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (enemyTurn.arraySize-1, enemyTurn);

		EditorGUILayout.EndHorizontal ();

		for(int i = 0; i < enemyTurn.arraySize; i++)
		{
			GUILayout.Space (10);
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Turn:" + (i+1) + " Enemy Pattern", EditorStyles.boldLabel, GUILayout.MaxWidth(150));
			enemyPattern = enemyTurn.GetArrayElementAtIndex (i).FindPropertyRelative ("enemyPattern");
			enemyPattern.arraySize = EditorGUILayout.IntField (enemyPattern.arraySize, GUILayout.MaxWidth(40));
			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, enemyTurn);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, enemyTurn);
			EditorGUILayout.EndHorizontal ();

			for(int j = 0; j < enemyPattern.arraySize; j++)
			{
				GUILayout.Space (5);
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Use Alternative", EditorStyles.label, GUILayout.MaxWidth(100));
				EditorGUILayout.PropertyField (enemyPattern.GetArrayElementAtIndex(j).FindPropertyRelative ("UseAlternativePattern"), GUIContent.none, GUILayout.MaxWidth(20));
				bool UseAlternative = enemyPattern.GetArrayElementAtIndex (j).FindPropertyRelative ("UseAlternativePattern").boolValue;
				if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (j, enemyPattern);
				if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (j, enemyPattern);
				EditorGUILayout.EndHorizontal ();

				if (UseAlternative == false)
				{
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Main Pattern: " + (j+1), EditorStyles.label, GUILayout.MaxWidth(100));
					pattern = enemyPattern.GetArrayElementAtIndex (j).FindPropertyRelative ("pattern");
					EditorGUILayout.PropertyField (pattern.FindPropertyRelative("patternType"), GUIContent.none, GUILayout.MaxWidth(150));
					PatternType type = (PatternType)pattern.FindPropertyRelative ("patternType").enumValueIndex;

					if (type == PatternType.ACTION)
					{
						actionEffect = pattern.FindPropertyRelative ("actionEffect");
						EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("actionType"), GUIContent.none, GUILayout.MaxWidth(150));

						ActionType actionType = (ActionType)actionEffect.FindPropertyRelative ("actionType").enumValueIndex;
						if (actionType == ActionType.BOOST)
						{
							EditorGUILayout.PropertyField (pattern.FindPropertyRelative("elementType"), GUIContent.none, GUILayout.MaxWidth(150));
						}
						else
						{
							EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("num"), GUIContent.none, GUILayout.MaxWidth(150));
						}

					}

					if (type == PatternType.STATUS)
					{
						statusEffect = pattern.FindPropertyRelative ("statusEffect");
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("statusType"), GUIContent.none, GUILayout.MaxWidth(150));
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("numOfTurns"), GUIContent.none, GUILayout.MaxWidth(150));
					}

					EditorGUILayout.EndHorizontal ();
				}
				if (UseAlternative == true)
				{
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Main Pattern: " + (j+1), EditorStyles.label, GUILayout.MaxWidth(100));
					pattern = enemyPattern.GetArrayElementAtIndex (j).FindPropertyRelative ("pattern");
					EditorGUILayout.PropertyField (pattern.FindPropertyRelative("patternType"), GUIContent.none, GUILayout.MaxWidth(150));
					PatternType type = (PatternType)pattern.FindPropertyRelative ("patternType").enumValueIndex;

					if (type == PatternType.ACTION)
					{
						actionEffect = pattern.FindPropertyRelative ("actionEffect");
						EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("actionType"), GUIContent.none, GUILayout.MaxWidth(150));

						ActionType actionType = (ActionType)actionEffect.FindPropertyRelative ("actionType").enumValueIndex;
						if (actionType == ActionType.BOOST)
						{
							EditorGUILayout.PropertyField (pattern.FindPropertyRelative("elementType"), GUIContent.none, GUILayout.MaxWidth(150));
						}
						else
						{
							EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("num"), GUIContent.none, GUILayout.MaxWidth(150));
						}
					}

					if (type == PatternType.STATUS)
					{
						statusEffect = pattern.FindPropertyRelative ("statusEffect");
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("statusType"), GUIContent.none, GUILayout.MaxWidth(150));
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("numOfTurns"), GUIContent.none, GUILayout.MaxWidth(150));
					}

					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Alternate : " + (j+1), EditorStyles.label, GUILayout.MaxWidth(100));
					alternativePattern = enemyPattern.GetArrayElementAtIndex (j).FindPropertyRelative ("alternativePattern");
					EditorGUILayout.PropertyField (alternativePattern.FindPropertyRelative("patternType"), GUIContent.none, GUILayout.MaxWidth(150));
					type = (PatternType)alternativePattern.FindPropertyRelative ("patternType").enumValueIndex;

					if (type == PatternType.ACTION)
					{
						actionEffect = alternativePattern.FindPropertyRelative ("actionEffect");
						EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("actionType"), GUIContent.none, GUILayout.MaxWidth(150));

						ActionType actionType = (ActionType)actionEffect.FindPropertyRelative ("actionType").enumValueIndex;
						if (actionType == ActionType.BOOST)
						{
							EditorGUILayout.PropertyField (alternativePattern.FindPropertyRelative("elementType"), GUIContent.none, GUILayout.MaxWidth(150));
						}
						else
						{
							EditorGUILayout.PropertyField (actionEffect.FindPropertyRelative("num"), GUIContent.none, GUILayout.MaxWidth(150));
						}
					}

					if (type == PatternType.STATUS)
					{
						statusEffect = alternativePattern.FindPropertyRelative ("statusEffect");
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("statusType"), GUIContent.none, GUILayout.MaxWidth(150));
						EditorGUILayout.PropertyField (statusEffect.FindPropertyRelative("numOfTurns"), GUIContent.none, GUILayout.MaxWidth(150));
					}
					EditorGUILayout.EndHorizontal ();
				}

			}
		}
		serializedObject.ApplyModifiedProperties ();
	}

	void AddEntry(int index, SerializedProperty sp)
	{
		if(sp.arraySize >= 0) sp.InsertArrayElementAtIndex (index);
	}

	void RemoveEntry(int index, SerializedProperty sp)
	{
		if(sp.arraySize >= 0) sp.DeleteArrayElementAtIndex (index);
	}
}
