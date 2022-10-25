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
    [SerializeField]
    Transform _robotPlace;
    [SerializeField]
    Transform _boundaryPlace;

    //������ �¼��� �ǹ�.
    EBaseCondition _currentSituation = EBaseCondition.Peaceful;
    //���� ����ϴ� �׵θ��� ��Ÿ����. ->> ���� ����Ʈ�� �����ϰ� �̸� �ڵ����� Ž���� �����ϴ� �ý����� ����� ����,
    [Header("�������ٵ� �ϴ��� �ð�������� ������ ������� ��ġ�ؾ���")]
    [SerializeField]
    List<GameObject> _borderObjectList = new List<GameObject>();
    [SerializeField]
    List<Vector3> _borderPointList = new List<Vector3>();
    
    [SerializeField]
    RobotPathListDict _robotpathdic = new RobotPathListDict();

    public List<Vector3> BaseBorderPointList { get => _borderPointList; set => _borderPointList = value; }

    public void Start()
    {
        RefreshRobots();
        RefreshBoundary();
        CheckBoundary();
    }
    public void Update()
    {

    }

    void RefreshRobots()
    {
        _robotpathdic.Clear();
        for (int i = 0; i < _robotPlace.childCount; i++)
        {
            _robotpathdic.Add(_robotPlace.GetChild(i).GetComponent<VigilRobot>(), new List<Vector3>());
        }
    }

    void RefreshBoundary() 
    {
        _borderObjectList.Clear();
        for (int i = 0; i < _boundaryPlace.childCount; i++) 
        {
            _borderObjectList.Add(_boundaryPlace.GetChild(i).gameObject);
        }
    }


    //���������� �ܼ��� �ൿ �ݰ��� ��Ʈ�� �����ϱ����ؼ� �̷��� ���Ѵ�.
    void CheckBoundary()
    {
        //���� ��������� ������ ���� ������ ������ ���Ŀ��� �� ������ �������

        #region// ���� ��� �κ�
        //�׵θ��� ����Ѵ�.
        //�׵θ��� ����� ������ ������ �׵θ��� ������ ���� �� ������ ��ȸ�ϴ� �ý����� ������ ��.
        BaseBorderPointList.Add(_borderObjectList[0].transform.position);
        float totalDistance = 0;
        for (int i = 1; i < _borderObjectList.Count; i++)
        {
            BaseBorderPointList.Add(_borderObjectList[i].transform.position);
            totalDistance += Vector3.Distance(BaseBorderPointList[i], BaseBorderPointList[i - 1]);
        }
        BaseBorderPointList.Add(_borderObjectList[0].transform.position);

        totalDistance += Vector3.Distance(BaseBorderPointList[_borderObjectList.Count - 1], BaseBorderPointList[0]);

        //�� ���ǵ带 ���Ѵ�.
        float totalSpeed = 0;
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic)
            totalSpeed += _pair.Key.GetSpeed();
        #endregion


        #region

        //������ ���� ����� �� ����.
        float areaPercent = 0;
        //������ �׳� ù ������ ������ ���߿��� �߾����� �������� ���� �� �κ��� �߽����� ��� �� �Ұ��̴�.
        //

        //
        //Debug.Log(totalDistance);
        int currentPoint = 1;
        Vector3 StartPoint = BaseBorderPointList[0];
        // ����Ʈ�� §������ �� ����Ʈ����
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic)
        {
            areaPercent = _pair.Key.GetSpeed() / totalSpeed;
            areaPercent = totalDistance * areaPercent;
            _pair.Value.Clear();
            //���������� ���� ���� ���� ������ �����Ѵ�.

            int safe = 10;
            _pair.Value.Add(StartPoint);
            while (safe>0) 
            {
                safe--;
                //�������� ���� �� ������ �Ÿ��� ���
                Vector3 targetPoint = BaseBorderPointList[currentPoint];
                float distanceToNextDot = Vector3.Distance(StartPoint, targetPoint);
                //Debug.Log(currentPoint + " / " + BaseBorderPointList.Count + ":" + StartPoint +"\n "+ 
                //areaPercent + " / " + totalDistance + ":" + distanceToNextDot);

                //�Ÿ��� �����ϴ� ������ ũ�⺸�� ���� ���
                if (areaPercent <= distanceToNextDot)
                {
                    //���ϴ� ���̺��� ª�� ��Ȳ ���� ������ ��� �ش��ϴ� ��ŭ �̵������ָ� �ȴ�.
                    StartPoint -= Vector3.Normalize(StartPoint - targetPoint) * areaPercent;
                    _pair.Value.Add(StartPoint);
                    Debug.Log("Got it"+ StartPoint);
                    break;
                }
                else 
                {
                    Debug.Log("pass");
                    StartPoint = BaseBorderPointList[currentPoint];
                    currentPoint++;
                    _pair.Value.Add(StartPoint);
                }
                areaPercent -= distanceToNextDot;
                //���� ���� ���̰� �������� ��� ���� ���� �Ѿ��.
            }

            _pair.Key.SetDPathOnRobot(_pair.Value);
            _pair.Key.BeginWork();
            // �ѰŸ��� �κ��� �Ÿ��� ���� �̵��� �Žø� ���Ѵ�.
        }



        //DebugRayManager.ShowRoute(BaseBorderPointList, Color.red, 6);

        #endregion
        //�׸��� ����� ������ �ѹ��� ȣ���ؼ� ���������� �߰��� �߻��Ҽ��ִ� ���� ����.
        //RefresrobotMove();
    }

    void RefresrobotMove()
    {
        foreach (KeyValuePair<IRobotFunction, List<Vector3>> _pair in _robotpathdic)
            _pair.Key.SetDPathOnRobot(_pair.Value);
    }


}
