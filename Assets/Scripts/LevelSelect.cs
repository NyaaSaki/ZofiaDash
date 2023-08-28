using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject LoadingScreen;
   
    void Awake(){
        LoadingScreen = GameObject.FindGameObjectWithTag("loadingScreen");
    }

    void Start()  {
        LoadingScreen.SetActive(false);
    }

    void OnDestroy() {
        LoadingScreen.SetActive(false);
    }

    public IEnumerator AsyncLoad(string Lname){
        var scene = SceneManager.LoadSceneAsync(Lname);
        //if(scene.isDone) LoadingScreen.SetActive(false);
        scene.allowSceneActivation = false;
        do{
            yield return null;
        }
        while(scene.progress < 0.9f);
        LoadingScreen.SetActive(false);
        yield return null;
        scene.allowSceneActivation = true;
        
        
    }

    // Update is called once per frame
    public void Select(string Lname){
        //FindObjectOfType<SpeedRunTimer>().resetClock();
        //LoadingScreen.SetActive(true);
        StartCoroutine(AsyncLoad(Lname));

        try {FindObjectOfType<SpeedRunTimer>().resetClock();}
        catch(UnityException a){}
    }

    void callReset(){
        FindObjectOfType<GameSessionManager>().QuitLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void resetLevel(){
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<death>().Kill();
        Invoke("callReset",0.25f);
    }
}
