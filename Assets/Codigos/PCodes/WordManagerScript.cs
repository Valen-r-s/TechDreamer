using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;



public class WordManagerScript : MonoBehaviour
{
    public List<string> words; // Lista de palabras a usar
    public GameObject wordPrefab; // Prefab del objeto que representa la palabra
    public Transform spawnPoint; // Punto de aparición de las palabras
    public float speed = 5f; // Velocidad a la que se mueven las palabras
    private Vector3 targetPosition = new Vector3(2.95f, -4.12f, 0f); // Posición objetivo hacia la que se mueven las palabras
    public TextMeshPro paragraphText;

    private List<GameObject> wordObjects = new List<GameObject>(); // Lista de objetos de palabras en la escena
    private GameObject closestWordObject; // Objeto de palabra más cercano
    private string currentTypedWord = ""; // Palabra actualmente escrita por el jugador

    private float redColorDuration = 1f; // Duración del color rojo en la palabra
    private float redColorTimer = 0f; // Temporizador para el color rojo
    private float hurtDuration = 1.0f; // Duración del estado "herido" del personaje
    private float hurtTimer = 0f; // Temporizador para el estado "herido"
    private int collisionCount = 0; // Contador de colisiones

    public SpriteRenderer characterSpriteRenderer; // Renderizador del sprite del personaje
    public Sprite normalSprite; // Sprite normal del personaje
    public Sprite hurtSprite; // Sprite del personaje herido

    private string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    void Start()
    {
        words = new List<string> { "def", "suma", "return", "a", "b", "resultado", "function", "if", "else", "input", "int", "print", "for", "in", "range", "i", "len", "list", "total", "append" };
        ShuffleWords(); // Mezcla las palabras para variar el orden en cada juego
        InvokeRepeating("SpawnWord", 0f, 1.5f); // Empieza a invocar la aparición de palabras
        paragraphText.text = "<alpha=#00>" + string.Join(" ", words.ToArray()) + "</alpha>";
    }

    void ShuffleWords() // Función para mezclar las palabras
    {
        for (int i = 0; i < words.Count; i++)
        {
            string temp = words[i];
            int randomIndex = Random.Range(i, words.Count);
            words[i] = words[randomIndex];
            words[randomIndex] = temp;
        }
    }

    void Update()
    {
        UpdateWordColors();
        HandleInput();
        MoveWords();
        UpdateClosestWord();
        UpdateCharacterSprite();

        if (collisionCount >= 3)
        {
            PlayerPrefs.SetInt("NumeroDeColisiones", collisionCount);
            PlayerPrefs.Save();
            SceneManager.LoadScene("FinalScene");
        }
        else if (wordObjects.Count == 0 && words.Count == 0)
        {
            PlayerPrefs.SetInt("NumeroDeColisiones", collisionCount);
            PlayerPrefs.Save();
            SceneManager.LoadScene("puntaje");
        }
    }

    void SpawnWord()
    {
        if (words.Count > 0)
        {
            int randomIndex = Random.Range(0, words.Count);
            string randomWord = words[randomIndex];
            words.RemoveAt(randomIndex);

            float randomX = Random.Range(-10f, 10f);
            Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);
            GameObject wordObject = Instantiate(wordPrefab, spawnPosition, Quaternion.identity, transform);
            wordObjects.Add(wordObject);

            TextMeshPro textMeshPro = wordObject.GetComponent<TextMeshPro>();
            textMeshPro.text = randomWord;
        }
        else
        {
            CancelInvoke("SpawnWord");
        }
    }

    void MoveWords()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject wordObject in wordObjects)
        {
            Vector3 directionToTarget = (targetPosition - wordObject.transform.position).normalized;
            wordObject.transform.Translate(directionToTarget * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(wordObject.transform.position, targetPosition) < 0.1f)
            {
                characterSpriteRenderer.sprite = hurtSprite;
                toRemove.Add(wordObject);
                collisionCount++;
                redColorTimer = redColorDuration;
                hurtTimer = hurtDuration;
            }
        }

        foreach (GameObject word in toRemove)
        {
            wordObjects.Remove(word);
            Destroy(word);
        }
    }

    void HandleInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
                {
                    char keyChar = keyCode.ToString().ToLower()[0];
                    currentTypedWord += keyChar;
                    CheckTypedWord();
                }
            }
        }
    }

    void CheckTypedWord()
    {
        if (closestWordObject != null)
        {
            TextMeshPro textMeshPro = closestWordObject.GetComponent<TextMeshPro>();
            string plainText = Regex.Replace(textMeshPro.text, "<.*?>", string.Empty);

            if (plainText.StartsWith(currentTypedWord))
            {
                if (currentTypedWord.Equals(plainText))
                {
                    wordObjects.Remove(closestWordObject);
                    Destroy(closestWordObject);
                    MakeWordVisible(plainText); // Haz la palabra visible en el párrafo
                    closestWordObject = null;
                    currentTypedWord = "";
                }
                else
                {
                    string updatedText = $"<color=#00FF00>{currentTypedWord}</color>" + plainText.Substring(currentTypedWord.Length);
                    textMeshPro.text = updatedText;
                }
            }
            else
            {
                textMeshPro.text = $"<color=#FF0000>{plainText}</color>";
                redColorTimer = redColorDuration;
                currentTypedWord = "";
            }
        }
    }


    void UpdateWordColors()
    {
        if (redColorTimer > 0)
        {
            redColorTimer -= Time.deltaTime;
            if (redColorTimer <= 0)
            {
                ResetWordColors();
            }
        }
    }

    void ResetWordColors()
    {
        if (closestWordObject != null)
        {
            TextMeshPro textMeshPro = closestWordObject.GetComponent<TextMeshPro>();
            string plainText = Regex.Replace(textMeshPro.text, "<.*?>", string.Empty);
            textMeshPro.text = plainText;
        }
    }

    void MakeWordVisible(string word)
    {
        // Reemplaza la primera ocurrencia de la palabra con una versión visible
        string hiddenWord = "<alpha=#00>" + word + "</alpha>";
        string visibleWord = "<alpha=#FF>" + word + "</alpha>";
        paragraphText.text = ReplaceFirst(paragraphText.text, hiddenWord, visibleWord);
    }


    void UpdateClosestWord()
    {
        float lowestY = float.MaxValue;
        GameObject lowestWordObject = null;

        foreach (GameObject wordObject in wordObjects)
        {
            float posY = wordObject.transform.position.y;
            if (posY < lowestY)
            {
                lowestY = posY;
                lowestWordObject = wordObject;
            }
        }

        closestWordObject = lowestWordObject;
    }

    void UpdateCharacterSprite()
    {
        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer <= 0)
            {
                characterSpriteRenderer.sprite = normalSprite;
            }
        }
    }
}
