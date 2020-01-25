using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoadingBar : MonoBehaviour
{

	public Transform LoadingBar;

    public IEnumerator FillLoadingBar(float useDelay)
	{
		LoadingBar.gameObject.SetActive(true);
		for (int i = 0; i < 1000; i++)
		{
			LoadingBar.GetChild(0).localScale = new Vector3(i / 1000f, LoadingBar.GetChild(0).localScale.y, LoadingBar.GetChild(0).localScale.z);
			yield return new WaitForSeconds(useDelay / 1000f);
		}
		LoadingBar.gameObject.SetActive(false);
	}
}
