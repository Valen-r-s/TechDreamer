using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class WordManagerScript : MonoBehaviour
{
    public List<string> words;
    public GameObject wordPrefab;
    public Transform spawnPoint;
    public float speed = 5f;
    private Vector3 targetPosition = new Vector3(0f, -4.49f, 0f); 
    public int collisionCount = 0;  // Contador de colisiones
    public int maxCollisions = 3;   // Número máximo de colisiones antes del Game Over


    private List<GameObject> wordObjects = new List<GameObject>();
    private GameObject closestWordObject;
    private string currentTypedWord = "";

    private float redColorDuration = 1f;
    private float redColorTimer = 0f;
    private float hurtDuration = 1.0f;  // Duración en segundos que el personaje se muestra herido
    private float hurtTimer = 0f;       // Temporizador para el estado herido


    // Referencias para el cambio de sprite del personaje
    public SpriteRenderer characterSpriteRenderer;
    public Sprite normalSprite;
    public Sprite hurtSprite;

    void Start()
    {
        words = new List<string> { "espacio", "nave", "estrella", "galaxia", "planeta" };
        InvokeRepeating("SpawnWord", 0f, 2f);
    }

    void Update()
    {
        UpdateWordColors();
        HandleInput();
        MoveWords();
        UpdateClosestWord();
        UpdateCharacterSprite();
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
                    break;
                }
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
                collisionCount++;  // Incrementa el contador de colisiones
                if (collisionCount >= maxCollisions)
                {
                    GameOver();  // Llama a la función de Game Over
                }
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
    void GameOver()
    {
        Debug.Log("Game Over!");  // Imprime un mensaje de Game Over en la consola
                                  // Aquí podrías añadir más lógica, como mostrar una pantalla de Game Over, detener el juego, etc.
    }

    void UpdateClosestWord()
    {
        float lowestY = float.MaxValue;  // Inicia con el máximo valor para comparar
        GameObject lowestWordObject = null;

        foreach (GameObject wordObject in wordObjects)
        {
            float posY = wordObject.transform.position.y;
            if (posY < lowestY)  // Busca la posición más baja en Y
            {
                lowestY = posY;
                lowestWordObject = wordObject;
            }
        }

        closestWordObject = lowestWordObject;  // Actualiza el objeto de palabra más cercano
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

    void ResetWordColors()
    {
        if (closestWordObject != null)
        {
            TextMeshPro textMeshPro = closestWordObject.GetComponent<TextMeshPro>();
            string plainText = Regex.Replace(textMeshPro.text, "<.*?>", string.Empty);
            textMeshPro.text = plainText; // Restablece el texto a blanco
        }
    }

    void SpawnWord()
    {
        string randomWord = words[Random.Range(0, words.Count)];
        float randomX = Random.Range(-10f, 10f);
        Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);
        GameObject wordObject = Instantiate(wordPrefab, spawnPosition, Quaternion.identity, transform);
        wordObjects.Add(wordObject);

        TextMeshPro textMeshPro = wordObject.GetComponent<TextMeshPro>();
        textMeshPro.text = randomWord;
    }
}

