using UnityEngine;

public class ConditionVerifier : MonoBehaviour
{
    public int Vertical = 0,
        Horizontal = 0,
        DiagonalL = 0,
        DiagonalR = 0;

    public void VerifyVertical(int ID)
    {
        if (ID > 30 && ID < 196)                        // Verify Vertical
        {
            BroadcastMessage("Vertical", ID);
        }
    }

    public void VerifyHorizontal(int ID, int v)
    {
        if (v > 2 && v < 13)                            // Verify Horizontal
        {
            BroadcastMessage("Horizontal", ID);
        }
    }

    public void VerifyDiagonals(int ID, int v)
    {
        if (ID > 30 && ID < 196 && v > 2 && v < 13)     // Verify Diagonal
        {
            BroadcastMessage("DiagonalR", ID);
            BroadcastMessage("DiagonalL", ID);
        }
    }

    public void ResetValues()
    {
        Vertical = 0;
        Horizontal = 0;
        DiagonalL = 0;
        DiagonalR = 0;
    }
}
