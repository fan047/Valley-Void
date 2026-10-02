using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PooledEnemy : MonoBehaviour
{
    private EnemyPool enemyPool;
    private EnemyType enemyType;
    private PlayableDirector director;
    private bool isReturning;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        // 对象池第一次创建敌机时，还没有调用 SetPool。
        if (enemyPool == null) return;

        isReturning = false;

        // 每次从池中取出，都让飞行 Timeline 从头开始。
        director.stopped -= OnTimelineStopped;
        director.Stop();
        director.time = 0;
        director.stopped += OnTimelineStopped;
        director.Play();
    }

    private void OnDisable()
    {
        if (director != null)
            director.stopped -= OnTimelineStopped;
    }

    public void SetPool(EnemyPool pool, EnemyType type)
    {
        enemyPool = pool;
        enemyType = type;
    }

    private void OnTimelineStopped(PlayableDirector stoppedDirector)
    {
        if (stoppedDirector == director)
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        // 防止“被击毁”和“Timeline 结束”同时回收同一架敌机。
        if (enemyPool == null || isReturning) return;

        isReturning = true;
        enemyPool.Release(gameObject, enemyType);
    }
}