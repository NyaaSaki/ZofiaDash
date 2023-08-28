using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameSessionManager : MonoBehaviour
{
    [SerializeField] public bool isMuted;
    [SerializeField] int Deaths = 0;
    [SerializeField] int Blue = 0;

    [SerializeField] int BlueSaved = 0;
    [SerializeField] GameObject deathCountText;
    [SerializeField] GameObject blueCountText;
    [SerializeField] GameObject AssistButton;
    [SerializeField] Sprite AssistOn;
    [SerializeField] Sprite AssistOff;

    [SerializeField] GameObject Tips;

    // Start is called before the first frame update

    public bool AssistMode = false;
    public void NextLevel(){
        BlueSaved = Blue;
        GetComponent<SpeedRunTimer>().resetClock();
    }
    public void saveLevel(){
        BlueSaved = Blue;
    }
    public void QuitLevel(){
        Blue = BlueSaved;
        blueCountText.GetComponent<TextMeshProUGUI>().text = Blue.ToString();
        GetComponent<SpeedRunTimer>().resetClock();
    }

    void HideTips(){
        Tips.SetActive(false);
    }

    void Start()
    {
        Tips.SetActive(true);
        Invoke("HideTips",2f);
        if(FindObjectsOfType<GameSessionManager>().Length > 1) {Destroy(gameObject); }
        else {
            
            DontDestroyOnLoad(gameObject);
            }
        
        deathCountText.GetComponent<TextMeshProUGUI>().text = "0";
        blueCountText.GetComponent<TextMeshProUGUI>().text = "0";
        }

    // Update is called once per frame

    public void onDie(){
        Deaths +=1;
        deathCountText.GetComponent<TextMeshProUGUI>().text = Deaths.ToString();
        }

    public void onPick(){
        Blue +=1;
        blueCountText.GetComponent<TextMeshProUGUI>().text = Blue.ToString();
        }

    void Update()
    {

    }
    public void toogleAssist(){
        AssistMode= !AssistMode;
        if(AssistMode) {
            AssistButton.GetComponent<Image>().sprite = AssistOn;
            Time.timeScale = 0.7f;
            GetComponent<SpeedRunTimer>().invalidate();
        
        }
        else {
            AssistButton.GetComponent<Image>().sprite = AssistOff;
            Time.timeScale = 1;
            }
    }
    public void toogleMute(){
        isMuted = !isMuted;
    }
}
