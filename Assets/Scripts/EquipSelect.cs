using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EquipSelect : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] public List<String> equip;
    [SerializeField] int currentEquip = -1;
    [SerializeField] TextMeshProUGUI itemName;
    void Start()
    {
     if(FindObjectOfType<SaveFile>()) equip = FindObjectOfType<SaveFile>().SkillObtained; 
     print(equip);
     scroll(FindObjectOfType<PlayerController>());   
    }

    public void scroll(PlayerController player){
        if(equip.Count == 0) return;
        currentEquip = (currentEquip + 1)%equip.Count;
        print(equip[currentEquip]);
        itemName.text = ".> "+ equip[currentEquip];

        player.hasUmbrella = (equip[currentEquip] == "Umbrella");
        player.hasPaws = (equip[currentEquip]) == "Cat Paws";

    }


}
