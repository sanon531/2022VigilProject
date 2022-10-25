using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRobotFunction
{
    public void SetDPathOnRobot(List<Vector3> _pathPoint);
    public float GetSpeed();
    public void BeginWork();

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
        //��ΰ� �ٲ�� �ִ� ��带 ���ؼ� �����Ѵ�.
        //�Ǵ� �ִܳ�尡 �ƴ� ��Ʈ�� �ϴ� ���� �븻 �� ã�ƴٰ� ���尡��� ���������� �����Ѵٴ� �͵� ������ ��.
        _pathList = _pathPoint;
    }
    public void BeginWork()
    {
        ChangeMovement(_pathList[currentNum]);
        DebugRayManager.ShowRoute(_pathList, _routeColor, disapeartime);

    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _pathList[currentNum]) < reachLength) 
        {
            currentNum++;
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
        //������ �״�� ���������� ��ġ�� ������ �����.
    }

    public float GetSpeed()
    {
        return speed;
    }

    [Header(" for Debug")]
    [SerializeField]
    Color _routeColor= Color.cyan;
    [SerializeField]
    float disapeartime= 3f;




    #endregion

}
