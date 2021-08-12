using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Phase : int
    {   
        None = 0,
        Beginning = 1,
        Prerroll = 2,
        Rolling = 3,
        Result = 4,
        PostResult = 5,
        Movement = 6,
        PreLanding = 7,
        Landing = 8,
        PostLanding = 9
    }

    [SerializeField]
    GameObject baseLadder;
    [SerializeField]
    GameObject baseOtherLadder;
    [SerializeField]
    GameObject baseChute;
    [SerializeField]
    GameObject baseWormHole;
    [SerializeField]
    GameObject baseOtherWormHole;
    [SerializeField]
    GameObject baseOtherChute;
    [SerializeField]
    GameObject[] LadderToCreate;
    [SerializeField]
    GameObject[] OtherLadderToCreate;
    [SerializeField]
    GameObject[] board1Postions;
    [SerializeField]
    GameObject[] board2Postions;
    [SerializeField]
    GameObject[] ChuteToCreate;
    [SerializeField]
    GameObject[] OtherChuteToCreate;
    [SerializeField]
    GameObject[] wormHoleToCreate;
    [SerializeField]
    GameObject[] otherWormHoleToCreate;
    [SerializeField]
    int amountOfLadders = 3;
    [SerializeField]
    int amountOfChutes = 3;
    [SerializeField]
    int amountOfWormHoles = 4;
    [SerializeField]
    public Phase phase = Phase.None;
    [SerializeField]
    PlayerMovement[] playerList;
    [SerializeField]
    public int currentPlayerIndex;
    [SerializeField]
    Text currentPlayerName;
    [SerializeField]
    AudioSource musicPlayer1;
    [SerializeField]
    AudioSource musicPlayer2;
    


    public void HandleGameEvent(GameEvent gameEvent) {
        switch (gameEvent.eventType) {
            case GameEvent.Type.startTurn:
                phase = Phase.Beginning;
                break;
            case GameEvent.Type.playCard:                
                break;
            case GameEvent.Type.diceRoll:
                DiceRoll();
                phase = Phase.Result;
                break;
            case GameEvent.Type.endTurn:
                currentPlayerIndex++;
                currentPlayerIndex %= 4;
                if (!playerList[currentPlayerIndex].isInReverseBoard)
                {
                    playerList[currentPlayerIndex].MoveCamera(playerList[currentPlayerIndex].camera, playerList[currentPlayerIndex].cameraRails1);
                    if (musicPlayer2.isPlaying) {
                        musicPlayer2.Stop();
                        musicPlayer1.Play();
                    }
                } else {
                    playerList[currentPlayerIndex].MoveCamera(playerList[currentPlayerIndex].camera, playerList[currentPlayerIndex].cameraRails2);
                    if (musicPlayer1.isPlaying)
                    {
                        musicPlayer1.Stop();
                        musicPlayer2.Play();
                    }


                }
                Debug.Log("starting turn");
                phase = Phase.Beginning;

                break;
        }    
    }

    public void DiceRoll()
    {
        PlayerMovement player = playerList[currentPlayerIndex];
        if (player.currentPosition < 100 && !player.isInReverseBoard)
        {
            player.movementForward();
        } else if (player.currentPosition > -1 && player.isInReverseBoard)
        {
            player.movementBackWards();
        }
    }
        void LadderFactory(GameObject[] boardPositions){
        for (int i = 0; i < amountOfLadders; i++) {
            GameObject firstLadder = Instantiate(baseLadder, new Vector3(0, 0, 0), Quaternion.identity);

            GameObject otherLadder = Instantiate(baseOtherLadder, new Vector3(0, 0, 0), Quaternion.identity);

            firstLadder.GetComponent<Ladders>().otherLadder = otherLadder;
            switch (i) {
                case 0:
            firstLadder.GetComponent<Ladders>().position = Random.Range(0, 20);
                    otherLadder.GetComponent<OtherLadder>().otherPosition = firstLadder.GetComponent<Ladders>().position + Random.Range(8, 19);
                    break;
                case 1:
             firstLadder.GetComponent<Ladders>().position = Random.Range(22, 38);
                    otherLadder.GetComponent<OtherLadder>().otherPosition = firstLadder.GetComponent<Ladders>().position + Random.Range(8, 19);
                    break;
                case 2:
                    firstLadder.GetComponent<Ladders>().position = Random.Range(41, 58);
                    otherLadder.GetComponent<OtherLadder>().otherPosition = firstLadder.GetComponent<Ladders>().position + Random.Range(8, 19);
                    break;
                case 3:
                    firstLadder.GetComponent<Ladders>().position = Random.Range(62, 77);
                    otherLadder.GetComponent<OtherLadder>().otherPosition = firstLadder.GetComponent<Ladders>().position + Random.Range(8, 19);
                    break;
                case 4:
                    firstLadder.GetComponent<Ladders>().position = Random.Range(0, 80);
                    otherLadder.GetComponent<OtherLadder>().otherPosition = firstLadder.GetComponent<Ladders>().position + Random.Range(8, 19);
                    break;
            }
            LadderToCreate[i] = firstLadder;
            OtherLadderToCreate[i] = otherLadder;
            LadderToCreate[i].transform.position = boardPositions[firstLadder.GetComponent<Ladders>().position].transform.position;
            LadderToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
            OtherLadderToCreate[i].transform.position = boardPositions[otherLadder.GetComponent<OtherLadder>().otherPosition].transform.position;
            OtherLadderToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
        }
    }
    void ChutesFactory(GameObject[] boardPositions)
    {
        for (int i = 0; i < amountOfChutes; i++)
        {
            GameObject firstChute = Instantiate(baseChute, new Vector3(0, 0, 0), Quaternion.identity);
            firstChute.transform.eulerAngles = new Vector3(firstChute.transform.eulerAngles.x + 60, firstChute.transform.eulerAngles.y, firstChute.transform.eulerAngles.z);

            GameObject otherChute = Instantiate(baseOtherChute, new Vector3(0, 0, 0), Quaternion.identity);

            firstChute.GetComponent<Chutes>().otherChute = otherChute;
            switch (i)
            {
                case 0: 
                    firstChute.GetComponent<Chutes>().position = Random.Range(13, 24);
                    if (firstChute.GetComponent<Chutes>().position >= 13)
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(7, 13);
                    else if(firstChute.GetComponent<Chutes>().position > 13 && firstChute.GetComponent<Chutes>().position < 18) {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(12, 17);

                    }
                    else if (firstChute.GetComponent<Chutes>().position >= 18 && firstChute.GetComponent<Chutes>().position < 24)
                    {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(17, 23);

                    }
                    break;
                case 1:
                    firstChute.GetComponent<Chutes>().position = Random.Range(22, 33);
                    if (firstChute.GetComponent<Chutes>().position >= 22)
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(7, 13);
                    else if (firstChute.GetComponent<Chutes>().position > 22 && firstChute.GetComponent<Chutes>().position < 27)
                    {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(12, 17);
                    }
                    else if (firstChute.GetComponent<Chutes>().position >= 27 && firstChute.GetComponent<Chutes>().position < 33)
                    {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(17, 23);
                    }
                    break;
                case 2:
                    firstChute.GetComponent<Chutes>().position = Random.Range(32, 48);
                    if (firstChute.GetComponent<Chutes>().position >= 32)
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(7, 13);
                    else if (firstChute.GetComponent<Chutes>().position > 32 && firstChute.GetComponent<Chutes>().position < 41)
                    {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(12, 17);
                    }
                    else if (firstChute.GetComponent<Chutes>().position >= 41 && firstChute.GetComponent<Chutes>().position < 48)
                    {
                        otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position - Random.Range(17, 23);
                    }
                    break;
                case 3:
                    firstChute.GetComponent<Chutes>().position = Random.Range(62, 77);
                    otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position + Random.Range(8, 19);
                    break;
                case 4:
                    firstChute.GetComponent<Chutes>().position = Random.Range(0, 80);
                    otherChute.GetComponent<OtherChutes>().otherPosition = firstChute.GetComponent<Chutes>().position + Random.Range(8, 19);
                    break;
            }
            ChuteToCreate[i] = firstChute;
            OtherChuteToCreate[i] = otherChute;
            ChuteToCreate[i].transform.position = boardPositions[firstChute.GetComponent<Chutes>().position].transform.position;
            ChuteToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
            OtherChuteToCreate[i].transform.position = boardPositions[otherChute.GetComponent<OtherChutes>().otherPosition].transform.position;
            OtherChuteToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
        }
    }

    void WormHolesFactory(GameObject[] boardPositions1, GameObject[] boardPositions2) {


        for (int i = 0; i < amountOfWormHoles; i++)
        {
            GameObject firstWormHole = Instantiate(baseWormHole, new Vector3(0, 0, 0), Quaternion.identity);


            GameObject otherWormHole = Instantiate(baseWormHole, new Vector3(0, 0, 0), Quaternion.identity);

            firstWormHole.GetComponent<WormHole>().otherWormHole = otherWormHole;
            switch (i)
            {
                case 0:
                    firstWormHole.GetComponent<WormHole>().position = Random.Range(24,39);

                    break;
                case 1:
                    firstWormHole.GetComponent<WormHole>().position = Random.Range(40, 55);
                    break;
                case 2:
                    firstWormHole.GetComponent<WormHole>().position = Random.Range(56, 76);
                    break;
                case 3:
                    firstWormHole.GetComponent<WormHole>().position = Random.Range(77, 88);
                    break;
                case 4:
                    firstWormHole.GetComponent<WormHole>().position = Random.Range(89, 99);
                    break;
            }
            otherWormHole.GetComponent<WormHole>().position = firstWormHole.GetComponent<WormHole>().position;
            otherWormHole.GetComponent<WormHole>().otherPosition = firstWormHole.GetComponent<WormHole>().position;
            firstWormHole.GetComponent<WormHole>().otherPosition = otherWormHole.GetComponent<WormHole>().position;
            firstWormHole.GetComponent<WormHole>().otherWormHole = otherWormHole;
            otherWormHole.GetComponent<WormHole>().otherWormHole = firstWormHole;
            wormHoleToCreate[i] = firstWormHole;
            otherWormHoleToCreate[i] = otherWormHole;
            wormHoleToCreate[i].transform.position = boardPositions1[firstWormHole.GetComponent<WormHole>().position].transform.position;
            wormHoleToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
            otherWormHoleToCreate[i].transform.position = boardPositions2[otherWormHole.GetComponent<WormHole>().otherPosition].transform.position;
            otherWormHoleToCreate[i].transform.position -= new Vector3(0, 0.2f, 0);
        }



    }


    // Start is called before the first frame update
    void Start()
    {
        WormHolesFactory(board1Postions, board2Postions);
        ChutesFactory(board1Postions);
        ChutesFactory(board2Postions);
        LadderFactory(board1Postions);
        LadderFactory(board2Postions);  
        currentPlayerIndex = 0;
        GameEvent startTurnEvent = new GameEvent();
        startTurnEvent.eventType = GameEvent.Type.startTurn;
        startTurnEvent.playerIndex = currentPlayerIndex;
        HandleGameEvent(startTurnEvent);
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Space) && playerList[pl]currentPosition < 100 && !isInReverseBoard && isActive)
        {
            movementForward();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentPosition > -1 && isInReverseBoard && isActive)
        {
            movementBackWards();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isActive)
        {
            isActive = false;
            GameEvent gameEvent = new GameEvent();
            gameEvent.eventType = GameEvent.Type.endTurn;
            gameManager.HandleGameEvent(gameEvent);
        }*/

    }
}
