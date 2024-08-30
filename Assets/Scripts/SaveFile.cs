using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using TMPro;

public class SaveFile : MonoBehaviour
{
    // Start is called before the first frame update

    public List<int> CollectedBerries;

    public List<int> BrokenBlocks;
    public List<int> OpenedDoors;
    public List<int> CollectedQuaso;
    public List<int> CompletedLevels;
    public List<int> DeathCount;
    public List<String> SkillObtained;

    public Vector3 SpawnPoint;


    
    [SerializeField] List<string> levels;

    public int getCollectibleCount(int chap=-1,bool isQuaso = false){
        int CollectCount = 0;
        if(isQuaso) return CollectedQuaso.Count;
        if(CollectedBerries == null) CollectedBerries = new List<int>();
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
    async void Start()
    {
        if(FindObjectsOfType<SaveFile>().Length > 1) {Destroy(gameObject); }
        else {
            DontDestroyOnLoad(gameObject);
            }
        //Log In
        foreach (var item in levels)
        {
            DeathCount.Add(0);
        }

        await UnityServices.InitializeAsync();


    }


    public async void LogIn(String user , String pass = "Z0f!a_default"){
        try{
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(user,pass);
            print("Logged in as" + user);
            LoadData();
        }
        catch (RequestFailedException){
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(user,pass);
            print("Signed Up");
        }
        
    }
    [SerializeField] TMP_InputField Uname;
    public void LogInClick(){
        LogIn(Uname.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    struct cache{
        public List<int> berry;
        public List<int> doors;
        public List<int> blocks;

        public List<int> deaths;
        public List<String> skills; 
        public Vector3 spawn;
    }
    cache tempCache;
    async void InitializeData(){
        var savSession = CloudSaveService.Instance.Data;
        cache initCache = new cache();
        initCache.skills = new List<String>();
        initCache.deaths = DeathCount;
        var saveData = new Dictionary<string, object>();
        saveData["cache"] = initCache;
        await savSession.Player.SaveAsync(saveData);
        tempCache = initCache;
        
    }

    async void LoadData(){
        var savSession = CloudSaveService.Instance.Data;
        if(!AuthenticationService.Instance.IsSignedIn) {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            InitializeData();}

        var _load = await savSession.Player.LoadAllAsync();
        
        cache loadCache;
        try{loadCache = _load["cache"].Value.GetAs<cache>(); }
        catch(KeyNotFoundException){
            InitializeData();
            loadCache = tempCache;
        }
        
        BrokenBlocks = loadCache.blocks;
        SkillObtained = loadCache.skills;
        SpawnPoint = loadCache.spawn;
        CollectedBerries = loadCache.berry;
        DeathCount = loadCache.deaths; 
    }

    public async void saveCache(){
        var savSession = CloudSaveService.Instance.Data;
        cache UpdateCache = new cache();

        UpdateCache.deaths = DeathCount;
        UpdateCache.skills = SkillObtained;
        UpdateCache.blocks = BrokenBlocks;
        UpdateCache.berry = CollectedBerries;
        

        try{
            UpdateCache.spawn = FindObjectOfType<death>().respawn;
        }
        catch(Exception){ print("no spawn point");}
        var saveData = new Dictionary<string, object>();
        saveData["cache"] = UpdateCache;
        await savSession.Player.SaveAsync(saveData);
    }
}
