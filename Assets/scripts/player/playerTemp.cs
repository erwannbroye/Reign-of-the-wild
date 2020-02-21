using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTemp : MonoBehaviour
{
    // Start is called before the first frame update

    public float Temp;
    public float equipementHeat;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
            Temp += other.gameObject.GetComponent<boneFire>().heat;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
            Temp -= other.gameObject.GetComponent<boneFire>().heat;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
            Temp = other.gameObject.GetComponent<boneFire>().heat;
    }
    void Start()
    {
        
    }

    void OnEquipementChange(float newHeat)
    {
        equipementHeat = newHeat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
