
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyBaseState
{
    public EnemyPatrol(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    NavMeshAgent _agent;
    Transform _player;
    Transform _transform;
    bool _foundPlayer;
    float _distanceToPlayer;
    float _breakTime;
    float _timerCountDown;
    Vector3 _currentTarget;
    List<Vector3> _patrolPoints = new List<Vector3>();
    public override void EnterState()
    {
        _agent = _context.agent;
        _currentTarget = _context.transform.position;
        _transform = _context.transform;
        _player = _context.player;
        if (_context.destinationPoints.Length <= 0) Debug.LogWarning("Have no any patrol point");
        else
        {
            foreach (Transform point in _context.destinationPoints)
            {
                _patrolPoints.Add(point.position);
            }
        }
        SetNewDestination();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        _distanceToPlayer = Vector3.Distance(_player.position, _transform.position);
        _foundPlayer = _context.readyToFindPlayer && _distanceToPlayer <= _context.playerDetection ? true : false;
    
        if(_agent.remainingDistance <= 0.5f)
        {
            _context.readyToFindPlayer = true;
            if (FinishedResting()) SetNewDestination();
        }
    }
    public override void CheckSwitchState()
    {
        if (_foundPlayer) SwitchState(_factory.Chasing());
    }
    private bool FinishedResting()
    {
        _timerCountDown += Time.deltaTime;
        return _timerCountDown >= _breakTime;
    }
    private void SetNewDestination()
    {
        // random next step
        if (_patrolPoints.Count <= 0 || _timerCountDown <= _breakTime) return;

        int index = Random.Range(0, _patrolPoints.Count);
        if (_patrolPoints[index] == _currentTarget) SetNewDestination();
        else
        {
            _currentTarget = _patrolPoints[index];
        }
        // random breakTime at next destination
        _breakTime = Random.Range(0.5f, 2f);
        _timerCountDown = 0f;

        // set destination
        _agent.SetDestination(_currentTarget);
    }

}
