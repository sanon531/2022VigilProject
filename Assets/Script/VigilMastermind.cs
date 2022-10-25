using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBaseCondition
{
    Peaceful = 0,
    Action_Manual = 1,
    Action_Improvised = 2
}


public class VigilMastermind : MonoBehaviour
{
    public static VigilMastermind _instance;
    //������ ���� ��� �¼��� ��ȭ�� �� �ִ� �κ��� ���� �������̽���
    List<IRobotFunction> _robotlist = new List<IRobotFunction>();
    //������ �¼��� �ǹ�.
    EBaseCondition _currentSituation = EBaseCondition.Peaceful;
    
    //���� ����ϴ� �׵θ��� ��Ÿ����. ->> ���� ����Ʈ�� �����ϰ� �̸� �ڵ����� Ž���� �����ϴ� �ý����� ����� ����,
    [Header("�������ٵ� �ϴ��� �ð�������� ������ ������� ��ġ�ؾ���")]
    [SerializeField]
    List<GameObject> _baseBorderObjectList = new List<GameObject>();
    List<Vector3> _baseBorderPointList = new List<Vector3>();
    [SerializeField]
    RobotPathListDict _robotpathdic = new RobotPathListDict();

    public void Start()
    {
        CheckBoundary();
    }
    public void Update()
    {
        
    }



    void CheckBoundary() 
    {
        //�׵θ��� ����Ѵ�.
        //�׵θ��� ����� ������ ������ �׵θ��� ������ ���� �� ������ ��ȸ�ϴ� �ý����� ������ ��.
        _baseBorderPointList.Add(_baseBorderObjectList[0].transform.position);
        float totalDistance = 0;
        for (int i = 1; i < _baseBorderObjectList.Count;i++) 
        {
            _baseBorderPointList.Add(_baseBorderObjectList[i].transform.position);
            totalDistance += Vector3.Distance(_baseBorderPointList[i], _baseBorderPointList[i - 1]);
        }
        totalDistance += Vector3.Distance(_baseBorderPointList[_baseBorderObjectList.Count-1], _baseBorderPointList[0]);

        //�� ���ǵ带 ���Ѵ�.
        float totalSpeed = 0;
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic)
            totalSpeed += _pair.Key.GetSpeed();

        //������ ���� ����� �� ����.
        float areaPercent = 0;
        Vector3 _latestPoint = new Vector3();
        //
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic) 
        {
            areaPercent = _pair.Key.GetSpeed()/ totalSpeed;
            areaPercent = totalDistance * areaPercent;
            // �ѰŸ��� �κ��� �Ÿ��� ���� �̵��� �Žø� ���Ѵ�.




        }



        DebugRayManager.ShowRoute(_baseBorderPointList, Color.red, 6);
        //�׸��� ����� ������ �ѹ��� ȣ���ؼ� ���������� �߰��� �߻��Ҽ��ִ� ���� ����.
        //RefresrobotMove();
    }

    void RefresrobotMove() 
    {
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic) 
            _pair.Key.SetDPathOnRobot(_pair.Value);
    }


}
