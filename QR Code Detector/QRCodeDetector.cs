using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Microsoft.MixedReality.QR;

using Microsoft.MixedReality.Toolkit.UI;
using QRTracking;
#if WINDOWS_UWP
using Windows.Perception.Spatial;
#endif
namespace QRCodeTracking
{

    public class QRCodeDetector: QRCodeListener
    {     
        //public string ExpectedQRCodeText;
        //public string ExpectedQRCodeText2;

        public GameObject qrCodePrefab;

        public GameObject PanelMesin;
        public GameObject PanelLokasi;
            
       
        public UnityEvent onQRCode;
        private Microsoft.MixedReality.QR.QRCode the_qrcode;
        
        public string ExpectedQRCodeText;
        public string Code1;
        public string Code2;
        public string Code3;
        public string Code4;
        public string Code5;
        public string Code6;
        public string Code7;
        public string Code8;
        public string Code9;
        public string Code10;
        public string Code11;
        public string Code12;
        public string Code13;
        public string Code14;

        public string CodeMC1;
        public string CodeMC2;
        public string CodeMC3;


        private Pose QRPose;
        private GameObject qrCodeObject = null;

#if WINDOWS_UWP
        private SpatialCoordinateSystem CoordinateSystem = null;
#endif

        // Use this for initialization
        public override void Start()
        {
            //Debug.Log("QRCodeDetector waiting for "+ Code1);
            base.Start();
        }

        public override void HandleQRCodeAdded(Microsoft.MixedReality.QR.QRCode qrCode)
        {
            // new QR code detected
            
            //Pembacaan mesin 
           if (ExpectedQRCodeText == qrCode.Data)
            {               
                
                PanelMesin.SetActive(true);
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "00");
                PlayerPrefs.SetString("MesinNama","Tes QR pake Link");
            }

            if (Code1 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "01");
                PlayerPrefs.SetString("MesinNama","MC 22 TOSHIBA 170T");
            }

            if (Code2 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "02");
                PlayerPrefs.SetString("MesinNama","MC 01 JSW 330T");
            }

            if (Code3 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "03");
                PlayerPrefs.SetString("MesinNama","MC 17 TOSHIBA 100T");
            }

            if (Code4 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "04");
                PlayerPrefs.SetString("MesinNama","MC 2 MITSHUBISHI 350T");
            }

            if (Code5 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "05");
                PlayerPrefs.SetString("MesinNama","MC 3 DCSJ 400 T");
            }

            if (Code6 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "06");
                PlayerPrefs.SetString("MesinNama","MC 4 TOSHIBA 170T");
            }
            
            if (Code7 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "07");
                PlayerPrefs.SetString("MesinNama","MC 6 ENGGEL 100T");
            }

            if (Code8 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "08");
                PlayerPrefs.SetString("MesinNama","MC 7 JSW 150T");
            }

            if (Code9 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "09");
                PlayerPrefs.SetString("MesinNama","MC 10 FANUC 150T");
            }

            if (Code10 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "10");
                PlayerPrefs.SetString("MesinNama","MC 11 FANUC 150T");
            }

            if (Code11 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "11");
                PlayerPrefs.SetString("MesinNama","MC 12 FANUC 100T");
            }

            if (Code12 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "12");
                PlayerPrefs.SetString("MesinNama","MC 13 BORCHE 150T");
            }

            if (Code13 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "13");
                PlayerPrefs.SetString("MesinNama","MC 17 FANUC 50T");
            }

            if (Code14 == qrCode.Data)
            {
                PanelMesin.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("MesinID", "14");
                PlayerPrefs.SetString("MesinNama","MC 20 FANUC 100T");
            }

            //Pembacaan Scan Lokasi
            if (CodeMC1 == qrCode.Data)
            {
                PanelLokasi.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("LokasiID", "01");
                PlayerPrefs.SetString("LokasiNama","Warehouse Production 1");
            }

            if (CodeMC2 == qrCode.Data)
            {
                PanelLokasi.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("LokasiID", "02");
                PlayerPrefs.SetString("LokasiNama","Warehouse Production 2");
            }

            if (CodeMC3 == qrCode.Data)
            {
                PanelLokasi.SetActive(true);               
                the_qrcode = qrCode;
                onQRCode.Invoke();
                PlayerPrefs.SetString("LokasiID", "03");
                PlayerPrefs.SetString("LokasiNama","Warehouse Production 3");
            }




        }

        private bool GetPoseFromSpatialNode(System.Guid nodeId, out Pose pose)
        {
            
            bool found = false;
            pose = Pose.identity;

#if WINDOWS_UWP
                
                CoordinateSystem = Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateCoordinateSystemForNode(nodeId);
                

                if (CoordinateSystem != null)
                {
                    info.text += "\ngot coordinate";
                    Quaternion rotation = Quaternion.identity;
                    Vector3 translation = new Vector3(0.0f, 0.0f, 0.0f);

                    SpatialCoordinateSystem rootSpatialCoordinateSystem = (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(UnityEngine.XR.WSA.WorldManager.GetNativeISpatialCoordinateSystemPtr());

                    // Get the relative transform from the unity origin
                    System.Numerics.Matrix4x4? relativePose = CoordinateSystem.TryGetTransformTo(rootSpatialCoordinateSystem);

                    if (relativePose != null)
                    {
                        info.text += "\n got relative pose";
                        System.Numerics.Vector3 scale;
                        System.Numerics.Quaternion rotation1;
                        System.Numerics.Vector3 translation1;
       
                        System.Numerics.Matrix4x4 newMatrix = relativePose.Value;

                        // Platform coordinates are all right handed and unity uses left handed matrices. so we convert the matrix
                        // from rhs-rhs to lhs-lhs 
                        // Convert from right to left coordinate system
                        newMatrix.M13 = -newMatrix.M13;
                        newMatrix.M23 = -newMatrix.M23;
                        newMatrix.M43 = -newMatrix.M43;

                        newMatrix.M31 = -newMatrix.M31;
                        newMatrix.M32 = -newMatrix.M32;
                        newMatrix.M34 = -newMatrix.M34;

                        System.Numerics.Matrix4x4.Decompose(newMatrix, out scale, out rotation1, out translation1);
                        translation = new Vector3(translation1.X, translation1.Y, translation1.Z);
                        rotation = new Quaternion(rotation1.X, rotation1.Y, rotation1.Z, rotation1.W);
                        pose = new Pose(translation, rotation);
                        found = true;
                      

                        // can be used later using gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
                        //Debug.Log("Id= " + id + " QRPose = " +  pose.position.ToString("F7") + " QRRot = "  +  pose.rotation.ToString("F7"));
                    } else {
                          info.text += "\nrelative pos NULL";
                    }
                } else {
                  info.text += "\ncannot retrieve coordinate";
                }
                
#endif
            return found;
            
        }

        void Update()
        {
            base.Update();
            if ((the_qrcode != null) && (qrCodeObject == null) && (qrCodePrefab != null))
            {  
                
                var found  = GetPoseFromSpatialNode(the_qrcode.SpatialGraphNodeId, out QRPose);
                if (found)
                {
                    qrCodeObject = Instantiate(qrCodePrefab, QRPose.position, QRPose.rotation);
                    info.text += " QRPose = " + QRPose.position.ToString("F7") + " QRRot = " + QRPose.rotation.ToString("F7");
                }
                
            }
        }   

    }


}
