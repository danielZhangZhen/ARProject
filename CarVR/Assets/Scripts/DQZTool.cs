using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace myTool
{
    public abstract class MouseFollowRotation : MonoBehaviour
    {
        public Transform target;
        public float xSpeed = 250, ySpeed = 120, mSpeed = 10, SpinSpeed = .004f;
        public float yMinLimit = -50, yMaxLimit = 50;
        public float distance = 10, minDistance = 10, maxDistance = 20, ZoomSpeed = 5;
        private Quaternion rotation;
        private Vector3 position;
        public bool needDamping = true;
        public float damping = 8.0f;
        public float x = 0.0f;
        public float y = 0.0f;
        private Vector2 oldPosition1, oldPosition2;

        private bool isCamAutoRot = true;

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }
        void LateUpdate()
        {



            if (Application.isEditor)
            {
                testUse();
            }
            else
            {
                androidUse();
            }

        }
        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
        private void androidUse()
        {
            if (target)
            {
                if (Input.touchCount > 0 )
                {
                    if( Input.GetTouch(0).phase == TouchPhase.Began){
                        if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            isCamAutoRot = false;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                        isCamAutoRot = true;
                    }
                }

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && isCamAutoRot)
                {
                    OnBackFunc();
                    x += Input.GetAxis("Mouse X") * xSpeed * SpinSpeed / 3;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * SpinSpeed / 3;
                    y = ClampAngle(y, yMinLimit, yMaxLimit);
                }
                else if (Input.touchCount == 2)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject())
                    {
                        Vector2 tempPosition1 = Input.GetTouch(0).position;
                        Vector2 tempPosition2 = Input.GetTouch(1).position;

                        if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                        {
                            if (distance > minDistance)
                                distance -= ZoomSpeed;
                        }
                        else
                        {
                            if (distance < maxDistance)
                                distance += ZoomSpeed;
                        }
                        oldPosition1 = tempPosition1;
                        oldPosition2 = tempPosition2;
                    }
                }

                rotation = Quaternion.Euler(y, x, 0.0f);
                Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
                position = rotation * disVector + target.position;

                //adjust the camera
                if (needDamping)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
                }
                else
                {
                    transform.rotation = rotation;
                    transform.position = position;
                }


            }
        }

        protected virtual void OnBackFunc()
        {

        }
        private bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
        {
            float leng1 = Vector2.Distance(oP1, oP2);
            float leng2 = Vector2.Distance(nP1, nP2);
            if (leng1 < leng2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void testUse()
        {
            if (target)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        isCamAutoRot = false;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isCamAutoRot = true;
                }
                //use the light button of mouse to rotate the camera
                if (Input.GetMouseButton(0) && isCamAutoRot/*!EventSystem.current.IsPointerOverGameObject()*/)
                {
                    
                    OnBackFunc();
                    x += Input.GetAxis("Mouse X") * xSpeed * SpinSpeed;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * SpinSpeed;
                    y = ClampAngle(y, yMinLimit, yMaxLimit);
                }

                distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
                Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * disVector + target.position;
                //adjust the camera
                if (needDamping)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
                }
                else
                {
                    transform.rotation = rotation;
                    transform.position = position;
                }
            }

        }

    }
    public abstract class showHideObjectsNoSetActive : MonoBehaviour
    {
        public GameObject[] allGameObj = null;
        private MeshRenderer[][] myMR;
        void Awake()
        {
            if (allGameObj.Length > 0)
            {
                myMR = new MeshRenderer[allGameObj.Length][];
                if (allGameObj.Length > 0)
                {
                    for (int i = 0; i < allGameObj.Length; i++)
                    {
                        myMR[i] = allGameObj[i].GetComponentsInChildren<MeshRenderer>();
                    }
                }
            }
        }
        protected virtual void Car_ShowHide(bool showCar)
        {
            if (showCar)
            {
                for (int i = 0; i < allGameObj.Length; i++)
                {
                    for (int j = 0; j < myMR[i].Length; j++)
                    {
                        myMR[i][j].enabled = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < allGameObj.Length; i++)
                {
                    for (int j = 0; j < myMR[i].Length; j++)
                    {
                        myMR[i][j].enabled = false;
                    }
                }
            }

        }
    }
    public abstract class manualGyroController : MonoBehaviour
    {
        private bool gyroEnabled = true;
        private const float lowPassFilterFactor = 0.2f;
        private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);
        private Quaternion cameraBase = Quaternion.identity;
        private Quaternion calibration = Quaternion.identity;
        private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
        private Quaternion baseOrientationRotationFix = Quaternion.identity;

        private Quaternion referanceRotation = Quaternion.identity;

        private float h, horizontalSpeed, testN = 1f;
        private Vector3 touchVec3;
        [SerializeField]
        private Transform myCameraHubPoint = null;
        protected void Start()
        {
            AttachGyro();
        }
        protected void Update()
        {
            if (!gyroEnabled)
                return;

            if (!myCameraHubPoint)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), lowPassFilterFactor);
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), lowPassFilterFactor);
                myCameraHubPoint.rotation = Quaternion.Euler(touchVec3);
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        h = Input.GetAxis("Mouse X");
                        if (h < 0)
                        {
                            horizontalSpeed = 2.4f;
                            testN += horizontalSpeed;
                        }
                        if (h > 0)
                        {
                            horizontalSpeed = 2.4f;
                            testN -= horizontalSpeed;
                        }
                        touchVec3 = new Vector3(1, testN, 1);
                    }
                }
            }

        }

        private void AttachGyro()
        {
            Input.gyro.enabled = true;
            gyroEnabled = true;
            ResetBaseOrientation();
            UpdateCalibration(true);
            UpdateCameraBaseRotation(true);
            RecalculateReferenceRotation();
        }
        private void UpdateCalibration(bool onlyHorizontal)
        {
            if (onlyHorizontal)
            {
                var fw = (Input.gyro.attitude) * (-Vector3.forward);
                fw.z = 0;
                if (fw == Vector3.zero)
                {
                    calibration = Quaternion.identity;
                }
                else
                {
                    calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
                }
            }
            else
            {
                calibration = Input.gyro.attitude;
            }
        }

        private void UpdateCameraBaseRotation(bool onlyHorizontal)
        {
            if (onlyHorizontal)
            {
                var fw = transform.forward;
                fw.y = 0;
                if (fw == Vector3.zero)
                {
                    cameraBase = Quaternion.identity;
                }
                else
                {
                    cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
                }
            }
            else
            {
                cameraBase = transform.rotation;
            }
        }
        private static Quaternion ConvertRotation(Quaternion q)
        {
            return new Quaternion(q.x, q.y, -q.z, -q.w);
        }
        private Quaternion GetRotFix()
        {
            return Quaternion.identity;
        }
        private void ResetBaseOrientation()
        {
            baseOrientationRotationFix = GetRotFix();
            baseOrientation = baseOrientationRotationFix * baseIdentity;
        }
        private void RecalculateReferenceRotation()
        {
            referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
        }


    }
    public abstract class autoRotating : MonoBehaviour
    {
        public MouseFollowRotation myMFR;
        private float PressUpPos, PressDownPos, Puss;
        private enum turnLiftRight { left, Rigth, stop };
        private turnLiftRight myTurn = turnLiftRight.left;

        private bool isCamAutoRot = true;
        void LateUpdate()
        {

            if (myTurn == turnLiftRight.left)
            {
                myMFR.x += Puss;
            }
            else if (myTurn == turnLiftRight.Rigth)
            {
                myMFR.x -= Puss;
            }
            else if (myTurn == turnLiftRight.stop)
            {
                myMFR.x = myMFR.x + 0;
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    isCamAutoRot = false;
                }
                Puss = 0f;
                PressDownPos = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isCamAutoRot)
                {
                    //Debug.Log(!EventSystem.current.IsPointerOverGameObject());
                    PressUpPos = Input.mousePosition.x;
                    float dis = Mathf.Abs(PressUpPos - PressDownPos);
                    if (dis < 100)
                    { myTurn = turnLiftRight.stop; }
                    else
                    {
                        float _dis = PressUpPos - PressDownPos;
                        Puss += Mathf.Abs(_dis / 1000);
                        if (_dis > 0)
                        {

                            myTurn = turnLiftRight.left;
                        }
                        else
                        {
                            myTurn = turnLiftRight.Rigth;
                        }
                    }
                }
                else
                {
                    isCamAutoRot = true;
                }
            }
