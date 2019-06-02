using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig waveConfig;
    private int wayPointIndex = 0;
    private List<Transform> wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        PathingMovement();

    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void PathingMovement()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPos = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
