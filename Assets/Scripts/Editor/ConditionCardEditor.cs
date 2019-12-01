using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ConditionCardData))]
[CanEditMultipleObjects]
public class ConditionCardEditor : Editor {

	SerializedProperty condition;
	SerializedProperty failedConditionEffects;
	SerializedProperty successConditionEffects;

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		condition = serializedObject.FindProperty ("conditions");
		failedConditionEffects = serializedObject.FindProperty ("failedConditionEffects");
		successConditionEffects = serializedObject.FindProperty ("successConditionEffects");

		ConditionCardData card = (ConditionCardData)target;

		card.IsConditionCard = EditorGUILayout.Toggle ("Is Condition Card", card.IsConditionCard);
		card.name = EditorGUILayout.TextField ("Card Name", card.name);
		card.staminaCost = EditorGUILayout.IntField ("Stamina Cost", card.staminaCost);

		EditorGUILayout.LabelField ("Description", GUIStyle.none, GUILayout.MaxWidth(30));
		GUIStyle myTextAreaStyle = new GUIStyle(EditorStyles.textArea);
		myTextAreaStyle.wordWrap = true;

		card.description = EditorGUILayout.TextArea(card.description,myTextAreaStyle, GUILayout.Height(60), GUILayout.MaxHeight(60));
		card.artwork = (Sprite)EditorGUILayout.ObjectField ("Artwork", card.artwork, typeof(Sprite), false);

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Conditions", EditorStyles.boldLabel, GUILayout.MaxWidth(80));
		condition.arraySize = EditorGUILayout.IntField (condition.arraySize);

		for(int i = 0; i < condition.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (condition.GetArrayElementAtIndex(i).FindPropertyRelative ("conditionType"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.PropertyField (condition.GetArrayElementAtIndex(i).FindPropertyRelative ("conditionComparison"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.PropertyField (condition.GetArrayElementAtIndex(i).FindPropertyRelative ("value"), GUIContent.none, GUILayout.MaxWidth(50));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, condition);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, condition);

			EditorGUILayout.EndHorizontal ();
		}

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Failed Condition Effects", EditorStyles.boldLabel, GUILayout.MaxWidth(180));
		failedConditionEffects.arraySize = EditorGUILayout.IntField (failedConditionEffects.arraySize);

		for(int i = 0; i < failedConditionEffects.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (failedConditionEffects.GetArrayElementAtIndex(i).FindPropertyRelative ("actionType"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.PropertyField (failedConditionEffects.GetArrayElementAtIndex (i).FindPropertyRelative ("num"), GUIContent.none, GUILayout.MaxWidth (50));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, failedConditionEffects);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, failedConditionEffects);

			EditorGUILayout.EndHorizontal ();
		}

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Success Condition Effects", EditorStyles.boldLabel, GUILayout.MaxWidth(180));
		successConditionEffects.arraySize = EditorGUILayout.IntField (successConditionEffects.arraySize);

		for(int i = 0; i < successConditionEffects.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (successConditionEffects.GetArrayElementAtIndex(i).FindPropertyRelative ("actionType"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.PropertyField (successConditionEffects.GetArrayElementAtIndex (i).FindPropertyRelative ("num"), GUIContent.none, GUILayout.MaxWidth (50));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, successConditionEffects);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, successConditionEffects);

			EditorGUILayout.EndHorizontal ();
		}
		serializedObject.ApplyModifiedProperties ();
	}



	void AddEntry(int index, SerializedProperty sp)
	{
		if(sp.arraySize > 0) sp.InsertArrayElementAtIndex (index);
	}

	void RemoveEntry(int index, SerializedProperty sp)
	{
		if(sp.arraySize > 0) sp.DeleteArrayElementAtIndex (index);
	}

	void MoveUp(int index, SerializedProperty sp)
	{
		sp.MoveArrayElement (index, index - 1);
	}

	void MoveDown(int index, SerializedProperty sp)
	{
		sp.MoveArrayElement (index, index + 1);
	}

	//card.description = EditorGUILayout.TextField ("Description", card.description);//, myTextAreaStyle, GUILayout.MaxHeight(60));
	//card.description = EditorGUILayout.TextArea(card.description,myTextAreaStyle, GUILayout.MaxHeight(60));
	//card.description = EditorGUILayout.TextArea("Description", card.description,GUILayout.Height(50));
	//card.artwork = EditorGUILayout.ObjectField (Sprite, card.artwork);
	//GUIStyle myTextAreaStyle = new GUIStyle(EditorStyles.textArea);
	//myTextAreaStyle.wordWrap = true;
}
