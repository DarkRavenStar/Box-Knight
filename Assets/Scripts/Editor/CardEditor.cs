using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData))]
[CanEditMultipleObjects]
public class CardEditor : Editor {

	TextData textData = new TextData ();
	SerializedProperty actionEffect;
	SerializedProperty statusEffect;


	static string UppercaseFirst(string s)
    {
		if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		actionEffect = serializedObject.FindProperty("actionEffect");
		statusEffect = serializedObject.FindProperty ("statusEffect");

		CardData card = (CardData)target;

		card.IsConditionCard = EditorGUILayout.Toggle ("Is Condition Card", card.IsConditionCard);
		//Upgrade TYPE
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Upgrade Type", GUIStyle.none, GUILayout.MinWidth(40), GUILayout.ExpandWidth(true));
		GUILayout.FlexibleSpace();
		card.upgradeType = (UpgradeType)EditorGUILayout.EnumPopup (card.upgradeType, GUILayout.ExpandWidth(true));
		EditorGUILayout.EndHorizontal ();

		//Upgrade Card
		card.upgradeCard = (CardData)EditorGUILayout.ObjectField ("Upgrade Card", card.upgradeCard, typeof(CardData), false);

		card.name = EditorGUILayout.TextField ("Card Name", card.name);
		card.staminaCost = EditorGUILayout.IntField ("Stamina Cost", card.staminaCost);
		card.element = (ElementData) EditorGUILayout.ObjectField ("Element Type", card.element, typeof(ElementData), false);

		EditorGUILayout.LabelField ("Description", GUIStyle.none, GUILayout.MaxWidth(30));
		GUIStyle myTextAreaStyle = new GUIStyle(EditorStyles.textArea);
		myTextAreaStyle.wordWrap = true;
		card.description = EditorGUILayout.TextArea(card.description,myTextAreaStyle, GUILayout.Height(60), GUILayout.MaxHeight(60));

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Full Description", GUIStyle.none, GUILayout.MaxWidth(30));
		card.fullDescription = EditorGUILayout.TextArea(card.fullDescription,myTextAreaStyle, GUILayout.Height(60), GUILayout.MaxHeight(60));
		card.artwork = (Sprite)EditorGUILayout.ObjectField ("Artwork", card.artwork, typeof(Sprite), false);

		// Action Effect
		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Action Effect", EditorStyles.boldLabel, GUILayout.MaxWidth(100));

		EditorGUILayout.BeginHorizontal ();

		actionEffect.arraySize = EditorGUILayout.IntField (actionEffect.arraySize);
		if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (actionEffect.arraySize, actionEffect);
		if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (actionEffect.arraySize-1, actionEffect);

		EditorGUILayout.EndHorizontal ();

		for(int i = 0; i < actionEffect.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (actionEffect.GetArrayElementAtIndex(i).FindPropertyRelative ("actionType"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.PropertyField (actionEffect.GetArrayElementAtIndex(i).FindPropertyRelative ("num"), GUIContent.none, GUILayout.MaxWidth(150));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, actionEffect);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, actionEffect);

			EditorGUILayout.EndHorizontal ();
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Status Effect
		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Status Effect", EditorStyles.boldLabel, GUILayout.MaxWidth(100));

		EditorGUILayout.BeginHorizontal ();

		statusEffect.arraySize = EditorGUILayout.IntField (statusEffect.arraySize);
		if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (statusEffect.arraySize, statusEffect);
		if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (statusEffect.arraySize-1, statusEffect);

		EditorGUILayout.EndHorizontal ();

		for(int i = 0; i < statusEffect.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (statusEffect.GetArrayElementAtIndex(i).FindPropertyRelative ("statusType"), GUIContent.none, GUILayout.MaxWidth(150));
			//EditorGUILayout.PropertyField (statusEffect.GetArrayElementAtIndex(i).FindPropertyRelative ("num"), GUIContent.none, GUILayout.MaxWidth(150));
			EditorGUILayout.LabelField ("Turns", GUILayout.MaxWidth(50));
			EditorGUILayout.PropertyField (statusEffect.GetArrayElementAtIndex(i).FindPropertyRelative ("numOfTurns"), GUIContent.none, GUILayout.MaxWidth(150));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft,GUILayout.MaxWidth(20)))	AddEntry (i, statusEffect);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight,GUILayout.MaxWidth(20)))	RemoveEntry (i, statusEffect);

			EditorGUILayout.EndHorizontal ();
		}
		serializedObject.ApplyModifiedProperties ();

		/*
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Description Builder
		StringBuilder stringBuilder = new StringBuilder ();
		if (card.actionEffect.Count != null)
		{
			for (int i = 0; i < card.actionEffect.Count; i++)
			{
				ActionType type = card.actionEffect [i].actionType;
				string effect = textData.ActionString[(int)type];
				string number = card.actionEffect [i].num.ToString ();
				string final = "";
				if (type == ActionType.ENHANCE)
				{
					final = effect;
				}
				else if (type == ActionType.BOOST)
				{
					if (card.element != null)
					{
						ElementType elementType = card.element.elements;
						string element = textData.ElementString[(int)elementType];
						final = effect + ": " + element;
					}
				}
				else
				{
					final = effect + ": " + number;
				}

				if(i < card.actionEffect.Count-1)
				{
					stringBuilder.Append(final + ", ");
				}
				else if(i == card.actionEffect.Count-1 && card.statusEffect.Count > 0)
				{
					stringBuilder.Append(final + ", ");
				}
				else
				{
					stringBuilder.Append(final);
				}
			}
		}

		if (card.statusEffect.Count != null)
		{
			for (int i = 0; i < card.statusEffect.Count; i++)
			{
				StatusType type = card.statusEffect [i].statusType;
				string effect = textData.StatusString[(int)type];
				string final = "";

				final = effect;

				if(i < card.statusEffect.Count-1)
				{
					stringBuilder.Append(final + ", ");
				}
				else
				{
					//stringBuilder.AppendLine
					stringBuilder.Append(final);
				}
			}
		}

		card.description = stringBuilder.ToString ();

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Full Description Builder
		StringBuilder stringFullBuilder = new StringBuilder ();
		if (card.actionEffect.Count != null)
		{
			for (int i = 0; i < card.actionEffect.Count; i++)
			{
				ActionType type = card.actionEffect [i].actionType;
				string effect = textData.ActionLongDesc[(int)type];
				string number = card.actionEffect [i].num.ToString ();
				string final = "";

				if (type == ActionType.BOOST)
				{
					if (card.element != null)
					{
						ElementType elementType = card.element.elements;
						string element = textData.ElementString[(int)elementType];
						effect = effect.Replace("_", element);
					}
				}
				else
				{
					effect = effect.Replace("_", number);
				}

				final = effect;

				if(i < card.actionEffect.Count-1)
				{
					stringFullBuilder.Append(final + "\n");
				}
				else if(i == card.actionEffect.Count-1 && card.statusEffect.Count > 0)
				{
					stringFullBuilder.Append(final + "\n");
				}
				else
				{
					stringFullBuilder.Append(final);
				}
			}
		}

		if (card.statusEffect.Count != null)
		{
			for (int i = 0; i < card.statusEffect.Count; i++)
			{
				StatusType type = card.statusEffect [i].statusType;
				string effect = textData.StatusLongDesc[(int)type];
				string status = textData.StatusString[(int)type];
				string final = "";
				if (type == StatusType.BLEED || type == StatusType.POISON || type == StatusType.STUN)
				{
					effect = effect.Replace("_", status);
				}

				final = effect;

				if(i < card.statusEffect.Count-1)
				{
					stringFullBuilder.Append(final + "\n ");
				}
				else
				{
					//stringBuilder.AppendLine
					stringFullBuilder.Append(final);
				}
			}
		}

		card.fullDescription = stringFullBuilder.ToString ();
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		*/
	}



	void AddEntry(int index, SerializedProperty sp)
	{
		if(index >= 0) sp.InsertArrayElementAtIndex (index);
	}

	void RemoveEntry(int index, SerializedProperty sp)
	{
		if(index >= 0) sp.DeleteArrayElementAtIndex (index);
	}

	void MoveUp(int index, SerializedProperty sp)
	{
		if(index >= 0) sp.MoveArrayElement (index, index - 1);
	}

	void MoveDown(int index, SerializedProperty sp)
	{
		if(index >= 0) sp.MoveArrayElement (index, index + 1);
	}
}
