using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class UnitAI : MonoBehaviour
{
    public Animator anim;
    public GameObject currentTarget;
    public Transform rangedAttackLocation;
    public bool isAlive = true;
    public bool isActive = false;
    
    [SerializeField] private GameObject missile;
    [SerializeField] private ParticleSystem slashParticle;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private UnitData unitData;
    
    private Queue<GameObject> Targets;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private int _health;
    private int _attack;
    private float _attackSpeed;
    
    private static readonly int IsAlive = Animator.StringToHash("isAlive");
    private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

    private void Awake()
    {
        FindEnemies();
    }

    private void Start()
    {
        _navMeshAgent.stoppingDistance = unitData.attackRange;
        _health = unitData.health;
        _attack = unitData.attackDamage;
        _attackSpeed = unitData.attackSpeed;
        anim.SetFloat(AttackSpeed, _attackSpeed);
    }

    private void Update()
    {
        if (isActive)
        {
            Attack();
        }
    }

    private void SelectEnemy()
    {
        currentTarget = Targets.Dequeue();
    }

    public void Attack()
    {
        if (_navMeshAgent.pathPending) return;
        if (!(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) return;
        if (_navMeshAgent.hasPath && _navMeshAgent.velocity.sqrMagnitude != 0f) return;
        switch (unitData.unitClass)
        {
            case UnitData.ClassCategory.Melee:
                anim.SetBool("isActive",true);
                break;
            case UnitData.ClassCategory.Ranged:
                anim.SetBool("isActive",true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RangedAttack() // working as an animation  event
    {
        
        var nextMissile = Instantiate(missile, rangedAttackLocation.position, rangedAttackLocation.rotation);
        nextMissile.transform.DOJump(currentTarget.transform.position, 1f, 0, 1f).OnComplete((() =>
        {
            currentTarget.GetComponent<UnitAI>().TakeDamage(_attack);
        }));
        if (currentTarget.GetComponent<UnitAI>().isAlive == false)
        {
            SelectEnemy();
        }
    }

    private IEnumerator Death()
    {
        anim.SetBool(IsAlive,false);
        yield return new WaitForSeconds(1);
        deathParticle.Play();
        if (unitData.unitTeam == UnitData.TeamCategory.Enemy)
        {
            transform.parent.GetComponent<EnemyGroup>().deathCount++;
        }
    }

    public void MeleeAttack() // working as an animation  event
    {
        slashParticle.Play();
        currentTarget.GetComponent<UnitAI>().TakeDamage(_attack);
        if (currentTarget.GetComponent<UnitAI>().isAlive == false)
        {
            SelectEnemy();
        }
    }
    private void FindEnemies()
    {
        Targets = unitData.unitTeam switch
        {
            UnitData.TeamCategory.Enemy => new Queue<GameObject>(GameObject.FindGameObjectsWithTag("ArmyUnitMelee")
                .Concat(GameObject.FindGameObjectsWithTag("ArmyUnitRanged"))),
            UnitData.TeamCategory.Soldier => new Queue<GameObject>(GameObject.FindGameObjectsWithTag("EnemyMelee")
                .Concat(GameObject.FindGameObjectsWithTag("EnemyRanged"))),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            isAlive = false;
            StartCoroutine(Death());
        }
    }
}
