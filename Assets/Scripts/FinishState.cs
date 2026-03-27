using System.Collections.Generic;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    public List<EnemyHitHandle> healthSystems = new List<EnemyHitHandle>();   // ← Initialize here

    public GameObject TriggerZone;

    public GameObject Guide;

    private void Awake()
    {
        TriggerZone.SetActive(false);
        Guide.SetActive(false);
        healthSystems.Clear();

        var found = FindObjectsOfType<EnemyHitHandle>(true);

        foreach (var slot in found)
        {
            if (!healthSystems.Contains(slot))
            {
                healthSystems.Add(slot);
            }
        }
    }

    private void Update()
    {
        if (healthSystems.Count == 0)
        {
            TriggerZone.SetActive(true);
            Guide.SetActive(true);
        }
        foreach (var healthSystem in healthSystems)
        {
            if (healthSystem.HealthSystem.IsDead)
            {
                healthSystems.Remove(healthSystem);
            }
        }
    }
}