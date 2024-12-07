using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {

    public static string keyword;

    // Start is called before the first frame update
    void Start(){
        
        
        
    }

    public void askGoogle(string passedKeyword) {
        keyword = passedKeyword;
        

    }

    public void changeScene(string scene){
        SceneManager.LoadSceneAsync(scene);

    }


    public void Quit()
    {
        Application.Quit();
    }

    
    IEnumerator makeURLRequest()
    {
        string url = "https://bhojrajbhatt.com/";

        using (WWW www = new WWW(url))
        {
            yield return www;
            Debug.Log(www.text);
            
           
        }
    }




}







// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
