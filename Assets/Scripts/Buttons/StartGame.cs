using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button buttonStart;
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
    }

    void OnStartButtonClicked()
    {
        Debug.Log(" Empecemos.");
        SceneManager.LoadScene(juego, LoadSceneMode.Single);
    }
}
