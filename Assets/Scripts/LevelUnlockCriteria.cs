using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockCriteria : MonoBehaviour
{
    // Start is called before the first frame update
    SaveFile save;

    [SerializeField] int UnlockLevel = -1;
    [SerializeField] int UnlockQuaso = -1;
    bool locked = true;
    void Start()
    {
        save = FindObjectOfType<SaveFile>();
    }

    // Update is called once per frame
    void Update()
    {
        locked = false;
        if(UnlockLevel != -1 && !save.CheckLevel(UnlockLevel)) locked = true;
        if(UnlockQuaso != -1 && !save.CheckCollectible(UnlockQuaso,true))   locked = true;
        GetComponent<Button>().interactable = !locked;

    }
}
