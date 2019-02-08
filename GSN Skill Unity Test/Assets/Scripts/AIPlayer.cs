using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public bool placed;

    ConditionVerifier cv;

    public void AIDecision()
    {
        cv = GetComponent<ConditionVerifier>();
        placed = false;
        List<PotentialPieces> potentialPieces = new List<PotentialPieces>();
        List<PotentialPieces> playerVictoryNextTurn = new List<PotentialPieces>();
        List<PotentialPieces> AIPieces = new List<PotentialPieces>();

        potentialPieces.Clear();
        playerVictoryNextTurn.Clear();
        AIPieces.Clear();

        // Verify All Player Pieces and possibilities
        FillPossibilities(true, -3, potentialPieces);

        // If the player can win in the next turn use this
        FillPossibilities(true, -4, playerVictoryNextTurn);

        // If the AI can win in this turn use this as priority
        FillPossibilities(false, 4, AIPieces);

        if (AIPieces.Count > 0)
        {
            PlacePiece(AIPieces);
        }
        else
        {
            if (potentialPieces.Count > 0)
            {
                if (playerVictoryNextTurn.Count > 0)
                {
                    PlacePiece(playerVictoryNextTurn);
                }
                else
                {
                    PlacePiece(potentialPieces);
                }
            }
            else
            {
                while (!placed)
                {
                    GameObject[] sl;

                    if (GameController.gc.PlayerPiece == 0)
                    {
                        sl = GameObject.FindGameObjectsWithTag("OPiece");
                    }
                    else
                    {
                        sl = GameObject.FindGameObjectsWithTag("XPiece");
                    }

                    if (sl.Length > 0)
                    {
                        PutPiece(sl);
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", Random.Range(1, 225));
                    }
                }
            }
        }
    }

    private void FillPossibilities(bool player, int ammount, List<PotentialPieces> PotentialPieces)
    {
        GameObject[] sl;
        if (player)
        {
            if (GameController.gc.PlayerPiece == 0)
            {
                sl = GameObject.FindGameObjectsWithTag("XPiece");
            }
            else
            {
                sl = GameObject.FindGameObjectsWithTag("OPiece");
            }
        }
        else
        {
            if (GameController.gc.PlayerPiece == 0)
            {
                sl = GameObject.FindGameObjectsWithTag("OPiece");
            }
            else
            {
                sl = GameObject.FindGameObjectsWithTag("XPiece");
            }
        }

        // Verify All AI Pieces and Possibilities
        foreach (GameObject go in sl)
        {
            cv.ResetValues();

            PotentialPieces pp = new PotentialPieces();
            int ID = go.GetComponent<Slots>().ID;
            int v = ((ID - 1) % 15);
            cv.VerifyVertical(ID);
            if (cv.Vertical == ammount)
            {
                pp.ID = ID;
                pp.Ammount = cv.Vertical;
                pp.Direction = "vertical";
                PotentialPieces.Add(pp);
            }
            cv.VerifyHorizontal(ID, v);
            if (cv.Horizontal == ammount)
            {
                pp.ID = ID;
                pp.Ammount = cv.Horizontal;
                pp.Direction = "horizontal";
                PotentialPieces.Add(pp);
            }
            cv.VerifyDiagonals(ID, v);
            if (cv.DiagonalL == ammount)
            {
                pp.ID = ID;
                pp.Ammount = cv.DiagonalL;
                pp.Direction = "diagonalL";
                PotentialPieces.Add(pp);
            }
            if (cv.DiagonalR == ammount)
            {
                pp.ID = ID;
                pp.Ammount = cv.DiagonalR;
                pp.Direction = "diagonalR";
                PotentialPieces.Add(pp);
            }
        }
    }

    private void PutPiece(GameObject[] sl)
    {
        int piece = sl[Random.Range(0, sl.Length - 1)].GetComponent<Slots>().ID;
        int id = piece;
        int sort = Random.Range(0, 3);
        bool invert = false;
        switch (sort)
        {
            case 0:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 15;
                        if (id < piece - 30)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 15;
                        if (id > piece + 30)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 1:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id--;
                        if (id < piece - 2)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id++;
                        if (id > piece + 2)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 2:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 14;
                        if (id < piece - 28)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 14;
                        if (id > piece + 28)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 3:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 16;
                        if (id < piece - 32)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 16;
                        if (id > piece + 32)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
        }
    }

    private void PutPiece()
    {
        GameObject[] sl;

        if (GameController.gc.PlayerPiece == 0)
        {
            sl = GameObject.FindGameObjectsWithTag("OPiece");
        }
        else
        {
            sl = GameObject.FindGameObjectsWithTag("XPiece");
        }

        int piece = sl[Random.Range(0, sl.Length - 1)].GetComponent<Slots>().ID;
        int id = piece;
        int sort = Random.Range(0, 3);
        bool invert = false;
        switch (sort)
        {
            case 0:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 15;
                        if (id < piece - 30)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 15;
                        if (id > piece + 30)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 1:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id--;
                        if (id < piece - 2)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id++;
                        if (id > piece + 2)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 2:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 14;
                        if (id < piece - 28)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 14;
                        if (id > piece + 28)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
            case 3:
                while (!placed)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 16;
                        if (id < piece - 32)
                        {
                            invert = true;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 16;
                        if (id > piece + 32)
                        {
                            PutPiece(sl);
                            return;
                        }
                    }
                }
                break;
        }
    }

    public void PlacePiece(List<PotentialPieces> po)
    {
        bool invert = false;
        int sort;
        if (po.Count == 1)
        {
            sort = 0;
        }
        else
        {
            sort = Random.Range(0, po.Count - 1);
        }

        int id = po[sort].ID;

        if (po[sort].Direction == "vertical")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 30)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 15;
                        if (id < po[sort].ID - 30)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 15;
                    }
                }
                else
                {
                    PlacePiece(po, 1);
                    return;
                }
            }
        }
        else if (po[sort].Direction == "horizontal")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 2)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id--;
                        if (id < po[sort].ID - 2)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id++;
                    }
                }
                else
                {
                    PlacePiece(po, 1);
                    return;
                }
            }
        }
        else if (po[sort].Direction == "diagonalL")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 28)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 14;
                        if (id < po[sort].ID - 28)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 14;
                    }
                }
                else
                {
                    PlacePiece(po, 1);
                    return;
                }
            }
        }
        else if (po[sort].Direction == "diagonalR")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 32)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 16;
                        if (id < po[sort].ID - 32)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 16;
                    }
                }
                else
                {
                    PlacePiece(po, 1);
                    return;
                }
            }
        }
    }

    public void PlacePiece(List<PotentialPieces> po, int attemp)
    {
        bool invert = false;
        int sort;
        if (po.Count == 1)
        {
            sort = 0;
        }
        else
        {
            sort = Random.Range(0, po.Count - 1);
        }

        int id = po[sort].ID;

        if (po[sort].Direction == "vertical")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 30)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 15;
                        if (id < po[sort].ID - 30)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 15;
                    }
                }
                else
                {
                    if (po.Count > 2 && attemp <= po.Count)
                    {
                        PlacePiece(po, ++attemp);
                        return;
                    }
                    else
                    {
                        PutPiece();
                    }
                }
            }
        }
        else if (po[sort].Direction == "horizontal")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 2)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id--;
                        if (id < po[sort].ID - 2)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id++;
                    }
                }
                else
                {
                    if (po.Count > 2 && attemp <= po.Count)
                    {
                        PlacePiece(po, ++attemp);
                        return;
                    }
                    else
                    {
                        PutPiece();
                    }
                }
            }
        }
        else if (po[sort].Direction == "diagonalL")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 28)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 14;
                        if (id < po[sort].ID - 28)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 14;
                    }
                }
                else
                {
                    if (po.Count > 2 && attemp <= po.Count)
                    {
                        PlacePiece(po, ++attemp);
                        return;
                    }
                    else
                    {
                        PutPiece();
                    }
                }
            }
        }
        else if (po[sort].Direction == "diagonalR")
        {
            while (!placed)
            {
                if (id <= po[sort].ID + 32)
                {
                    if (!invert)
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id -= 16;
                        if (id < po[sort].ID - 32)
                        {
                            invert = true;
                            id = po[sort].ID;
                        }
                    }
                    else
                    {
                        BroadcastMessage("AIPiecePlace", id);
                        id += 16;
                    }
                }
                else
                {
                    if (po.Count > 2 && attemp <= po.Count)
                    {
                        PlacePiece(po, ++attemp);
                        return;
                    }
                    else
                    {
                        PutPiece();
                    }
                }
            }
        }
    }
}

public class PotentialPieces
{
    public int ID;
    public int Ammount;
    public string Direction;
}
