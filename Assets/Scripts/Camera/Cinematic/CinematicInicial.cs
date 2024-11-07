using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CinematicScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // El VideoPlayer asignado desde el Inspector
    public Button skipButton;  // El botón de skip asignado desde el Inspector

    void Start()
    {
        // Asegurarse de que el VideoPlayer está inicializado
        videoPlayer.loopPointReached += OnVideoFinished; // Evento cuando el video termine

        // Agregar la funcionalidad al botón de skip
        skipButton.onClick.AddListener(SkipWithButton);
    }

    // Este método será llamado cuando termine el video
    void OnVideoFinished(VideoPlayer vp)
    {
        // Cargar la escena de gameplay
        SceneManager.LoadScene("Tutorial");
    }

    // Método para saltar con el botón
    public void SkipWithButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Método para saltar con la tecla 'E' (opcional, puedes mantenerlo si quieres ambas opciones)
    public void Skip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
