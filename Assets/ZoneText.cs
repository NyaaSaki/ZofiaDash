using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneText : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI TextDesc;
    [SerializeField] TextMeshProUGUI TextName;
    
    [SerializeField] string ZoneName = "Highway";
    [SerializeField] string ZoneDesc = "These roads look broken...";
    [SerializeField] float fadeTime;
    [SerializeField] GameObject Zofia;


    void Start()
    {
        TextName.fontMaterial.SetColor("_FaceColor", Color.clear);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
  
    }

    void close(){
        StartCoroutine("FadeOut");
    }

    float cooldown = -1f;

    private void OnTriggerEnter2D(Collider2D other) {

        if(!other.CompareTag("Player") || cooldown > 0) return; 
        else {
        cooldown = 300;
        StartCoroutine("FadeIn");
        TextName.text = ZoneName;
        TextDesc.text = ZoneDesc;
        Invoke("close",3f);
        
        }
    }

    IEnumerator FadeIn(){
        float waitTime = 0;
        while (waitTime < 1)
        {
        TextName.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, waitTime));
        yield return null;
        waitTime += 2*Time.deltaTime / fadeTime;
        }
    }   

        IEnumerator FadeOut(){
        float waitTime = 0;
        while (waitTime < 1)
        {
        TextName.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white , Color.clear , waitTime));
        yield return null;
        waitTime += Time.deltaTime / fadeTime;
        }
    }   

}
