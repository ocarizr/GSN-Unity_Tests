using UnityEngine;
using UnityEngine.UI;

public class MainMenuTextUpdate : MonoBehaviour
{
    public enum TextType { Vic, Loss, Draw};

    public TextType textType;
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        switch(textType)
        {
            case TextType.Draw:
                txt.text = GameController.gc.Draw.ToString();
                break;
            case TextType.Loss:
                txt.text = GameController.gc.Lose.ToString();
                break;
            case TextType.Vic:
                txt.text = GameController.gc.Victory.ToString();
                break;
        }
    }
}
