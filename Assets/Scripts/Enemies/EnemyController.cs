using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int randomIdleCase;
    private float cronometer;
    private Animator mAnimator;
    private CharacterController mCharacterController;
    private Quaternion angle;
    private float degree;

    public EnemySO enemyData;

    private GameObject target;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mCharacterController = GetComponent<CharacterController>();
        target = GameObject.Find("Player");
    }

    public void EnemyBehavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > enemyData.range)
        {
            EnemyWalk();  
        }
        else
        {
            EnemyFollowPlayer();
        }
    }

    public void EnemyWalk()
    {
        //caminar
        mAnimator.SetBool("run", false);
        cronometer += 1 * Time.deltaTime;
        if (cronometer >= 4)
        {
            randomIdleCase = Random.Range(0,2);
            cronometer = 0;
        }
        switch (randomIdleCase)
        {
            case 0:
                mAnimator.SetBool("walk", false);
                break;

            case 1:
                degree = Random.Range(0, 360);
                angle = Quaternion.Euler(0, degree, 0);
                randomIdleCase++;
                break;
            
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                //transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                mCharacterController.Move(transform.forward * 1 * Time.deltaTime);
                mAnimator.SetBool("walk", true);
                break;
        }
    }

    public void EnemyFollowPlayer()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
        mAnimator.SetBool("walk", false);
        mAnimator.SetBool("run", true);
        //transform.Translate(Vector3.forward * enemyData.speed * Time.deltaTime);
        mCharacterController.Move(transform.forward * enemyData.speed * Time.deltaTime);
    }

    private void Update() {
        EnemyBehavior();
    }
}
