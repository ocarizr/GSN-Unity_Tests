using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gc = null;
    public bool
        PlayerTurn = true,
        GameStart = false;
    public int
        PlayerPiece = 0,
        AIPiece = 1;

    public int
        Victory = 0,
        Lose = 0,
        Draw = 0,
        gameResult = 0;

    public GameObject EmptySlot;

    public GameObject[] UIElements;

    ConditionVerifier cv;

    private void Awake()
    {
        if (gc == null)
        {
            gc = this;
        } else if (gc != this)
        {
            Destroy(gameObject);
        }

        cv = GetComponent<ConditionVerifier>();

        if (PlayerPrefs.HasKey("Victory"))
        {
            Victory = PlayerPrefs.GetInt("Victory");
        }
        if (PlayerPrefs.HasKey("Loses"))
        {
            Lose = PlayerPrefs.GetInt("Loses");
        }
        if (PlayerPrefs.HasKey("Draw"))
        {
            Draw = PlayerPrefs.GetInt("Draw");
        }
    }

    public void AICondition()
    {
        cv.ResetValues();

        GameObject[] go;

        if (AIPiece == 0)
        {
            go = GameObject.FindGameObjectsWithTag("XPiece");
        }
        else
        {
            go = GameObject.FindGameObjectsWithTag("OPiece");
        }

        foreach (GameObject s in go)
        {
            int ID = s.GetComponent<Slots>().ID;
            int v = ((ID - 1) % 15);
            cv.VerifyVertical(ID);
            cv.VerifyHorizontal(ID, v);
            cv.VerifyDiagonals(ID, v);

            if (cv.Vertical == 5 || cv.Horizontal == 5 || cv.DiagonalL == 5 || cv.DiagonalR == 5)
            {
                GameFinish(1);
            }
            else
            {
                cv.ResetValues();
            }
        }
        if (GameStart)              // if don't win
        {
            SwitchTurn();
        }

        VerifyTableFull();
    }

    public void PlayerCondition()
    {
        cv.ResetValues();

        GameObject[] go;

        if (PlayerPiece == 0)
        {
            go = GameObject.FindGameObjectsWithTag("XPiece");
        }
        else
        {
            go = GameObject.FindGameObjectsWithTag("OPiece");
        }

        foreach (GameObject s in go)
        {
            int ID = s.GetComponent<Slots>().ID;
            int v = ((ID - 1) % 15);
            cv.VerifyVertical(ID);
            cv.VerifyHorizontal(ID, v);
            cv.VerifyDiagonals(ID, v);

            if (cv.Vertical == -5 || cv.Horizontal == -5 || cv.DiagonalL == -5 || cv.DiagonalR == -5)
            {
                GameFinish(2);
            }
            else
            {
                cv.ResetValues();
            }
        }

        if (GameStart)              // if don't win
        {
            SwitchTurn();
        }

        VerifyTableFull();
    }

    private void VerifyTableFull()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("EmptySlot");

        if (go == null)
        {
            GameFinish(0);
        }
    }

    private void SwitchTurn()
    {
        if (PlayerTurn == true)
        {
            PlayerTurn = false;
            FindObjectOfType<ActiveTurnTxtUpdate>().UpdateTurnText();
            StartCoroutine(AIMove());
        }
        else
        {
            PlayerTurn = true;
            FindObjectOfType<ActiveTurnTxtUpdate>().UpdateTurnText();
        }
    }

    public void PS_Button()
    {
        PlayerTurn = true;
        PlayerPiece = 0;
        AIPiece = 1;
        GameStart = true;
        UIElements[1].SetActive(true);
        UIElements[0].SetActive(false);
    }

    public void AIS_Button()
    {
        PlayerTurn = false;
        PlayerPiece = 1;
        AIPiece = 0;
        GameStart = true;
        UIElements[1].SetActive(true);
        UIElements[0].SetActive(false);
        StartCoroutine(AIMove());
    }

    public void GameFinish(int result)
    {
        UIElements[1].SetActive(false);
        UIElements[2].SetActive(true);
        GameStart = false;
        gameResult = result;
        switch (result)
        {
            case 0:
                Draw++;
                break;
            case 1:
                Lose++;
                break;
            case 2:
                Victory++;
                break;
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Victory", Victory);
        PlayerPrefs.SetInt("Loses", Lose);
        PlayerPrefs.SetInt("Draw", Draw);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    public IEnumerator AIMove()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AIPlayer>().AIDecision();
    }
}
