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
   public int currentPosition = 0;
    [SerializeField]
    public bool isInReverseBoard = false;
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
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    public int playerIndex;
    [SerializeField]
   public Button rollButton;
    [SerializeField]
   public Button endTurnButton;
    [SerializeField]
   public GameObject camera;
    [SerializeField]
   public Transform[] cameraRails1;
    [SerializeField]
   public Transform[] cameraRails2;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = positions[0].transform.position;
    }

    public void UpdateUI(string playerName, string diceResult, string phase) {
        CurrentPhase.text ="Current phase: " + phase;
        currentPlayerName.text = "Player: " + playerName;        
    }

    public void MoveCamera(GameObject camera, Transform[] rails) {

        if (this.currentPosition > 0 && this.currentPosition < 20) {
            camera.transform.position = rails[0].transform.position;        
        }
        else if (this.currentPosition >= 20 && this.currentPosition < 38)
        {
            camera.transform.position = rails[1].transform.position;
        }
        else if (this.currentPosition >= 38 && this.currentPosition < 58)
        {
            camera.transform.position = rails[2].transform.position;
        }
        else if (this.currentPosition >= 58 && this.currentPosition < 69)
        {
            camera.transform.position = rails[3].transform.position;
        }
        else if (this.currentPosition >= 69 && this.currentPosition < 80)
        {
            camera.transform.position = rails[4].transform.position;
        }
        else if (this.currentPosition >= 80 && this.currentPosition < 100)
        {
            camera.transform.position = rails[5].transform.position;
        }


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


    public void movementForward()
    {
        TurnIsFinished = false;
        int dice = Random.Range(1, 7);
        diceResult = dice;
        currentPosition += dice;
        diceRollText.text = "Dice result: " + dice.ToString();
        if (currentPosition >= 100)
        {

            currentPosition = 99;
        }
        this.transform.position = positions[currentPosition].transform.position;
        TurnIsFinished = true;
        Debug.Log("Se ha hecho una tirada de dado");


    }
    public void movementBackWards() {
        TurnIsFinished = false;
        int dice = Random.Range(1, 7);
        diceResult = dice;
        currentPosition -= dice;
        if (currentPosition <= 0)
        {
            currentPosition = 0;
        }
        diceRollText.text = "Dice result: " + dice.ToString();
        this.transform.position = positions2[currentPosition].transform.position;
        TurnIsFinished = true;
    }

    public void Roll() {

        print("player " + (playerIndex + 1) + " rolled their dice");
        GameEvent gameEvent = new GameEvent();
        gameEvent.eventType = GameEvent.Type.diceRoll;
        gameManager.HandleGameEvent(gameEvent);


    }
    public void EndTurn() {

        print("player " + (playerIndex + 1) + " ended their turn");
        GameEvent gameEvent = new GameEvent();
        gameEvent.eventType = GameEvent.Type.endTurn;
        gameManager.HandleGameEvent(gameEvent);


    }


    public void Movement() {       
        if (!isActive) { return; }
        if (Input.GetKeyDown(KeyCode.Space) && currentPosition < 100 && !isInReverseBoard )
        {
            movementForward();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentPosition > -1 && isInReverseBoard )
        {
            movementBackWards();
        } else if (Input.GetKeyUp(KeyCode.Space) && isActive){
            isActive = false;
            GameEvent gameEvent = new GameEvent();
            gameEvent.eventType = GameEvent.Type.endTurn;
            gameManager.HandleGameEvent(gameEvent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentPlayerIndex == this.playerIndex)
        {
            rollButton.interactable = ((int)gameManager.phase < (int)GameManager.Phase.Rolling);
            endTurnButton.interactable = ((int)gameManager.phase > (int)GameManager.Phase.Rolling);
        }
        else {
            rollButton.interactable = false;
            endTurnButton.interactable = false;
        }
      

    }
}