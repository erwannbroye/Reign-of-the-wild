using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
	new public string name = "Recipe Name";
	public Item itemOutput;
	public float craftTime = 0f;
	public List<Item> itemList = new List<Item>();
}