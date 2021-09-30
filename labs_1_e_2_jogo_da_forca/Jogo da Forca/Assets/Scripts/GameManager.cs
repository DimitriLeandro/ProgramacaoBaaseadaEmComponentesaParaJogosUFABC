using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject letra; 
    public GameObject centro; 

    private string[] palavrasOcultas = new string[] {
        "carro", 
        "elefante", 
        "futebol",
        "girafa",
        "prato",
        "girassol",
        "escola",
        "parque",
        "bola",
        "casa",
        "aviao",
        "maquina",
        "castelo",
        "sapatenis",
        "estrela",
        "planeta",
        "facil"
    };
    private int numTentativas; 
    private int maxNumTentativas; 
    private string palavraOculta = ""; 
    private int tamanhoPalavraOculta; 
    
    int score = 0;
    char[] letrasOcultas; 
    bool[] letrasDescobertas; 

    void Start()
    {
        centro = GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
        numTentativas = 0;
        maxNumTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }

    void Update()
    {
       checkTeclado(); 
    }

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;
        for (int i=0; i<numLetras; i++){
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1); 
            l.transform.SetParent(GameObject.Find("Canvas").transform); 
        }
    }

    void InitGame()
    {
        int numeroAleatorio = Random.Range(0, palavrasOcultas.Length);
        palavraOculta = palavrasOcultas[numeroAleatorio];
        tamanhoPalavraOculta = palavraOculta.Length; 
        palavraOculta = palavraOculta.ToUpper(); 
        letrasOcultas = new char[tamanhoPalavraOculta];
        letrasDescobertas = new bool[tamanhoPalavraOculta];
        letrasOcultas = palavraOculta.ToCharArray(); 
    }

    void checkTeclado()
    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);

            if(letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                numTentativas++;
                UpdateNumTentativas();

                if(numTentativas >= maxNumTentativas)
                {
                    SceneManager.LoadScene("Lab1_forca");
                }

                for (int i=0; i<=tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i+1)).GetComponent<Text>().text = letraTeclada.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            VerificaSePalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas: " + numTentativas + "/" + maxNumTentativas;
    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }

    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;

        for(int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }
        if(condicao)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }
}
