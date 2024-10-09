using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CinematicFinal : MonoBehaviour
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
        // Cargar la escena de men� inicial
        SceneManager.LoadScene("MenuInicial");
    }

    // M�todo para saltar con el bot�n de la UI
    public void SkipWithButton()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    // M�todo para saltar con la tecla 'E' (opcional)
    public void Skip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("MenuInicial");
        }
    }
}
