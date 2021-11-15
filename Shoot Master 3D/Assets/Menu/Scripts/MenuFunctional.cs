using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctional : MonoBehaviour
{
    [SerializeField] private GameObject menuFone;
    [SerializeField] private float restartDelay;

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void ChangeMenuState(bool state)
    {
        menuFone.SetActive(state);
        if(state) ChangeTimeScale(0);
        else ChangeTimeScale(1);
    }

    public IEnumerator DelayedRestart()
    {
        ChangeTimeScale(0.5f);
        yield return new WaitForSecondsRealtime(restartDelay);
        ReloadLevel();
    }

    private void Start() 
    {
        ChangeMenuState(true);
    }

    
}
