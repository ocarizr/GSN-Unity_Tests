using UnityEngine;

public class testScript : MonoBehaviour
{
    /*
    public int[] solution(int M, int P, int[] C)
    {
        int change = M - P;
        int[] coinsToCustomer = new int[C.Length];

        for (int i = (C.Length - 1); i >= 0; i--)
        {
            coinsToCustomer[i] = change / C[i];

            change = change % C[i];
        }

        return coinsToCustomer;
    }
    

    private void Start()
    {
        int[] C = { 1, 5, 10, 25, 50 };
        int M = 1000;
        int P = 499;

        int[] R = solution(M, P, C);

        for (int i = 0; i < C.Length; i++)
        {
            Debug.Log(R[i]);
        }
    }
    */

    public int solution (string[] C)
    {
        int[] set = new int[52];
        foreach (string card in C)
        {
            switch(card)
            {
                case "A♣":
                    set[0]++;
                    break;
                case "A♦":
                    set[1]++;
                    break;
                case "A♥":
                    set[2]++;
                    break;
                case "A♠":
                    set[3]++;
                    break;
                case "2♣":
                    set[4]++;
                    break;
                case "2♦":
                    set[5]++;
                    break;
                case "2♥":
                    set[6]++;
                    break;
                case "2♠":
                    set[7]++;
                    break;
                case "3♣":
                    set[8]++;
                    break;
                case "3♦":
                    set[9]++;
                    break;
                case "3♥":
                    set[10]++;
                    break;
                case "3♠":
                    set[11]++;
                    break;
                case "4♣":
                    set[12]++;
                    break;
                case "4♦":
                    set[13]++;
                    break;
                case "4♥":
                    set[14]++;
                    break;
                case "4♠":
                    set[15]++;
                    break;
                case "5♣":
                    set[16]++;
                    break;
                case "5♦":
                    set[17]++;
                    break;
                case "5♥":
                    set[18]++;
                    break;
                case "5♠":
                    set[19]++;
                    break;
                case "6♣":
                    set[20]++;
                    break;
                case "6♦":
                    set[21]++;
                    break;
                case "6♥":
                    set[22]++;
                    break;
                case "6♠":
                    set[23]++;
                    break;
                case "7♣":
                    set[24]++;
                    break;
                case "7♦":
                    set[25]++;
                    break;
                case "7♥":
                    set[26]++;
                    break;
                case "7♠":
                    set[27]++;
                    break;
                case "8♣":
                    set[28]++;
                    break;
                case "8♦":
                    set[29]++;
                    break;
                case "8♥":
                    set[30]++;
                    break;
                case "8♠":
                    set[31]++;
                    break;
                case "9♣":
                    set[32]++;
                    break;
                case "9♦":
                    set[33]++;
                    break;
                case "9♥":
                    set[34]++;
                    break;
                case "9♠":
                    set[35]++;
                    break;
                case "10♣":
                    set[36]++;
                    break;
                case "10♦":
                    set[37]++;
                    break;
                case "10♥":
                    set[38]++;
                    break;
                case "10♠":
                    set[39]++;
                    break;
                case "J♣":
                    set[40]++;
                    break;
                case "J♦":
                    set[41]++;
                    break;
                case "J♥":
                    set[42]++;
                    break;
                case "J♠":
                    set[43]++;
                    break;
                case "Q♣":
                    set[44]++;
                    break;
                case "Q♦":
                    set[45]++;
                    break;
                case "Q♥":
                    set[46]++;
                    break;
                case "Q♠":
                    set[47]++;
                    break;
                case "K♣":
                    set[48]++;
                    break;
                case "K♦":
                    set[49]++;
                    break;
                case "K♥":
                    set[50]++;
                    break;
                case "K♠":
                    set[51]++;
                    break;
            }
        }

        int lessAmount = 1000000000;

        for (int i =0; i < set.Length; i++)
        {
            if (set[i] < lessAmount)
            {
                lessAmount = set[i];
            }
        }
        return lessAmount;
    }

    private void Start()
    {
        string[] C = new string[] { "Q♥", "J♥", "K♣", "2♣", "8♠", "Q♣", "6♦", "7♥", "2♠", "8♥", "3♣", "3♥", "7♦", "J♠", "A♦", "J♦", "10♠", "9♣", "A♥", "5♠", "K♥", "J♥", "10♣", "3♠", "A♣", "J♦", "8♦", "9♦", "2♥", "10♣", "7♣", "2♠", "10♦", "Q♦", "2♦", "A♦", "A♣", "K♠", "3♠", "A♠", "10♠", "4♥", "6♠", "J♣", "7♣", "2♥", "9♣", "10♥", "6♥", "8♠", "4♦", "K♥", "4♣", "10♥", "9♥", "Q♣", "K♦", "3♦", "8♣", "6♥", "4♥", "7♠", "5♦", "A♥", "6♣", "Q♥", "8♦", "3♦", "9♥", "J♣", "4♠", "Q♦", "5♣", "9♦", "Q♠", "4♣", "2♦", "9♠", "2♣", "6♠", "8♥", "6♦", "3♣", "8♣", "9♠", "7♠", "K♣", "7♦", "5♦", "Q♠", "A♠", "K♦", "10♦", "7♥", "5♥", "K♠", "4♦", "5♠", "6♣", "3♥", "J♠", "4♠", "5♥" };
        Debug.Log(solution(C));
    }
}
