using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    // Start is called before the first frame update

    public List<int> CollectedBerries;
    public List<int> CollectedQuaso;
    public List<int> CompletedLevels;
    public List<int> DeathCount;


    
    [SerializeField] List<string> levels;

    public int getCollectibleCount(int chap=-1,bool isQuaso = false){
        int CollectCount = 0;
        if(isQuaso) return CollectedQuaso.Count;

        foreach (int item in CollectedBerries)
        {
            if(chap <-1) CollectCount ++;
            else if(Mathf.RoundToInt(item/10) == chap) CollectCount++;
        }
        return CollectCount;
    }

    public bool CheckCollectible(int ID , bool isQuaso = false){
        if(isQuaso) return(CollectedQuaso.Contains(ID));
        else return(CollectedBerries.Contains(ID));
    }

    public bool CheckLevel(int ID){
    return(CompletedLevels.Contains(ID));
    }

    public bool SaveCollectible(int ID , bool isQuaso = false){
        bool isNew = true;
        if(ID==-1) return true;

        if(isQuaso) isNew = !CollectedQuaso.Contains(ID);
        else isNew = !CollectedBerries.Contains(ID);

        if(isNew){
            if(isQuaso) CollectedQuaso.Add(ID);
            else CollectedBerries.Add(ID);
        }
        return isNew;
    }

    public bool FinishLevel(int ID){
        if(CompletedLevels.Contains(ID)) return false;
        else {
            CompletedLevels.Add(ID);
            return true;
            }
    }

    public void logDeath(int RoomID){
        DeathCount[RoomID] += 1;
    }

    public int getDeaths(int RoomID){
        return DeathCount[RoomID];
    }
    void Start()
    {
        if(FindObjectsOfType<SaveFile>().Length > 1) {Destroy(gameObject); }
        else {
            DontDestroyOnLoad(gameObject);
            }

        foreach (var item in levels)
        {
            DeathCount.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
