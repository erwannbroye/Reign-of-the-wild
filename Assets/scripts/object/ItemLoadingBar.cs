using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoadingBar : MonoBehaviour
{

	public Transform LoadingBar;
	bool isLoading;
	float useDelay;

	void Start()
	{
		isLoading = false;
	}

	void Update()
	{
		if (isLoading == true)
		{
			float loadingBarX = LoadingBar.GetChild(0).localScale.x;
			if (loadingBarX >= 1f)
			{
				LoadingBar.gameObject.SetActive(false);
				isLoading = false ;
				LoadingBar.GetChild(0).localScale = new Vector3(0f, LoadingBar.GetChild(0).localScale.y, LoadingBar.GetChild(0).localScale.z);
				return;
			}
			LoadingBar.GetChild(0).localScale = new Vector3(loadingBarX + (Time.deltaTime / useDelay), LoadingBar.GetChild(0).localScale.y, LoadingBar.GetChild(0).localScale.z);
			LoadingBar.gameObject.SetActive(true);
		}
	}

    public void FillLoadingBar(float newUseDelay)
	{
		if (newUseDelay > 0f)
		{
			useDelay = newUseDelay;
			isLoading = true;
		}
	}
}
