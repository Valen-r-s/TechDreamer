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
    public TextMeshPro paragraphText; // Texto del párrafo donde las palabras se vuelven visibles

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

    void Start()
    {
        words = new List<string> { "def", "suma", "return", "input", "int", "print", "for", "in", "range", "len", "list", "append", "if", "else" };
        string allText = @"def suma(a, b):
    return a + b

# Lista para almacenar resultados de sumas
resultados = []

# Usando input para obtener valores
a = int(input(""Ingrese el primer número: ""))
b = int(input(""Ingrese el segundo número: ""))

# Llamada a la función suma
resultado = suma(a, b)
resultados.append(resultado)

# Uso de for, in, range y print para mostrar resultados
for i in range(len(resultados)):
    print(f""Resultado {i+1}: {resultados[i]}"")
    
# Condicionales para verificar el total
total = sum(resultados)
if total > 100:
    print(""El total supera 100"")
else:
    print(""El total es menor o igual a 100"")";

        // Ocultar las palabras clave inicialmente
        foreach (string word in words)
        {
            allText = Regex.Replace(allText, @"\b" + Regex.Escape(word) + @"\b", "<color=#00000000>" + word + "</color>");
        }

        paragraphText.text = allText;
        ShuffleWords();
        InvokeRepeating("SpawnWord", 0f, 1.5f);
    }




    void ShuffleWords()
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
            SceneManager.LoadScene("FinalScene");
        }
        else if (wordObjects.Count == 0 && words.Count == 0)
        {
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
        foreach (GameObject wordObject in wordObjects)
        {
            Vector3 directionToTarget = (targetPosition - wordObject.transform.position).normalized;
            wordObject.transform.Translate(directionToTarget * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(wordObject.transform.position, targetPosition) < 0.1f)
            {
                Destroy(wordObject);
                wordObjects.Remove(wordObject);
                collisionCount++;
                characterSpriteRenderer.sprite = hurtSprite;
                hurtTimer = hurtDuration;
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
                }
            }
        }
    }

    void CheckTypedWord()
    {
        if (closestWordObject != null)
        {
            TextMeshPro textMeshPro = closestWordObject.GetComponent<TextMeshPro>();
            string plainText = Regex.Replace(textMeshPro.text, "<.*?>", string.Empty); // Eliminar formato

            if (plainText.StartsWith(currentTypedWord))
            {
                if (currentTypedWord.Equals(plainText))
                {
                    wordObjects.Remove(closestWordObject);
                    Destroy(closestWordObject);
                    MakeWordVisible(currentTypedWord); // Cambiar la palabra a visible en verde
                    closestWordObject = null;
                    currentTypedWord = "";
                }
                else
                {
                    textMeshPro.text = "<color=#00FF00>" + currentTypedWord + "</color>" + plainText.Substring(currentTypedWord.Length);
                }
            }
            else
            {
                textMeshPro.text = "<color=#FF0000>" + plainText + "</color>";
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
            textMeshPro.text = Regex.Replace(textMeshPro.text, "<.*?>", string.Empty);
        }
    }

    void MakeWordVisible(string word)
    {
        if (words.Contains(word)) // Solo hace visible la palabra si está en la lista
        {
            string pattern = "<color=#00000000>" + Regex.Escape(word) + "</color>";
            paragraphText.text = paragraphText.text.Replace(pattern, "<color=#00FF00>" + word + "</color>");
        }
    }



    string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
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

