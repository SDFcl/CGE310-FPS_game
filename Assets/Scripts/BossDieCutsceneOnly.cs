using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms;

public class BossDieCutsceneOnly : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    public List<MonoBehaviour> OnActionDisable = new List<MonoBehaviour>();
    public GameObject AnimationHand;
    public GameObject TimeSytstem;
    public GameObject goBullet;

    public CanvasGroup PlayerHUD;
    public CanvasGroup Cutscene;

    public EnemyHitHandle enemyHitHandle;

    private bool onAction = false;

    private void Awake()
    {
        playableDirector.enabled = false;
        Cutscene.alpha = 0f;
    }


    void Update()
    {
        Debug.Log(enemyHitHandle.HealthSystem.IsDead);
        
        if (enemyHitHandle.HealthSystem.IsDead && !onAction)
        {
            Time.timeScale = 0.5f;
            TimeSytstem.SetActive(false);
            onAction = true;
            PlayerHUD.alpha = 0f;
            Cutscene.alpha = 1f;
            StartCoroutine(testStart());
        }
    }

    IEnumerator testStart()
    {
        ActionCutscene();
        playableDirector.enabled = true;
        Rigidbody rb = goBullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * -10f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;

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
