using UnityEngine;
using UnityEngine.UI;

public class ActiveTurnTxtUpdate : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        UpdateTurnText();
    }

    // Update is called once per frame
    public void UpdateTurnText()
    {
        if (GameController.gc.PlayerTurn)
        {
            txt.text = "Player";
            txt.color = Color.green;
        }
        else
        {
            txt.text = "AI";
            txt.color = Color.red;
        }
    }
}
