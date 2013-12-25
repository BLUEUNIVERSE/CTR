using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    bool paused = false;
    public TweenPosition pauseMenu;
    public TweenPosition objecivesMenu;
    public TweenPosition quitMenu;
	// Use this for initialization


    void Awake()
    {
        Time.timeScale = 1.0f;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            pauseMenu.Play(false);
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            pauseMenu.Play(true);
            Time.timeScale = 0f;
            return (true);
        }
    }

    void OnResume()
    {
        pauseMenu.Play(false);
        Time.timeScale = 1.0f;
    }

    void OnRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnObjectives()
    {
         objecivesMenu.Play(true);
        quitMenu.Play(false);
    }

    void OnObjectivesOk()
    {
        objecivesMenu.Play(false);
    }
    void OnQuit()
    {
        quitMenu.Play(true);
        objecivesMenu.Play(false);
    }

    void OnCancelQuit()
    {
        quitMenu.Play(false);
    }
    void OnOkQuit()
    {
        Application.LoadLevel("Main Menu Scene");
    }
}
