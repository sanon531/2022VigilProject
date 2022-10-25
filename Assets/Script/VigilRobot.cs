using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRobotFunction
{
    public void SetDPathOnRobot(List<Vector3> _pathPoint);
}


public class VigilRobot : MonoBehaviour , IRobotFunction
{
    [SerializeField]
    List<Vector3> _pathList;
    [SerializeField]
    int currentNum = 0;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float reachLength = 0.1f;
    [SerializeField]
    Rigidbody2D _rigidbody2D;
    [SerializeField]
    Dictionary<int, int> _ss;




    public void SetDPathOnRobot(List<Vector3> _pathPoint) 
    {
        //경로가 바뀌면 최단 노드를 향해서 가야한다.
        //또는 최단노드가 아닌 패트롤 하는 점의 노말 값 찾아다가 가장가까운 벽면을향해 가야한다는 것도 가능할 듯.
        _pathList = _pathPoint;
    }
    private void Start()
    {
        ChangeMovement(_pathList[currentNum]);
        DebugRayManager.ShowRoute(_pathList, _routeColor, disapeartime);

    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _pathList[currentNum]) < reachLength) 
        {
            currentNum++;
            Debug.Log(currentNum + "s" + _pathList.Count);
            if (currentNum >= _pathList.Count)
                currentNum = 0;
            ChangeMovement(_pathList[currentNum]);
        }
    }



    #region // calling method
    void ChangeMovement(Vector3 _val) 
    {
        _val -= transform.position;

        _val = _val.normalized;
        _rigidbody2D.velocity = _val*speed;
        //방향은 그대로 하지만보는 위치의 변경을 만든다.
    }

    [Header(" for Debug")]
    [SerializeField]
    Color _routeColor= Color.cyan;
    [SerializeField]
    float disapeartime= 3f;




    #endregion

}
