using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Update = UnityEngine.PlayerLoop.Update;

public class AI : MonoBehaviour
{
    [SerializeField] public UnitData unitData;
    
    public Animator anim;
    private NavMeshAgent _navMeshAgent;
    private int _health;
    private int _attack;
    private float _attackSpeed;
    public Transform _currentEnemy;
    public GameObject missile;
    public ParticleSystem deathParticle;

    private UnitData.TeamCategory _team;
    private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
    private static readonly int IsAlive = Animator.StringToHash("isAlive");
    private static readonly int IsActive = Animator.StringToHash("isActive");
    
    private void Awake()
    {
        _team = unitData.unitTeam;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = unitData.health;
        _attack = unitData.attackDamage;
        _attackSpeed = unitData.attackSpeed;
        anim.SetFloat(AttackSpeed, _attackSpeed);
        anim.SetBool(IsActive, true);
    }

    private void Start()
    {
        FindEnemy(_team);
    }
    
    private void Update()
    {
        if (_currentEnemy != null)
        {
            _navMeshAgent.SetDestination(_currentEnemy.position);
            transform.LookAt(_currentEnemy);
            if (Vector3.Distance(transform.position, _currentEnemy.position) < unitData.attackRange)
            {
                anim.SetBool(IsActive,true);
                _navMeshAgent.velocity = Vector3.zero;
            } 
        }
    }

    private void Celebrit()
    {
        GameManager.Instance.SetGameState(GameState.Win);
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health > 0) return;
        StartCoroutine(Death());
    }

    private void FindEnemy(UnitData.TeamCategory team)  //finding nearest enemy;
    {
        Transform bestTarget = null;
        var closestDistanceSqr = Mathf.Infinity;
        var currentPosition = transform.position;

        switch (team)
        {
            case UnitData.TeamCategory.Ally:
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.transform).ToArray();
                if (enemies.Length == 0)
                {
                    Celebrit();
                    break;
                }
                foreach (var potentialTarget in enemies)
                {
                    var directionToTarget = potentialTarget.position - currentPosition;
                    var dSqrToTarget = directionToTarget.sqrMagnitude;
                    if(dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }
                }
                _currentEnemy=  bestTarget;
                break;
            }
            case UnitData.TeamCategory.Enemy:
            {
                var enemies = GameObject.FindGameObjectsWithTag("Ally").Select(enemy => enemy.transform).ToArray();
                if (enemies.Length == 0)
                {
                    Celebrit();
                    break;
                }
                foreach (var potentialTarget in enemies)
                {
                    var directionToTarget = potentialTarget.position - currentPosition;
                    var dSqrToTarget = directionToTarget.sqrMagnitude;
                    if(dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }
                }
                _currentEnemy= bestTarget;
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(team), team, null);
        }
    }
    public void MeleeAttack() // working as an animation  event
    {
        if (_currentEnemy.GetComponent<AI>()._health <0 || _currentEnemy == null) 
        {
            FindEnemy(_team);
        }
        _currentEnemy.gameObject.GetComponent<AI>().TakeDamage(_attack);
        
    }
    
    public void RangedAttack() // working as an animation  event
    {
        if (_currentEnemy.GetComponent<AI>()._health <0 || _currentEnemy == null) 
        {
            FindEnemy(_team);
        }
        Instantiate(missile, transform.position, transform.rotation).transform.DOJump(_currentEnemy.position, 1f, 0, 1f).OnComplete((() =>
        {
            _currentEnemy.GetComponent<AI>().TakeDamage(_attack);
        }));
    }
    
    private IEnumerator Death()
    {
        anim.SetBool(IsAlive,false);
        deathParticle.Play();
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
