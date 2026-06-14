using UnityEngine;

public static class SaveManager
{
    private const string UnlockedLevelKey = "unlocked_level";
    private const string LastLevelKey = "last_level";
    private const string LastCheckpointKey = "last_checkpoint";
    private const string FragmentPrefix = "fragment_";
    private const string FragmentCountKey = "fragment_count";
    private const int ExtendedEndingFragmentRequirement = 30;

    public static int UnlockedLevel => PlayerPrefs.GetInt(UnlockedLevelKey, 1);
    public static int LastLevel => PlayerPrefs.GetInt(LastLevelKey, 1);
    public static string LastCheckpointId => PlayerPrefs.GetString(LastCheckpointKey, string.Empty);
    public static int CollectedFragments => PlayerPrefs.GetInt(FragmentCountKey, 0);
    public static bool IsExtendedEndingUnlocked => CollectedFragments >= ExtendedEndingFragmentRequirement;

    public static void SaveCheckpoint(string checkpointId)
    {
        PlayerPrefs.SetString(LastCheckpointKey, checkpointId);
        PlayerPrefs.Save();
    }

    public static void SaveLevelProgress(int currentLevel, int unlockedLevel)
    {
        PlayerPrefs.SetInt(LastLevelKey, currentLevel);
        PlayerPrefs.SetInt(UnlockedLevelKey, Mathf.Max(UnlockedLevel, unlockedLevel));
        PlayerPrefs.SetString(LastCheckpointKey, string.Empty);
        PlayerPrefs.Save();
    }

    public static bool IsFragmentCollected(string fragmentId)
    {
        return PlayerPrefs.GetInt(FragmentPrefix + fragmentId, 0) == 1;
    }

    public static void CollectFragment(string fragmentId)
    {
        if (IsFragmentCollected(fragmentId))
        {
            return;
        }

        PlayerPrefs.SetInt(FragmentPrefix + fragmentId, 1);
        PlayerPrefs.SetInt(FragmentCountKey, CollectedFragments + 1);
        PlayerPrefs.Save();
    }

    public static void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
