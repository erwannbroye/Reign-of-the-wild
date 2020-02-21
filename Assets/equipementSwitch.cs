using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipementSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public int selected = 0;
    int previousSelect = 0;
    public Animator anim;
    Transform itemToUnequip;
    Transform actualItem;
    bool Switch = false;
    void Start()
    {
        SelectWeapon();
        anim.SetBool("equip", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != previousSelect)
            SelectWeapon();
        previousSelect = selected;
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selected) {
                actualItem = item;
                if (previousSelect == 0) {
                    Debug.Log("equip");
                item.gameObject.SetActive(true);
                    anim.SetBool("equip", true);
                }
                else if (selected == 0) {
                    Debug.Log("unequip");
                    item.gameObject.SetActive(true);
                    anim.SetBool("equip", false);
                }
                else {
                    anim.SetBool("equip", false);
                    Switch = true;
                    StartCoroutine(LateCall());
                }
            }
            else if (i == previousSelect) {
                itemToUnequip = item;
                StartCoroutine(LateCall());
            }
            i++;
        }
    }

         IEnumerator LateCall()
        {
    
            yield return new WaitForSeconds(1);
    
            itemToUnequip.gameObject.SetActive(false);
            if (Switch == true) {
                Switch = false;
                anim.SetBool("equip", true);
                actualItem.gameObject.SetActive(true);     
            }
        }
}
