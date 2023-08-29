using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    
   
 

    // public IEnumerator AsyncLoad(string Lname){
    //     var scene = SceneManager.LoadSceneAsync(Lname);
    //     //if(scene.isDone) LoadingScreen.SetActive(false);
    //     scene.allowSceneActivation = false;
    //     do{
    //         yield return null;
    //     }
    //     while(scene.progress < 0.9f);
    //     LoadingScreen.SetActive(false);
    //     scene.allowSceneActivation = true;
        
        
    // }

    // Update is called once per frame
    public void Select(string Lname){
 
        SceneManager.LoadScene(Lname);
        if(FindObjectOfType<SpeedRunTimer>()!=null) FindObjectOfType<SpeedRunTimer>().resetClock();
        
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
