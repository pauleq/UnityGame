using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public bool[] levelFinished;
    public int[] experience;
    public int[] stars;

    public GameSaveData()
    {
        levelFinished = new bool[5];
        experience = new int[5];
        stars = new int[5];

        for (int i = 0; i < 5; i++)
        {
            levelFinished[i] = false;
            experience[i] = 0;
            stars[i] = 0;
        }
    }

    public void UpdateSave(int finishedLevel, int exp, int levelStars)
    {
        levelFinished[finishedLevel] = true;
        experience[finishedLevel] = exp;
        stars[finishedLevel] = levelStars;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Game Save Data:");

        for (int i = 0; i < 5; i++)
        {
            sb.AppendLine($"Level {i + 1}: Finished={levelFinished[i]}, Experience={experience[i]}, Stars={stars[i]}");
        }

        return sb.ToString();
    }
}