using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instante;

    int lifes = 3;
    int turn = 1; // 0 - Computador | 1 - Animação | 2 - Jogador
    int userSequence = 0;
    int level = 1;

    float count = 0;

    List<int> sequence = new List<int>();
    public Transform[] lamps;

    public GameObject messageObj;
    public Sprite brokenLamp;

    private void Awake()
    {
        if (!instante)
            instante = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            sequence.Add(Random.Range(0, 14));
            for (int s = 0; s < 10; s++) { }
        }
        SendGameMessage("Nível " + level.ToString());
        StartCoroutine("PlaySequence");
    }

    void Update()
    {
        if (turn == 0) //Computador
        {
            sequence.Add(Random.Range(0, 14));
            turn = 1;
            SendGameMessage("Nível " + level.ToString());
            StartCoroutine("PlaySequence");
        }
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(2);
        foreach (int i in sequence)
        {
            PlayLamp(i);
            yield return new WaitForSeconds(2);
        }
        SendGameMessage("Sua Vez!");
        turn = 2;
    }

    public void PlayLamp(int index)
    {
        if (!lamps[index].GetComponent<LampButton>().broken)
            lamps[index].GetComponentInChildren<Animation>().Play("TurnOnLamp");
    }

    public void CheckUserInput(int lamp)
    {
        PlayLamp(lamp);
        while (lamps[sequence.ElementAt(userSequence)].GetComponent<LampButton>().broken)            
        {
            Debug.Log("Lampada quebrada");
            userSequence++;
        }

        if (sequence.ElementAt(userSequence) == lamp)
        {
            userSequence++;
            if (userSequence == sequence.Count)
            {
                turn = 0;
                userSequence = 0;
                level++;
                if (level > 10)
                {
                    SendGameMessage("AEAE VENCEU");
                    turn = 3;
                }
            }
        }
        else
        {
            lifes--;
            if (lifes == 0)
            {
                SendGameMessage("GAME OVER!");
                turn = 1;
                return;
            }
            SendGameMessage("ERROU!");
            lamps[lamp].GetComponent<LampButton>().broken = true;
            lamps[lamp].GetComponent<Image>().sprite = brokenLamp;
            lamps[lamp].GetComponentInChildren<Animation>().Stop();
            userSequence = 0;
            turn = 1;
            StartCoroutine("PlaySequence");
        }

    }

    public bool isUserTurn() { return turn == 2; }

    void SendGameMessage(string message)
    {
        Debug.Log(message);
        messageObj.GetComponentInChildren<Text>().text = message;
        messageObj.SetActive(false);
        messageObj.SetActive(true);

        //messageObj.gameObject.SetActive(false);
        //messageObj.GetComponentInChildren<Text>().text = message;
        //messageObj.gameObject.SetActive(true);
    }
}
