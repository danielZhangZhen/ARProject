using UnityEngine;
public class turnInterorOfCar : myTool.turn360 {
    void Start()
    {
        base.m_mSpeed = 2;
    }
    public void ResetXY()
    {
        base.m_deltX = 0;
        base.m_deltY = 0;
    }
}
