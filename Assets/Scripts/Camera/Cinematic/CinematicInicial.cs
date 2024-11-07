using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CinematicScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // El VideoPlayer asignado desde el Inspector
    public Button skipButton;  // El bot�n de skip asignado desde el Inspector

    void Start()
    {
        // Asegurarse de que el VideoPlayer est� inicializado
        videoPlayer.loopPointReached += OnVideoFinished; // Evento cuando el video termine

        // Agregar la funcionalidad al bot�n de skip
        skipButton.onClick.AddListener(SkipWithButton);
    }

    // Este m�todo ser� llamado cuando termine el video
    void OnVideoFinished(VideoPlayer vp)
    {
        // Cargar la escena de gameplay
        SceneManager.LoadScene("Tutorial");
    }

    // M�todo para saltar con el bot�n
    public void SkipWithButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // M�todo para saltar con la tecla 'E' (opcional, puedes mantenerlo si quieres ambas opciones)
    public void Skip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
