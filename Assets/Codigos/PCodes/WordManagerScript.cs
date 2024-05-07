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
    private Vector3 targetPosition = new Vector3(2.95f, -4.12f, 0f);
    public int collisionCount = 0;
    public int maxCollisions = 3;

    private List<GameObject> wordObjects = new List<GameObject>();
    private GameObject closestWordObject;
    private string currentTypedWord = "";

    private float redColorDuration = 1f;
    private float redColorTimer = 0f;
    private float hurtDuration = 1.0f;
    private float hurtTimer = 0f;

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
        UpdateWordVisibility();  // Added to update the visibility of the words
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
                collisionCount++;
                Debug.Log("Collision Count: " + collisionCount); // Rastrear el contador de colisiones
                if (collisionCount >= maxCollisions)
                {
                    Debug.Log("Game Over triggered"); // Confirmar que Game Over se activa
                    GameOver();
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


    // Aquí podrías añadir más lógica de finalización, como detener el juego, etc.
    void GameOver()
    {
        Debug.Log("GameOver function called");  // Confirmar que se llama a esta función
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager not found in the scene!");
        }
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
            textMeshPro.text = plainText;
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

    void UpdateWordVisibility()
    {
        // Manual coordinates from the provided sprite configuration
        float minX = 199.6386f - (527.999f / 2);
        float maxX = 199.6386f + (527.999f / 2);
        float minY = -2.4297f - (567.64f / 2);
        float maxY = -2.4297f + (567.64f / 2);

        foreach (GameObject wordObject in wordObjects)
        {
            Vector3 wordPos = wordObject.transform.position;
            TextMeshPro textMesh = wordObject.GetComponent<TextMeshPro>();
            bool isVisible = (wordPos.x >= minX && wordPos.x <= maxX &&
                              wordPos.y >= minY && wordPos.y <= maxY);
            textMesh.enabled = isVisible;
        }
    }
}

