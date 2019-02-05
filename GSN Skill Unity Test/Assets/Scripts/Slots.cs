using UnityEngine;

public class Slots : MonoBehaviour
{
    public int ID;
    public int State; // 0 to empty, 1 to Player and 2 to AI;
    public GameObject[] gamePieces; // 0 to player Piece and 1 to AI Piece

    // Start is called before the first frame update
    private void Start()
    {
        if (State == 0)
        {
            ID = (int)(transform.position.x + 1 + (transform.parent.position.y * 15));
        }
    }

    private void Update()
    {
        if (GameController.gc.PlayerTurn && GameController.gc.GameStart)
        {
            if (State == 0 && Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x > (transform.position.x - 0.5f) && mousePos.x < (transform.position.x + 0.5f))
                {
                    if (mousePos.y > (transform.position.y - 0.5f) && mousePos.y < (transform.position.y + 0.5f))
                    {
                        gamePieces[GameController.gc.PlayerPiece].GetComponent<Slots>().State = 1;
                        PiecePlacement(GameController.gc.PlayerPiece);
                        GameController.gc.PlayerCondition();
                    }
                }
            }
        }
    }

    public void AIPiecePlace(int _ID)
    {
        if (State == 0)
        {
            if (ID == _ID)
            {
                gamePieces[GameController.gc.AIPiece].GetComponent<Slots>().State = 2;
                PiecePlacement(GameController.gc.AIPiece);
                GameController.gc.AICondition();
                FindObjectOfType<AIPlayer>().placed = true;
            }
        }
    }

    void PiecePlacement(int piece)
    {
        gamePieces[piece].GetComponent<Slots>().ID = ID;
        GameObject go = Instantiate(gamePieces[piece], transform.position, Quaternion.identity);
        go.transform.parent = FindObjectOfType<GameController>().transform;
        Destroy(gameObject);
    }

    public void Vertical(int _ID)
    {
        if (_ID - 15 == ID || _ID - 30 == ID || _ID + 15 == ID || _ID + 30 == ID || _ID == ID)
        {
            if (State == 2)
            {
                FindObjectOfType<ConditionVerifier>().Vertical++;
            }
            else if (State == 1)
            {
                FindObjectOfType<ConditionVerifier>().Vertical--;
            }
        }
    }

    public void Horizontal(int _ID)
    {
        if (_ID - 2 == ID || _ID - 1 == ID || _ID + 2 == ID || _ID + 1 == ID || _ID == ID)
        {
            if (State == 2)
            {
                FindObjectOfType<ConditionVerifier>().Horizontal++;
            }
            else if (State == 1)
            {
                FindObjectOfType<ConditionVerifier>().Horizontal--;
            }
        }
    }

    public void DiagonalR(int _ID)
    {
        if (_ID - 16 == ID || _ID - 32 == ID || _ID + 16 == ID || _ID + 32 == ID || _ID == ID)
        {
            if (State == 2)
            {
                FindObjectOfType<ConditionVerifier>().DiagonalR++;
            }
            else if (State == 1)
            {
                FindObjectOfType<ConditionVerifier>().DiagonalR--;
            }
        }
    }

    public void DiagonalL(int _ID)
    {
        if (_ID - 14 == ID || _ID - 28 == ID || _ID + 14 == ID || _ID + 28 == ID || _ID == ID)
        {
            if (State == 2)
            {
                FindObjectOfType<ConditionVerifier>().DiagonalL++;
            }
            else if (State == 1)
            {
                FindObjectOfType<ConditionVerifier>().DiagonalL--;
            }
        }
    }
}
