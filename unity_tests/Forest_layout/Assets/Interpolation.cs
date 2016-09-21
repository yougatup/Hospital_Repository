using UnityEngine;
using System.Collections;

//Gary Ng, 2015

public class Interpolation //Simple interpolation class that does splines
{
    public static Vector3 CatmullRom(Vector3 pVec1, Vector3 pVec2, Vector3 pVec3, Vector3 pVec4, float pT)
    {
        _mWorkingVec = pVec2 * 2;
        _mWorkingVec += (pVec1 * -1 + pVec3) * pT;
        _mWorkingVec += (2 * pVec1 - 5 * pVec2 + 4 * pVec3 - pVec4) * pT * pT;
        _mWorkingVec += (pVec1 * -1 + 3 * pVec2 - 3 * pVec3 + pVec4) * pT * pT * pT;
        _mWorkingVec *= 0.5f;

        return _mWorkingVec;
    }

    public static Vector3 CubicBezier(Vector3 pVec1, Vector3 pVec2, Vector3 pVec3, Vector3 pVec4, float pT)
    {
        _mWorkingVec = (1 - pT) * (1 - pT) * (1 - pT) * pVec1;
        _mWorkingVec += 3 * (1 - pT) * (1 - pT) * pT * pVec2;
        _mWorkingVec += 3 * (1 - pT) * pT * pT * pVec3;
        _mWorkingVec += pT * pT * pT * pVec4;

        return _mWorkingVec;
    }

    private static Vector3 _mWorkingVec; //Since this is static it is shared. May cause issues in threaded cases.
}
