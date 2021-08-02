using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    GameObject[] positions;
    [SerializeField]
    GameObject[] positions2;
    [SerializeField]
    int currentPosition = 0;
    [SerializeField]
    bool isInReverseBoard = false;
    [SerializeField]
    bool hasMoved = false;
    [SerializeField]
    bool TurnIsFinished = false;
    [SerializeField]
    Text diceRollText;
    [SerializeField]
    Text CurrentPhase;
    [SerializeField]
    Text currentPlayerName;
    [SerializeField]
    string playerName;
    [SerializeField]
    public bool isActive;
    [SerializeField]
    public int diceResult = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = positions[0].transform.position;
    }

    public void UpdateUI(string playerName, string diceResult, string phase) {
        CurrentPhase.text ="Current phase: " + phase;
        currentPlayerName.text = "Player: " + playerName;        
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Wormholes" && TurnIsFinished)
        {
            
            this.currentPosition = other.gameObject.GetComponent<WormHole>().otherPosition;
            isInReverseBoard = !isInReverseBoard;
            if (!isInReverseBoard) { this.transform.position = positions[currentPosition].transform.position; } else {
                this.transform.position = positions2[currentPosition].transform.position;
            }
            TurnIsFinished = false;

        }


        if (other.gameObject.tag == "Ladders" && TurnIsFinished)
        {
            this.currentPosition = other.gameObject.GetComponent<Ladders>().otherPosition;
            if (!isInReverseBoard) { this.transform.position = positions[currentPosition].transform.position; }
            else
            {
                this.transform.position = positions2[currentPosition].transform.position;
            }
            TurnIsFinished = false;
        }

        if (other.gameObject.tag == "Chutes" && TurnIsFinished)
        {
            currentPosition = other.gameObject.GetComponent<Chutes>().otherPosition;
            if (!isInReverseBoard) { this.transform.position = positions[currentPosition].transform.position; }
            else
            {
                this.transform.position = positions2[currentPosition].transform.position;
            }
            TurnIsFinished = false;
        }

    }

    public void StartTurn() {
        isActive = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentPosition < 100 && !isInReverseBoard && isActive)
        {
            TurnIsFinished = false;
            int dice = Random.Range(1, 7);
            diceResult = dice;
            currentPosition += dice;
            diceRollText.text = "Dice result: " + dice.ToString();
            if (currentPosition >= 100) {

                currentPosition = 99;
            }
            this.transform.position = positions[currentPosition].transform.position;
            TurnIsFinished = true;
            isActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentPosition > -1 && isInReverseBoard && isActive)
        {
            TurnIsFinished = false;
            int dice = Random.Range(1, 7);
            diceResult = dice;
            currentPosition -= dice;
            if (currentPosition <= 0) {
                currentPosition = 0;
            }
            diceRollText.text = "Dice result: " + dice.ToString();
            this.transform.position = positions2[currentPosition].transform.position;
            TurnIsFinished = true;
            isActive = false;

        }
    }
}
