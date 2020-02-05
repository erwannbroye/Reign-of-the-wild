using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalBar : MonoBehaviour
{
	public float maxValue = 1000f;
	public float value;
	public float decreasingRatio = 1f;
	public Transform barUI;

	public GameObject inventoryIcon;

	void Start()
    {
		value = maxValue;
    }
	
    protected virtual void Update()
    {
		value -= decreasingRatio * Time.deltaTime;
		barUI.localScale = new Vector3(barUI.localScale.x, value / 1000f, barUI.localScale.z);
	}

	public void switchPos()
	{
		gameObject.SetActive(!gameObject.activeSelf);
		inventoryIcon.SetActive(!inventoryIcon.activeSelf);
	}

	public void increaseRatio(float changeValue)
	{
		decreasingRatio += changeValue;
	}

	public void decreaseRatio(float changeValue)
	{
		decreasingRatio -= changeValue;
	}

	public void changeRatio(float changeValue)
	{
		decreasingRatio = changeValue;
	}

	public void decreaseValue(float changeValue)
	{
		value -= changeValue;
		if (value < 0)
			value = 0;
	}

	public void increaseValue(float changeValue)
	{
		value += changeValue;
		if (value > maxValue)
			value = maxValue;
	}

	public void changeValue(float changeValue)
	{
		value = changeValue;
		if (value < 0)
			value = 0;
		if (value > maxValue)
			value = maxValue;
	}
}
