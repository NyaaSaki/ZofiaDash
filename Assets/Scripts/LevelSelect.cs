using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Select(string Lname){
        FindObjectOfType<SpeedRunTimer>().resetClock();
        SceneManager.LoadScene(Lname);
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