#elif UNITY_IPHONE || UNITY_ANDROID
             if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    isCamAutoRot = false;
                }
                Puss = 0f;
                PressDownPos = Input.mousePosition.x;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (isCamAutoRot)
                {
                    //Debug.Log(!EventSystem.current.IsPointerOverGameObject());
                    PressUpPos = Input.mousePosition.x;
                    float dis = Mathf.Abs(PressUpPos - PressDownPos);
                    if (dis < 100)
                    { myTurn = turnLiftRight.stop; }
                    else
                    {
                        float _dis = PressUpPos - PressDownPos;
                        Puss += Mathf.Abs(_dis / 1000);
                        if (_dis > 0)
                        {

                            myTurn = turnLiftRight.left;
                        }
                        else
                        {
                            myTurn = turnLiftRight.Rigth;
                        }
                    }
                }
                else
                {
                    isCamAutoRot = true;
                }
            }
#endif

        }
    }
    public abstract class onClickFunction : MonoBehaviour
    {
        private static Vector2 canClickDistaceDown, canClickDistaceUp;
        private static float DistanceAtoB;
        void Start()
        {
            inIt();
        }
        private void inIt()
        {
            canClickDistaceDown = new Vector2(0, 0);
            canClickDistaceUp = new Vector2(Screen.width, Screen.height);
        }
        void OnMouseDown()
        {
            canClickDistaceDown = Input.mousePosition;
        }
        void OnMouseUp()
        {
            canClickDistaceUp = Input.mousePosition;
            DistanceAtoB = Vector2.Distance(canClickDistaceDown, canClickDistaceUp);
            if (DistanceAtoB < 10)
            {
                playMyFunction();
            }
            inIt();
        }
        protected virtual void playMyFunction()
        {

        }
    }
    public abstract class FOVofCamera : MonoBehaviour
    {
        [SerializeField]
        private float fieldOfView = 60;
        private Camera myCam;
        void Start()
        {
            myCam = this.GetComponent<Camera>();
            myCam.fieldOfView = fieldOfView * (16f / 9f) / ((float)myCam.pixelWidth / myCam.pixelHeight);
        }

    }
    public abstract class turn360 : MonoBehaviour
    {

        protected float m_deltX = 0f, m_deltY = 0f;
        protected float m_mSpeed = 5f, upAngle = 70, downAngle = -70;//需要重写的方法，分别是拖动速度，
        private Vector2 oldPosition1, oldPosition2;

        void Update()
        {
            //鼠标右键点下控制相机旋转;
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                m_deltX += Input.GetAxis("Mouse X") * m_mSpeed;
                m_deltY -= Input.GetAxis("Mouse Y") * m_mSpeed;
                m_deltX = ClampAngle(m_deltX, -360, 360);
                m_deltY = ClampAngle(m_deltY, downAngle, upAngle);
                this.transform.rotation = Quaternion.Euler(m_deltY, m_deltX, 0);
            }
        }
        //规划角度;
        static float ClampAngle(float angle, float minAngle, float maxAgnle)
        {
            if (angle <= -360)
                angle += 360;
            if (angle >= 360)
                angle -= 360;
            return Mathf.Clamp(angle, minAngle, maxAgnle);
        }
    }
    public abstract class touchWaitingForJoy : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameJoy = null;
        protected float waitingTime = .5f;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(waitingTime);
            for (int i = 0; i < gameJoy.Length; i++)
            {
                gameJoy[i].SetActive(true);
            }
        }
    }
}


