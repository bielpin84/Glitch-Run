using System.Collections.Generic;
using UnityEngine;

public class RecoveredFilesManager : MonoBehaviour
{
    private readonly int[] unlockThresholds = { 2, 6, 14, 22, 30 };

    public List<bool> GetUnlockedFilesStatus()
    {
        int currentFragments = SaveManager.CollectedFragments;

        List<bool> unlockedStatus = new List<bool>();

        foreach (int threshold in unlockThresholds)
        {
            unlockedStatus.Add(currentFragments >= threshold);
        }

        return unlockedStatus;
    }

    public bool IsFileUnlocked(int fileIndex)
    {
        if (fileIndex < 0 || fileIndex >= unlockThresholds.Length)
        {
            return false;
        }

        return SaveManager.CollectedFragments >= unlockThresholds[fileIndex];
    }

    public int GetUnlockedFilesCount()
    {
        int count = 0;

        foreach (int threshold in unlockThresholds)
        {
            if (SaveManager.CollectedFragments >= threshold)
            {
                count++;
            }
        }

        return count;
    }
}