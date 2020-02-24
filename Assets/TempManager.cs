using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    // Start is called before the first frame update

	public Temp temp;
	public Temp tempInventory;

    public float globalTemp;
    public float windFactor;

    public playerTemp player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp.changeRatio( (-1 * ((globalTemp + player.Temp + player.equipementHeat - 15) * (windFactor / 100 + 1) )) / 10);
        tempInventory.changeRatio( (-1 * ((globalTemp + player.Temp + player.equipementHeat - 15) * (windFactor / 100 + 1) )) / 10);
    }
}
