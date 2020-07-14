using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    //인스펙터 정의

    //배경오브젝트 Set를 정의
    public GameObject _BgSetObj;
    public GameObject _BgSetObj2;

    public GameObject _EnemySet1;
    public List<GameObject> _nowEnemyChild = new List<GameObject>();
    public int _EnemyInt = 0;
    public float _YPos;

    //이동속도 정의
    public float _moveSpeed;
    public float _moveSpeedMax;

    //얼만큼 이동했는지를 체크 하기 위한 변수 선언
    float _xPosMoveChk = 0f;
    float _xPosMoveChk2 = 0f;

    public float _xPosMoveChkVal1;
    public float _xPosMoveChkVal2;

    public float _timerLim;
    public float _timerForSpeed;

    //게임 스코어 표현하기 위한 부분
    public UILabel _Score;
    public int _GameScore;
    public int _GameScorePer;

    public GameObject _ResultUI;
    public UILabel _ResultPoint;

    void Start()
    {
        _Score.text = _GameScore.ToString();
    }
    
    void Update()
    {
        _timerForSpeed += Time.deltaTime;
        if(_timerForSpeed > _timerLim)
        {
            _timerForSpeed = 0;
            if(_moveSpeed < _moveSpeedMax)
            {
                _moveSpeed = _moveSpeed * 1.1f;
            }
        }
        _xPosMoveChk += _moveSpeed * Time.deltaTime;
        _xPosMoveChk2 += _moveSpeed * Time.deltaTime * 0.5f;

        _BgSetObj.transform.localPosition += new Vector3(_moveSpeed * -1f * Time.deltaTime * 0.5f, 0, 0);
        _BgSetObj2.transform.localPosition += new Vector3(_moveSpeed * -1f * Time.deltaTime, 0, 0);
        _EnemySet1.transform.localPosition += new Vector3(_moveSpeed * -1f * Time.deltaTime, 0, 0);
        //매 프레임마다 정해진 속력으로 좌측이동

        if(_xPosMoveChk > 960f)//누적 이동량이 960보다 크면 체크
        {
            _xPosMoveChk = 0; //누적량 저장 변수를 0으로 리셋
            _BgSetObj2.transform.localPosition = new Vector3(0, -360f, 0);
            //BgSetObj 좌표를 초기값으로 변경
            _GameScore += _GameScorePer;    //게임 점수 라벨에 표시
            _Score.text = _GameScore.ToString();
        }

        if (_xPosMoveChk2 > 960f)//누적 이동량이 960보다 크면 체크
        {
            _xPosMoveChk2 = 0; //누적량 저장 변수를 0으로 리셋
            _BgSetObj.transform.localPosition = new Vector3(960 * -1f, 0, 0);
            //BgSetObj 좌표를 초기값으로 변경
        }

        if(_EnemySet1.transform.localPosition.x < _xPosMoveChkVal1)
        {
            _xPosMoveChkVal1 -= _xPosMoveChkVal2;

            ResetChildSet();
            _EnemyInt++;
            if(_EnemyInt >4)
            {
                _EnemyInt = 0;
            }
        }
    }

    void SpeedLimChk()
    {
        _timerForSpeed += Time.deltaTime;

        if(_timerForSpeed > _timerLim)
        {
            _timerForSpeed = 0;
            if(_moveSpeed < _moveSpeedMax)
            {
                _moveSpeed = _moveSpeed * 1.05f;
            }
            else
            {
                _moveSpeed = _moveSpeedMax;
            }
        }
    }

    private void ResetChildSet()
    {
        _nowEnemyChild[_EnemyInt].transform.localPosition += new Vector3(1440f, 0, 0);

        switch (Random.Range(1,5))
        {
            case 1:
                _YPos = 145f;
                break;
            case 2:
                _YPos = 0f;
                break;
            case 3:
                _YPos = 300f;
                break;
            case 4:
                _YPos = -70f;
                break;
        }

        _nowEnemyChild[_EnemyInt].transform.localPosition = new Vector3
            (_nowEnemyChild[_EnemyInt].transform.localPosition.x, _YPos, _nowEnemyChild[_EnemyInt].transform.localPosition.z);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _ResultUI.SetActive(true);
        _ResultPoint.text = _GameScore.ToString("N0");
    }

    //다시하기 버튼 호출
    void Regame()
    {
        _ResultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
