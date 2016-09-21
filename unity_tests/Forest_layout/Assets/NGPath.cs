using UnityEngine;
using System.Collections;

//Gary Ng, 2015
//Makes the object move on a path
//Add support for more stuff later



public class NGPath : MonoBehaviour
{
    public enum PathTypes
    {
        CATMUL_ROM,
        CUBIC_BEZIER
    };

    void Start()
    {
        if (mType == PathTypes.CATMUL_ROM || mType == PathTypes.CUBIC_BEZIER)
            if (mPathPoints.Length < 4) //Because we're only doing two types right now
                Debug.LogError("Error: Not enough points for path.");

        _mWorkingIndex = new int[4];
        _mWorkingIndex[0] = 0;
        _mWorkingIndex[1] = 1;
        _mWorkingIndex[2] = 2;
        _mWorkingIndex[3] = 3;
    }
    
    void Update()
    {
        if (!_mStop)
        {
            _mTime += mSpeed;

            if (mType == PathTypes.CATMUL_ROM)
                _mNext = Interpolation.CatmullRom((Vector3)mPathPoints[_mWorkingIndex[0]], (Vector3)mPathPoints[_mWorkingIndex[1]], (Vector3)mPathPoints[_mWorkingIndex[2]], (Vector3)mPathPoints[_mWorkingIndex[3]], _mTime);
            else if (mType == PathTypes.CUBIC_BEZIER)
                _mNext = Interpolation.CubicBezier((Vector3)mPathPoints[_mWorkingIndex[0]], (Vector3)mPathPoints[_mWorkingIndex[1]], (Vector3)mPathPoints[_mWorkingIndex[2]], (Vector3)mPathPoints[_mWorkingIndex[3]], _mTime);

            if (mFace) //If we need to face the way we're going..
            {
                this.gameObject.transform.LookAt(_mNext); //Look at it  
                this.gameObject.transform.localPosition = _mNext;
            }
            else if (mInverseFace) //For when the object faces the wrong way
            {
                _mWorkingVec = this.gameObject.transform.localPosition;
                this.gameObject.transform.localPosition = _mNext;
                this.gameObject.transform.LookAt(_mWorkingVec); //Look at it
            }
            else
            {
                this.gameObject.transform.localPosition = _mNext;
            }
            

            if (_mTime >= 1.0f)
            {
                _mTime = 0.0f;

                for (int i = 0; i < 4; ++i) //Update index
                {
                    ++_mWorkingIndex[i];

                    if (_mWorkingIndex[i] == mPathPoints.Length) //If we've reached the end
                    {
                        _mWorkingIndex[i] = 0;

                        if (!mInfinite) //If we're not infinite, just stop
                            _mStop = true;
                    }
                }
            }
        }
    }

    public PathTypes mType = PathTypes.CATMUL_ROM;
    public float mSpeed = 0.005f;
    public bool mInfinite = true;
    public bool mFace = false;
    public bool mInverseFace = false;
    public Vector3[] mPathPoints;

    private float _mTime = 0;    
    private int[] _mWorkingIndex; //Indexes for splines
    private bool _mStop = false;
    private Vector3 _mNext;
    private Vector3 _mWorkingVec;
}
