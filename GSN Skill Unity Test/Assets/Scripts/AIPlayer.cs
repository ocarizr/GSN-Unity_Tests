using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public int
        vertical = 0,
        horizontal = 0,
        diagonalL = 0,
        diagonalR = 0;

    public bool verifyingPlayer = true;

    public bool placed;

    public void AIDecision()
    {
        placed = false;
        GameObject[] sl;
        int ID;
        int v;
        if (GameController.gc.PlayerPiece == 0)
        {
            sl = GameObject.FindGameObjectsWithTag("XPiece");
        }
        else
        {
            sl = GameObject.FindGameObjectsWithTag("OPiece");
        }
        List<PotentialPieces> potentialPieces = new List<PotentialPieces>();
        List<PotentialPieces> playerVictoryNextTurn = new List<PotentialPieces>();
        List<PotentialPieces> AIPieces = new List<PotentialPieces>();

        potentialPieces.Clear();
        playerVictoryNextTurn.Clear();
        AIPieces.Clear();

        // Verify All Player Pieces and possibilities
        foreach (GameObject go in sl)
        {
            vertical = 0;
            horizontal = 0;
            diagonalR = 0;
            diagonalL = 0;

            PotentialPieces pp = new PotentialPieces();
            ID = go.GetComponent<Slots>().ID;
            v = ((ID - 1) % 15);
            VerifyVertical(ID);
            if (vertical <= -3)
            {
                pp.ID = ID;
                pp.Ammount = vertical;
                pp.Direction = "vertical";
                potentialPieces.Add(pp);
            }
            VerifyHorizontal(ID, v);
            if (horizontal <= -3)
            {
                pp.ID = ID;
                pp.Ammount = horizontal;
                pp.Direction = "horizontal";
                potentialPieces.Add(pp);
            }
            VerifyDiagonals(ID, v);
            if (diagonalL <= -3)
            {
                pp.ID = ID;
                pp.Ammount = diagonalL;
                pp.Direction = "diagonalL";
                potentialPieces.Add(pp);
            }
            if (diagonalR <= -3)
            {
                pp.ID = ID;
                pp.Ammount = diagonalR;
                pp.Direction = "diagonalR";
                potentialPieces.Add(pp);
            }
        }

        // If the player can win in the next turn use this
        foreach (GameObject go in sl)
        {
            vertical = 0;
            horizontal = 0;
            diagonalR = 0;
            diagonalL = 0;

            PotentialPieces pp = new PotentialPieces();
            ID = go.GetComponent<Slots>().ID;
            v = ((ID - 1) % 15);
            VerifyVertical(ID);
            if (vertical < -3)
            {
                pp.ID = ID;
                pp.Ammount = vertical;
                pp.Direction = "vertical";
                playerVictoryNextTurn.Add(pp);
            }
            VerifyHorizontal(ID, v);
            if (horizontal < -3)
            {
                pp.ID = ID;
                pp.Ammount = horizontal;
                pp.Direction = "horizontal";
                playerVictoryNextTurn.Add(pp);
            }
            VerifyDiagonals(ID, v);
            if (diagonalL < -3)
            {
                pp.ID = ID;
                pp.Ammount = diagonalL;
                pp.Direction = "diagonalL";
                playerVictoryNextTurn.Add(pp);
            }
            if (diagonalR < -3)
            {
                pp.ID = ID;
                pp.Ammount = diagonalR;
                pp.Direction = "diagonalR";
                playerVictoryNextTurn.Add(pp);
            }
        }

        if (GameController.gc.PlayerPiece == 0)
        {
            sl = GameObject.FindGameObjectsWithTag("OPiece");
        }
        else
        {
            sl = GameObject.FindGameObjectsWithTag("XPiece");
        }

        // Verify All AI Pieces and Possibilities
        foreach (GameObject go in sl)
        {
            vertical = 0;
            horizontal = 0;
            diagonalR = 0;
            diagonalL = 0;

            PotentialPieces pp = new PotentialPieces();
            ID = go.GetComponent<Slots>().ID;
            v = ((ID - 1) % 15);
            VerifyVertical(ID);
            if (vertical == 4)
            {
                pp.ID = ID;
                pp.Ammount = vertical;
                pp.Direction = "vertical";
                AIPieces.Add(pp);
            }
            VerifyHorizontal(ID, v);
            if (horizontal == 4)
            {
                pp.ID = ID;
                pp.Ammount = horizontal;
                pp.Direction = "horizontal";
                AIPieces.Add(pp);
            }
            VerifyDiagonals(ID, v);
            if (diagonalL == 4)
            {
                pp.ID = ID;
                pp.Ammount = diagonalL;
                pp.Direction = "diagonalL";
                AIPieces.Add(pp);
            }
            if (diagonalR == 4)
            {
                pp.ID = ID;
                pp.Ammount = diagonalR;
                pp.Direction = "diagonalR";
                AIPieces.Add(pp);
            }
        }

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

    public override string ToString()
    {
        return $"{ID} has {Ammount} Pieces in {Direction}";
    }
}
