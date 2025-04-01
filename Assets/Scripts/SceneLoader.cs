using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene(){
        int index = SceneManager.GetActiveScene().buildIndex +1;
        if (index < SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(index);
        }
    }

    public void PrevScene(){
        int index = SceneManager.GetActiveScene().buildIndex -1;
        if (index>= 0){
            SceneManager.LoadScene(index);
        } 
    }

    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public void ResetGame(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
