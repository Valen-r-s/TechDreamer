using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class puntuacion : MonoBehaviour
{
    public Image imagen1;
    public Image imagen2;
    public Image imagen3;
    public Sprite bronce;
    public Sprite plata;
    public Sprite oro;

    void Start()
    {
        int numeroDeColisiones = PlayerPrefs.GetInt("NumeroDeColisiones", 0);

        if (numeroDeColisiones == 0)
        {
            imagen3.sprite = oro;
            imagen2.sprite = plata;
            imagen1.sprite = bronce;
        }
        else if (numeroDeColisiones == 1)
        {
            imagen2.sprite = plata;
            imagen1.sprite = bronce;
        }
        else if (numeroDeColisiones == 2)
        {
            imagen1.sprite = bronce;
        }

        PlayerPrefs.DeleteKey("NumeroDeColisiones");
        PlayerPrefs.Save();
    }
}
