using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInstantiator : MonoBehaviour
{
    [SerializeField] BattleTypeManager[] availableBattles;

    [SerializeField] bool activateOnEnter;
    private bool inArea;

    [SerializeField] float timeBetweenBattles;
    private float battleCounter;

    [SerializeField] bool deactivateAfterStarting;

    private void Start()
    {
        battleCounter = Random.Range(timeBetweenBattles *0.5f, timeBetweenBattles * 1.5f);
    }

    private void Update()
    {
        if(inArea && !Player.instance.deactivateMovement)
        {
            if(Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0)
            {
                battleCounter -= Time.deltaTime;
            }
        }

        if(battleCounter <= 0)
        {
            battleCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
            StartCoroutine(StartBattleCoroutine());
        }
    }

    private IEnumerator StartBattleCoroutine()
    {
        MenuManager.Instance.FadeImage();
        
        GameManager.instance.battleIsActive = true;

        int selectedBattle = Random.Range(0,availableBattles.Length);

        BattleManager.instance.itemsReward = availableBattles[selectedBattle].rewardItems;
        BattleManager.instance.XPRewardAmount = availableBattles[selectedBattle].rewardXP;

        yield return new WaitForSeconds(1.5f);

        MenuManager.Instance.FadeOut();

        BattleManager.instance.StartBattle(availableBattles[selectedBattle].enemies);

        if(deactivateAfterStarting)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(activateOnEnter)
        {
            StartCoroutine(StartBattleCoroutine());
        }
        else
        {
            inArea = true;
        }
    }

}
