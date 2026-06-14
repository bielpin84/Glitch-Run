using UnityEngine;

public class MaterializationGlitch : MonoBehaviour
{
    [SerializeField] private MaterializationGroup activeGroup = MaterializationGroup.Blue;
    [SerializeField] private MaterializationPlatform[] platforms;

    public MaterializationGroup ActiveGroup => activeGroup;

    private void Start()
    {
        RefreshPlatforms();
    }

    private void Update()
    {
        if (!PauseManager.IsPaused && Input.GetKeyDown(KeyCode.Q))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        activeGroup = activeGroup == MaterializationGroup.Blue ? MaterializationGroup.Red : MaterializationGroup.Blue;
        RefreshPlatforms();
    }

    private void RefreshPlatforms()
    {
        foreach (MaterializationPlatform platform in platforms)
        {
            if (platform != null)
            {
                platform.SetActiveGroup(activeGroup);
            }
        }
    }
}
