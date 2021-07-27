using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = positions[0].transform.position;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentPosition < 100 && !isInReverseBoard)
        {
            TurnIsFinished = false;
            int dice = Random.Range(1, 6);
            
            currentPosition += dice;

                this.transform.position = positions[currentPosition].transform.position;

            TurnIsFinished = true;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentPosition > -1 && isInReverseBoard)
        {
            TurnIsFinished = false;
            int dice = Random.Range(1, 6);
            currentPosition -= dice;
            this.transform.position = positions2[currentPosition].transform.position;
            TurnIsFinished = true;

        }
        

        
    }
}
