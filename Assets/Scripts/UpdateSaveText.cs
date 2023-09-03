using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdateSaveText : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    [SerializeField] int ID= -1;
    [SerializeField] bool isBerry = false;

    SaveFile save;
    void Start()
    {
        save = FindObjectOfType<SaveFile>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBerry) text.text = save.getCollectibleCount(ID).ToString()+"/10 Blueberries";
        else text.text =  save.getDeaths(ID).ToString() + " Deaths";
    }
}
