using UnityEngine;
using UnityEngine.UI;

public class EndGameTXT : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    private void Start()
    {
        txt = GetComponent<Text>();

        switch(GameController.gc.gameResult)
        {
            case 0:
                txt.color = Color.yellow;
                txt.text = "Game Draw!";
                break;
            case 1:
                txt.color = Color.red;
                txt.text = "AI Wins!";
                break;
            case 2:
                txt.color = Color.white;
                txt.text = "Player Wins!";
                break;
        }   
    }
}
