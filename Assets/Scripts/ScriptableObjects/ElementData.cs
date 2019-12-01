using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ElementType
{
	NONE = 0,
	DARK,
	LIGHT,
	FIRE,
	EARTH,
	WATER
};

[System.Serializable]
public class ElementEffect
{
	public ElementType elements;
	public float elementBuff;
	public string description;
}

[CreateAssetMenu(fileName = "New Element", menuName = "Underdog/ElementData" )]
public class ElementData : ScriptableObject {

	public ElementType elements;
	public List <ElementEffect> elementWeakness = new List<ElementEffect>();
	public List <ElementEffect> elementStrength = new List<ElementEffect>();
}
