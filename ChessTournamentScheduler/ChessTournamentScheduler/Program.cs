using ChessTournamentScheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

//Parašykite programą, kuri sudarytų šachmatų varžybų tvarkaraštį.
//Varžybose dalyvauja n>1 žaidėjų, kurie poromis turi sužaisti po vieną kartą.
//Jei žaidėjų skaičius 2k+1, tai kiekvienas žaidėjas turi k partijų sužaisti balta ir k partijų juoda spalva
//(be to, kažkuris žaidėjas turi palaukti, kol kiti suloš vieną ratą).
//Jei žaidėjų skaičius 2k, tai pirmoji pusė žaidėjų sulošia k partijų balta ir k-1 partijų juoda spalva.

namespace ChessTournamentScheduler;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Kiek yra zaideju?");
        int playersNum = int.Parse(Console.ReadLine());
        var players = CreateList(playersNum);
        RotateThePlayers(players, playersNum);
        PrintStatistics(players);
    }

    private static List<Player> CreateList(int playersNum)
    {
        List<Player> tempPlayers = new List<Player>();

        for (int i = 0; i < playersNum; i++)
        {
            tempPlayers.Add(new Player
            {
                Number = i + 1,
                WhiteCount = 0,
                BlackCount = 0
            });
        }

        if (playersNum % 2 != 0)
            tempPlayers.Add(new Player
            {
                Number = 0,
                WhiteCount = 0,
                BlackCount = 0
            });

        return tempPlayers;
    }

    private static void PrintPlayers(List<Player> players, int playersNum)
    {
        for(int i = 0; i < players.Count / 2; i++)
        {
            var firstPlayer = players[i];
            var secondPlayer = players[players.Count - i - 1];

            if (firstPlayer.Number == 0)
            {
                Console.WriteLine($"{secondPlayer.Number} ilsisi");
                continue;
            }    
            if (secondPlayer.Number == 0)
            {
                Console.WriteLine($"{firstPlayer.Number} ilsisi");
                continue;
            }

            int k = playersNum / 2;
            int diff = (secondPlayer.Number - firstPlayer.Number + playersNum) % playersNum;

            Player whitePlayer;
            Player blackPlayer;

            if (playersNum % 2 != 0)
            {
                if (diff >= 1 && diff <= k)
                {
                    whitePlayer = firstPlayer;
                    blackPlayer = secondPlayer;
                }
                else
                {
                    whitePlayer = secondPlayer;
                    blackPlayer = firstPlayer;
                }
            }
            else
            {
                if (diff < k)
                {
                    whitePlayer = firstPlayer;
                    blackPlayer = secondPlayer;
                }
                else if (diff > k)
                {
                    whitePlayer = secondPlayer;
                    blackPlayer = firstPlayer;
                }
                else
                {
                    if (firstPlayer.Number <= k)
                    {
                        whitePlayer = firstPlayer;
                        blackPlayer = secondPlayer;
                    }
                    else
                    {
                        whitePlayer = secondPlayer;
                        blackPlayer = firstPlayer;
                    }
                }
            }

            whitePlayer.WhiteCount++;
            blackPlayer.BlackCount++;

            Console.WriteLine($"{whitePlayer.Number} Balti - {blackPlayer.Number} Juodi");
        }
    }
    private static void RotateThePlayers(List<Player> players, int playersNum)
    {
        for(int i = 0; i < players.Count - 1; i++)
        {
            Console.WriteLine($"----------RATAS NR.{i + 1}----------");
            PrintPlayers(players, playersNum);
            var lastPlayer = players[players.Count - 1];
            players.RemoveAt(players.Count - 1);
            players.Insert(1, lastPlayer);
        }
    }
    private static void PrintStatistics(List<Player> players)
    {
        foreach (var player in players)
        {
            if (player.Number == 0)
                continue;

            Console.WriteLine(
                $"Zaidejas {player.Number}: Balti {player.WhiteCount}, Juodi {player.BlackCount}"
            );
        }
    }
}