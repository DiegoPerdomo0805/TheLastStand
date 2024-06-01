using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseGame : MonoBehaviour
{
    public Button buttonStart;
    public Button buttonFinish;
    private string juego = "SampleScene";

    void Start()
    {
        // Ensure buttonStart is assigned
        if (buttonStart != null)
        {
            buttonStart.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("buttonStart is not assigned.");
        }

        if (buttonFinish != null)
        {
            buttonFinish.onClick.AddListener(OnFinishButtonClicked);
        }
        else
        {
            Debug.LogError("buttonFinish is not assigned.");
        }
    }

    void OnStartButtonClicked()
    {
        Debug.Log(" Empecemos.");
        SceneManager.LoadScene(juego, LoadSceneMode.Single);
    }

    void OnFinishButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
