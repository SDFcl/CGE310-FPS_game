using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Playables;

public class BossDieCutsceneOnly : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    public List<MonoBehaviour> OnActionDisable = new List<MonoBehaviour>();
    public GameObject AnimationHand;

    public EnemyHitHandle enemyHitHandle;

    private void Awake()
    {
        playableDirector.enabled = false;
    }


    void Update()
    {
        if (enemyHitHandle.HealthSystem.IsDead)
        {
            StartCoroutine(testStart());
        }
    }

    IEnumerator testStart()
    {
        yield return new WaitForSeconds(2f);
        ActionCutscene();
        playableDirector.enabled = true;
        yield return new WaitForSeconds((float)playableDirector.duration);
        CUTcutscene();
    }

    private void ActionCutscene()
    {
        foreach (var item in OnActionDisable)
        {
            item.enabled = false;
        }
        AnimationHand.SetActive(false);
    }

    private void CUTcutscene()
    {
        foreach (var item in OnActionDisable)
        {
            item.enabled = true;
        }
        AnimationHand.SetActive(true);
    }


}
