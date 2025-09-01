using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.DeviceSupport;
using System.Threading;
using static Crestron.SimplSharpPro.DM.Audio;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Sockets;
using System.Text;

namespace NFAHRooms
{


    public static class NVXHandler
    {
        static CTimer _timer;

        private static bool Student_AIOnVisState = false;
        private static bool Student_LayoutCtrOnVisState = false;
        private static bool Student_LayoutTopOnVisState = false;
        private static bool Student_LayoutFullOnVisState = false;
        private static bool Student_LayoutHeadOnVisState = false;
        private static bool Student_LayoutLeftOnVisState = false;
        private static bool Student_LayoutRightOnVisState = false;

        private static bool Teach_AIOnVisState = false;
        private static bool Teach_LayoutCtrOnVisState = false;
        private static bool Teach_LayoutTopOnVisState = false;
        private static bool Teach_LayoutFullOnVisState = false;
        private static bool Teach_LayoutHeadOnVisState = false;
        private static bool Teach_LayoutLeftOnVisState = false;
        private static bool Teach_LayoutRightOnVisState = false;


        private static void disp1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            ///
            ///btnPwrOff = 23,  //If power is on and you want to turn it off, it's this button
            ///btnPwrOn = 33,  //If power is off and you want to turn it on, it's this button
            ///btnPwrOnVis = 33 //If power is off this button should be visible
            ///

            //CrestronConsole.PrintLine("disp1 power on status {0}:", ControlSystem.disp1.Power.PowerOnFeedback.BoolValue);
            //CrestronConsole.PrintLine("disp1 power off status {0}:", ControlSystem.disp1.Power.PowerOffFeedback.BoolValue);
            //try
            //{
            //    if (ControlSystem.disp1.Power.PowerOnFeedback.BoolValue && !ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)  //Power On
            //    {
                    

            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
            //        //Mics.Mute("OFF");
                    
            //        if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
            //            ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
            //    }

            //    if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue && !ControlSystem.disp1.Power.PowerOnFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
            //        tp_ButtonStatus(((uint)NVXOutputs.out_Disp1).ToString(), ((uint)NVXInputs.in_Blank).ToString());
            //        //Mics.Mute("ON");
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler disp1_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler disp1_BaseEvent Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " NVXHandler disp1_BaseEvent Error", e.Message);
            //}

        }

        public static void tp_ButtonStatus(String Output, String Input)
        {
#if DEBUG
            CrestronConsole.PrintLine($"tp_ButtonStatus Output: {Output} Input: {Input}");
#endif
            switch (Output)
            {
                case "51":
                    //tp_ClearButtonStatus(((uint)NVXOutputs.out_Proj1).ToString());
                        if (Input != "0")
                        {
                            SetOutput(101);
                        }
                        
                        switch (Input)
                        {
                            case "1":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = true;
                                break;
                            case "2":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = true;
                                break;
                            case "3":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = true;
                                break;
                            case "4":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = true;
                                break;
                            case "5":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = true;
                                break;
                            case "8":
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = true;
                                break;
                            case "0":
                                //    using (TcpClient client = new TcpClient("172.16.1.238", ((int)SonyPort.ControlPort)))
                                //    {
                                //        string command = "power \"off\"";
                                //        using (NetworkStream stream = client.GetStream())
                                //        {
                                //            byte[] data = Encoding.ASCII.GetBytes(command);
                                //            stream.Write(data, 0, data.Length);
                                //        }
                                //        Thread.Sleep(500);
                                //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
                                //        Thread.Sleep(500);
                                //        Mics.Mute("on");
                                //ControlSystem.proj1.PowerOff();
                                //Thread.Sleep(500);
                                //ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                                //Mics.Mute("on");
                             break;
                        }
                        break;
                    
                
                
                        
                case "9":
                    tp_ClearButtonStatus(((uint)NVXOutputs.out_VTC).ToString());
                    
                    switch (Input)
                    {
                        case "1":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCPCOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCPC);
                            break;
                        case "2":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCEXTOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCEXT);
                            break;
                        case "3":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCDocCamOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCDocCam);
                            break;
                        case "4":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCAirMedia);
                            break;
                        case "5":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCAux);
                            break;
                        case "6":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCTeachCam);
                            break;
                        case "7":
                            ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = true;
                            ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCStudentCam);
                            break;
                            
                    }
                    break;
            }
        }
        private static void tp_ClearButtonStatus(String Output)
        {   
            switch (Output)
            {
                case "51":
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                    break;
                
                //case "10":
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCPCOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCEXTOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCDocCamOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = false;
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = false;
                //    break;
            }
            
        }
        private static void tp_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            
            try
            {
                if (currentDevice == ControlSystem.tp)
                {
                    switch (args.Sig.Type)
                    {
                        case eSigType.NA:
                            break;
                        case eSigType.Bool:
                            {
                                if (args.Sig.BoolValue)
                                {
                                    switch (args.Sig.Number)
                                    {
                                        case ((uint)Join.btn1_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn1_PCOff));
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = true;

                                                
                                                NVX.RouteNVX( ((uint)NVXInputs.in_PCMain).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn1_ExtDeskOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = true;

                                                

                                                NVX.RouteNVX(  ((uint)NVXInputs.in_PCExtDesk).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn1_DocCamOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = true;


                                                
                                                NVX.RouteNVX(  ((uint)NVXInputs.in_DocCam).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn1_AirMediaOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = true;
                                                
                                                NVX.RouteNVX(((uint)NVXInputs.in_AirMedia).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_AuxOff):
                                            {
                                                SetOutput((uint)Join.btn1_AuxOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = true;
                                                
                                                NVX.RouteNVX(((uint)NVXInputs.in_Aux).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn1_AuxOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = true;
                                                
                                                NVX.RouteNVX(((uint)NVXInputs.in_DS).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOn):  //Power On
                                            {
                                                if (RoomSetup.Display1 == "proj")
                                                {
                                                    //if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn1_PwrOn);
                                                }
                                                else if (RoomSetup.Display1 == "tv")
                                                {
                                                    //if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn1_PwrOn);
                                                }
                                                break;
                                            }
                                        case ((uint)Join.btn1_PwrOff):  //Power Off
                                            {
                                                if (RoomSetup.Display1 == "proj")
                                                {
                                                    //if (ControlSystem.proj1.PowerOnFeedback.BoolValue)
                                                    ControlSystem.proj1.PowerOff();

                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj1).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                else if (RoomSetup.Display1 == "tv")
                                                {

                                                    //if (ControlSystem.disp1.Power.PowerOnFeedback.BoolValue)
                                                    ControlSystem.disp1.Power.PowerOff();

                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj1).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                NVX.RouteNVX(((uint)NVXInputs.in_Blank).ToString() + "," + ((uint)NVXOutputs.out_Proj1).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn2_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn2_PCOff));
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = true;

                                                NVX.RouteNVX(  ((uint)NVXInputs.in_PCMain).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn2_ExtDeskOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_PCExtDesk).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn2_DocCamOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_DocCam).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn2_AirMediaOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_AirMedia).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;

                                            }
                                        case ((uint)Join.btn2_AuxOff):
                                            {

                                                SetOutput((uint)Join.btn2_AuxOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_Aux).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn2_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn2_DSPwrOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_DS).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn2_PwrOn):  //Power On
                                            {
                                                if (RoomSetup.Display2 == "proj")
                                                {
                                                    //if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn2_PwrOn);
                                                }
                                                else if (RoomSetup.Display2 == "tv")
                                                {
                                                    //if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn2_PwrOn);
                                                }
                                                //if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
                                                //    SetOutput((uint)Join.btn2_PwrOn);
                                                //if (RoomSetup.Display2 == "proj")
                                                //{
                                                //    if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
                                                //        SetOutput((uint)Join.btn2_PwrOn);
                                                //}
                                                //else if (RoomSetup.Display2 == "tv")
                                                //{
                                                //    if (ControlSystem.disp2.Power.PowerOffFeedback.BoolValue)
                                                //SetOutput((uint)Join.btn2_PwrOn);
                                                //}
                                                break;
                                            }
                                        case ((uint)Join.btn2_PwrOff):  //Power Off
                                            {
                                                if (RoomSetup.Display2 == "proj")
                                                {
                                                    //if (ControlSystem.proj2.PowerOnFeedback.BoolValue)
                                                    ControlSystem.proj2.PowerOff();
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj2).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                else if (RoomSetup.Display2 == "tv")
                                                {

                                                    //if (ControlSystem.disp2.Power.PowerOnFeedback.BoolValue)
                                                    ControlSystem.disp2.Power.PowerOff();
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj2).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                NVX.RouteNVX(((uint)NVXInputs.in_Blank).ToString() + "," + ((uint)NVXOutputs.out_Proj2).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_PCOff):
                                            {
                                                SetOutput(((uint)Join.btn3_PCOff));

                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = true;
                                                NVX.RouteNVX(((uint)NVXInputs.in_PCMain).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_ExtDeskOff):
                                            {
                                                SetOutput((uint)Join.btn3_ExtDeskOff);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_PCExtDesk).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_DocCamOff):
                                            {
                                                SetOutput((uint)Join.btn3_DocCamOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = true;
                                                NVX.RouteNVX(((uint)NVXInputs.in_DocCam).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                                
                                            }
                                        case ((uint)Join.btn3_AirMediaOff):
                                            {
                                                SetOutput((uint)Join.btn3_AirMediaOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = true;
                                                NVX.RouteNVX(((uint)NVXInputs.in_AirMedia).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_AuxOff):
                                            {
                                                SetOutput((uint)Join.btn3_AuxOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_Aux).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_DSPwrOff):
                                            {
                                                SetOutput((uint)Join.btn3_AuxOff);

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                

                                                ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = true;

                                                NVX.RouteNVX(((uint)NVXInputs.in_DS).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOn):  //Power On
                                            {
                                                if (RoomSetup.Display3 == "proj")
                                                {
                                                    //if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn3_PwrOn);
                                                }
                                                else if (RoomSetup.Display3 == "tv")
                                                {
                                                    //if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                                                    SetOutput((uint)Join.btn3_PwrOn);
                                                }
                                                //if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
                                                //    SetOutput((uint)Join.btn3_PwrOn);
                                                //break;
                                                //if (RoomSetup.Display3 == "proj")
                                                // {
                                                //if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
                                                //        SetOutput((uint)Join.btn3_PwrOn);
                                                //}
                                                //else if (RoomSetup.Display3 == "tv")
                                                //{
                                                //    if (ControlSystem.disp3.Power.PowerOffFeedback.BoolValue)
                                                //SetOutput((uint)Join.btn3_PwrOn);
                                                //}
                                                break;
                                            }
                                        case ((uint)Join.btn3_PwrOff):  //Power Off
                                            {
                                                if (RoomSetup.Display3 == "proj")
                                                {
                                                    //if (ControlSystem.proj3.PowerOnFeedback.BoolValue)
                                                    ControlSystem.proj3.PowerOff();
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj3).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                else if (RoomSetup.Display3 == "tv")
                                                {

                                                    //if (ControlSystem.disp3.Power.PowerOnFeedback.BoolValue)
                                                    ControlSystem.disp3.Power.PowerOff();
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PCOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_ExtDeskOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DocCamOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AirMediaOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_AuxOn)].BoolValue = false;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_DSPwrOn)].BoolValue = false;
                                                    //tp_ClearButtonStatus(((uint)EvertzOutputs.out_Proj3).ToString());
                                                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
                                                    Mics.Mute("On");
                                                }
                                                NVX.RouteNVX(((uint)NVXInputs.in_Blank).ToString() + "," + ((uint)NVXOutputs.out_Proj3).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCOpen):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.pgVTC)].BoolValue = true;
#if DEBUG
                                                CrestronConsole.PrintLine("VTC Page Opened");
#endif

                                                if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                                {
#if DEBUG
                                                    CrestronConsole.PrintLine("Student Cam Visible");
#endif
                                                    ControlSystem.tp.BooleanInput[((uint)Join.pgCam2AIVis)].BoolValue = true;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.pgCam1AIVis)].BoolValue = false;
                                                }
                                                
                                                if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                                {
#if DEBUG
                                                    CrestronConsole.PrintLine("Teach Cam Visible");
#endif
                                                    ControlSystem.tp.BooleanInput[((uint)Join.pgCam1AIVis)].BoolValue = true;
                                                    ControlSystem.tp.BooleanInput[((uint)Join.pgCam2AIVis)].BoolValue = false;
                                                }
                                                
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCClose):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.pgVTC)].BoolValue = false; 
                                                //ControlSystem.tp.BooleanInput[((uint)Join.pgAIVis)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.pgCam1AIVis)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.pgCam2AIVis)].BoolValue = false;
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCPCOff):
                                            {
                                                NVX.RouteNVX(((uint)NVXInputs.in_PCMain).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCEXTOff):
                                            {
                                                NVX.RouteNVX(((uint)NVXInputs.in_PCExtDesk).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCDocCamOff):
                                            {
                                                NVX.RouteNVX(((uint)NVXInputs.in_DocCam).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCAirMediaOff):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = true;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = false;
                                                ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCAirMedia);
                                                NVX.RouteNVX(((uint)NVXInputs.in_AirMedia).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCAuxOff):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = true;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = false;
                                                ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCAux);
                                                NVX.RouteNVX(((uint)NVXInputs.in_Aux).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCTeachCamOff):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = true;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = false;
                                                ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCTeachCam);
                                                NVX.RouteNVX(((uint)NVXInputs.in_TeachCam).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case ((uint)Join.btn_VTCStudentCamOff):
                                            {
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAirMediaOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCAuxOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = false;
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCStudentCamOn)].BoolValue = true;
                                                ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCStudentCam);
                                                NVX.RouteNVX(((uint)NVXInputs.in_StudentCam).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                                                break;
                                            }
                                        case (uint)Join.btn_Up:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveUP"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveUP"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_Down:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveDOWN"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveDOWN"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_Left:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LeftVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LeftVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_Right:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_RightVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_RightVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_UpRight:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveUPRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpRightVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveUPRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpRightVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_DownRight:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveDOWNRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownRightVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveDOWNRIGHT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownRightVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_UpLeft:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveUPLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpLeftVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveUPLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpLeftVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_DownLeft:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_MoveDOWNLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownLeftVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_MoveDOWNLEFT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownLeftVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_ZoomIn:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_ZoomIN"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomInVis)].BoolValue = true; 
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_ZoomIN"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomInVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_ZoomOut:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_ZoomOUT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomOutVis)].BoolValue = true;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_ZoomOUT"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomOutVis)].BoolValue = true;
                                            }
                                            break;
                                        case (uint)Join.btn_StudentCamControl:
                                            ControlSystem.tp.BooleanInput[((uint)Join.pgCam1AIVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.pgCam2AIVis)].BoolValue = true;
                                            Student_AIOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_AIOnVis)].BoolValue;
                                            Student_LayoutCtrOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue;
                                            Student_LayoutTopOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue;
                                            Student_LayoutFullOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutFullOnVis)].BoolValue;
                                            Student_LayoutHeadOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutHeadOnVis)].BoolValue;
                                            Student_LayoutLeftOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutLeftOnVis)].BoolValue;
                                            Student_LayoutRightOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutRightOnVis)].BoolValue;
                                            ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue = false;
                                            ControlSystem.tp.StringInput[((uint)Join.serial_Stream)].StringValue = Constants.rtsp + RoomSetup.SonyCameras.CommonProperties.StudentIP + Constants.rtspStream;
                                            break;
                                        case (uint)Join.btn_TeachCamControl:
                                            ControlSystem.tp.BooleanInput[((uint)Join.pgCam1AIVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.pgCam2AIVis)].BoolValue = false;
                                            Teach_AIOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_AIOnVis)].BoolValue;
                                            Teach_LayoutCtrOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue;
                                            Teach_LayoutTopOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue;
                                            Teach_LayoutFullOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutFullOnVis)].BoolValue;
                                            Teach_LayoutHeadOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutHeadOnVis)].BoolValue;
                                            Teach_LayoutLeftOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutLeftOnVis)].BoolValue;
                                            Teach_LayoutRightOnVisState = ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutRightOnVis)].BoolValue;
                                            ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue = true;
                                            ControlSystem.tp.StringInput[((uint)Join.serial_Stream)].StringValue = Constants.rtsp + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.rtspStream;
                                            break;
                                        case ((uint)Join.btn_TPreset1):
                                            _timer = new CTimer(Sony.TimerCallback, "TPS1", 5000);
                                            break;
                                        case ((uint)Join.btn_TPreset2):
                                            _timer = new CTimer(Sony.TimerCallback, "TPS2", 5000);
                                            break;
                                        case ((uint)Join.btn_TPreset3):
                                            _timer = new CTimer(Sony.TimerCallback, "TPS3", 5000);
                                            break;
                                        case ((uint)Join.btn_SPreset1):
                                            _timer = new CTimer(Sony.TimerCallback, "SPS1", 5000);
                                            break;
                                        case ((uint)Join.btn_SPreset2):
                                            _timer = new CTimer(Sony.TimerCallback, "SPS2", 5000);
                                            break;
                                        case ((uint)Join.btn_SPreset3):
                                            _timer = new CTimer(Sony.TimerCallback, "SPS3", 5000);
                                            break;
                                        case ((uint)Join.btn_AIOff):
                                                _ = Sony.SendRequest(Sony.urls["AI_ON"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_AIOnVis)].BoolValue = true;
                                                _ = Sony.SendRequest(Sony.urls["Lay_Ctr"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue = true;
                                                _ = Sony.SendRequest(Sony.urls["Lay_Top"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue = true;
                                                break;
                                        case ((uint)Join.btn_AIOn):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_AIOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutHeadOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn_LayoutCtrOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Ctr"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn_LayoutLeftOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Left"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutLeftOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn_LayoutRightOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Right"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutRightOnVis)].BoolValue = true;
                                            break;
                                        case ((uint)Join.btn_LayoutFullOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Full"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutFullOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutHeadOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn_LayoutTopOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Top"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutHeadOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn_LayoutHeadOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Head"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn_LayoutHeadOnVis)].BoolValue = true;
                                            break;

                                        case ((uint)Join.btn2_AIOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_ON2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_AIOnVis)].BoolValue = true;
                                            _ = Sony.SendRequest(Sony.urls["Lay_Ctr2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue = true;
                                            _ = Sony.SendRequest(Sony.urls["Lay_Top2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue = true;
                                            break;
                                        case ((uint)Join.btn2_AIOn):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_AIOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutHeadOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn2_LayoutCtrOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Ctr2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn2_LayoutLeftOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Left2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutLeftOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutRightOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn2_LayoutRightOff):
                                            _ = Sony.SendRequest(Sony.urls["Lay_Right2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutCtrOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutLeftOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutRightOnVis)].BoolValue = true;
                                            break;
                                        case ((uint)Join.btn2_LayoutFullOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Full2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutFullOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutHeadOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn2_LayoutTopOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Top2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue = true;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutHeadOnVis)].BoolValue = false;
                                            break;
                                        case ((uint)Join.btn2_LayoutHeadOff):
                                            _ = Sony.SendRequest(Sony.urls["AI_OFF2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["Lay_Head2"]);
                                            Thread.Sleep(1000);
                                            _ = Sony.SendRequest(Sony.urls["AI_ON2"]);
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutTopOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutFullOnVis)].BoolValue = false;
                                            ControlSystem.tp.BooleanInput[((uint)Join.btn2_LayoutHeadOnVis)].BoolValue = true;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                else if (!args.Sig.BoolValue)
                                {
                                    switch (args.Sig.Number)
                                    {
                                        case (uint)Join.btn_Up:

                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_Down:

                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_Left:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LeftVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_LeftVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_Right:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_RightVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_RightVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_UpRight:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpRightVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpRightVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_DownRight:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownRightVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownRightVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_UpLeft:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpLeftVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_UpLeftVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_DownLeft:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownLeftVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopPTZ"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_DownLeftVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_ZoomIn:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopZoom"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomInVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopZoom"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomInVis)].BoolValue = false;
                                            }
                                            break;
                                        case (uint)Join.btn_ZoomOut:
                                            if (ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Teach_StopZoom"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomOutVis)].BoolValue = false;
                                            }
                                            else if (ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue)
                                            {
                                                _ = Sony.SendRequest(Sony.urls["Student_StopZoom"]);
                                                ControlSystem.tp.BooleanInput[((uint)Join.btn_ZoomOutVis)].BoolValue = false;
                                            }
                                            break;
                                        case ((uint)Join.btn_TPreset1):
                                            
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Teach_Preset1"]);
                                            }
                                            break;
                                        case ((uint)Join.btn_TPreset2):
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Teach_Preset2"]);
                                            }
                                            break;
                                        case ((uint)Join.btn_TPreset3):
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Teach_Preset3"]);
                                            }
                                            break;
                                        case ((uint)Join.btn_SPreset1):
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Student_Preset1"]);
                                            }
                                            break;
                                        case ((uint)Join.btn_SPreset2):
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Student_Preset2"]);
                                            }
                                            break;
                                        case ((uint)Join.btn_SPreset3):
                                            if (_timer != null)
                                            {
                                                _timer.Stop();
                                                _timer = null;

                                                _ = Sony.SendRequest(Sony.urls["Student_Preset3"]);
                                            }
                                            break;
                                        default:
                                            break;
                                    } 
                                }

                                break;
                            }
                        case eSigType.UShort:
                            break;
                        case eSigType.String:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Notice($"NVXHandler Error:  {e.Message}");
                CrestronConsole.PrintLine($"NVXHandler Error:  {e.Message}");
                Email.SendEmail(RoomSetup.MailSubject + " TP_SigChange", e.Message);
            }
        }
        private static void SetOutput(uint OutputNum)
        {
            if (OutputNum > 100 && OutputNum < 200)
            {
                //if (RoomSetup.Display1 == "proj")
                //{
                //    using (TcpClient client = new TcpClient("172.16.1.238", ((int)SonyPort.ControlPort)))
                //    {
                //        //string command = "power \"on\"";
                //        string command = "power_status ?";
                //        using (NetworkStream stream = client.GetStream())
                //        {
                //            byte[] data = Encoding.ASCII.GetBytes(command);
                //            stream.Write(data, 0, data.Length);
                //            stream.Read(data, 0, data.Length);
                //            CrestronConsole.PrintLine("Sony Power Status {0}", Encoding.ASCII.GetString(data));
                //        }
                //        Thread.Sleep(500);
                //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
                //        Thread.Sleep(500);
                //        Mics.Mute("off");
                        //if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
                ControlSystem.disp1.Power.PowerOn();
                Thread.Sleep(500);
                ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
                Thread.Sleep(500);
                Mics.Mute("off");

                //if (!ControlSystem.proj1.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                //    ControlSystem.proj1.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
                //    }
                //}
                //else if (RoomSetup.Display1 == "tv")
                //{
                //    ControlSystem.disp1.Power.PowerOn();
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;

                //    //}

                //    //    if (ControlSystem.disp3.Video.Source.SourceSelect.UShortValue != 1)
                //    ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
                //CrestronConsole.PrintLine("Setoutput is tv");
                //CrestronConsole.PrintLine("Setoutput is tv power off feedback {0}", ControlSystem.disp1.Power.PowerOffFeedback.BoolValue);
                //if (ControlSystem.disp1.Power.PowerOffFeedback.BoolValue)
                //    ControlSystem.disp1.Power.PowerOn();

                //if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
                //    ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
                //}
            }
            if (OutputNum > 200 && OutputNum < 300)
            {
                //if (RoomSetup.Display2 == "proj")
                //{
                    //if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
                    ControlSystem.proj2.PowerOn();
                    Thread.Sleep(500);
                    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
                    Thread.Sleep(500);
                    Mics.Mute("off");
                    //using (TcpClient client = new TcpClient("172.16.1.237", ((int)SonyPort.ControlPort)))
                    //{
                    //    string command = "power \"on\"";
                    //    using (NetworkStream stream = client.GetStream())
                    //    {
                    //        byte[] data = Encoding.ASCII.GetBytes(command);
                    //        stream.Write(data, 0, data.Length);
                    //    }
                    //    Thread.Sleep(500);
                    //    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
                    //    Thread.Sleep(500);
                    //    Mics.Mute("off");



                    //if (!ControlSystem.proj2.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                    //ControlSystem.proj2.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
                //}
            //}
                //else if (RoomSetup.Display2 == "tv")
                //{
                //    ControlSystem.disp2.Power.PowerOn();
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;

                //    //}

                //    //    if (ControlSystem.disp3.Video.Source.SourceSelect.UShortValue != 1)
                //    ControlSystem.disp2.Video.Source.SourceSelect.UShortValue = 1;
                //    //if (ControlSystem.disp2.Power.PowerOffFeedback.BoolValue)
                //    //    ControlSystem.disp2.Power.PowerOn();



                //    //    if (ControlSystem.disp2.Video.Source.SourceSelect.UShortValue != 1)
                //    //    ControlSystem.disp2.Video.Source.SourceSelect.UShortValue = 1;
                //}
            }
            if (OutputNum > 300 && OutputNum < 400)
            {//CrestronConsole.PrintLine("SetOutpt {0}",OutputNum);
                //if (RoomSetup.Display3 == "proj")
                //{
                //  //  if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
                //        ControlSystem.proj3.PowerOn();
                //    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
                //    // if (!ControlSystem.proj3.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
                //    ControlSystem.proj3.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
                        
                //}
                //else if (RoomSetup.Display3 == "tv")
                //{
                    //CrestronConsole.PrintLine("Setoutput is tv");
                    //CrestronConsole.PrintLine("Setoutput is tv power off feedback {0}", ControlSystem.disp3.Power.PowerOffFeedback.BoolValue);
                    //if (((ushort)OutputNum) == ((ushort)Join.btn3_PCOff))
                    //    ControlSystem.disp3.Power.PowerOn();

                    //if (ControlSystem.disp3.Power.PowerOffFeedback.BoolValue)
                    //{
                    ControlSystem.disp3.Power.PowerOn();
                    Thread.Sleep(500);
                    ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
                    Thread.Sleep(500);
                    Mics.Mute("off");

                    //}

                    //    if (ControlSystem.disp3.Video.Source.SourceSelect.UShortValue != 1)
                    //ControlSystem.disp3.Video.Source.SourceSelect.UShortValue = 1;
                //}
            }

        }
        private static void tp_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    

                    if (Scheduling.errorCounts.TryGetValue("touchpanel", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["touchpanel"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }

                    ControlSystem.tp.ExtenderSystemReservedSigs.StandbyTimeout.UShortValue = RoomSetup.Touchpanel.StandbyTimeout;

                    if (RoomSetup.Touchpanel.ScreenSaver.ToLower() == "on" && RoomSetup.Touchpanel.StandbyTimeout != 0 && Scheduling.SS_Active)
                    {
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreenSaverImageUrl.StringValue = RoomSetup.Touchpanel.ImageUrl;

                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = false;
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = true;
                    }

                    if ((RoomSetup.Touchpanel.ScreenSaver.ToLower() == "off" && !Scheduling.SS_Active) || RoomSetup.Touchpanel.StandbyTimeout == 0)
                    {
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOn.BoolValue = false;
                        ControlSystem.tp.ExtenderScreenSaverReservedSigs.ScreensaverOff.BoolValue = true;
                    }   
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("touchpanel", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("TP_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("TP_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " TP_OnlineStatusChange", e.Message);
            }
        }
        

        private static void disp1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }

        private static void am3200_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("airmedia", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["airmedia"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("airmedia", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("AM3200_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("AM3200_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " AM3200_OnlineStatusChange", e.Message);
            }
        }
        private static void proj1_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("proj", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }
        private static void proj1_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.proj1.PowerOnFeedback.BoolValue && !Proj1On)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj1.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //        Mics.Mute("Off");
            //        Proj1On = true;
            //    }

            //    if (ControlSystem.proj1.PowerOffFeedback.BoolValue && Proj1On) //Power Off
            //    { 
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn1_PwrOnVis)].BoolValue = true;
            //        Proj1On = false;
            //        await Task.Delay(1000);
            //        if (ControlSystem.proj1.PowerOffFeedback.BoolValue)
            //        {
            //            NVX.RouteNVX($"{NVXOutputs.out_Proj1},{NVXInputs.in_Blank}");
            //            Mics.Mute("On");
            //        }
            //    }

            //    if (!ControlSystem.proj1.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
            //    {
            //        ControlSystem.proj1.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //    }   
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler proj1_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler proj1_BaseEvent Error  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " proj2_BaseEvent", e.Message);
            //}
        }
        private static void proj2_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("proj", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }
        private static void proj2_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.proj2.PowerOnFeedback.BoolValue && !Proj2On)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj2.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //        Mics.Mute("Off");
            //        Proj2On = true;
            //    }

            //    if (ControlSystem.proj2.PowerOffFeedback.BoolValue && Proj2On) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
            //        Proj2On = false;
            //        await Task.Delay(1000);
            //        if (ControlSystem.proj2.PowerOffFeedback.BoolValue)
            //        {
            //            NVX.RouteNVX($"{NVXOutputs.out_Proj2},{NVXInputs.in_Blank}");
            //            Mics.Mute("On");
            //        }
            //    }

            //    if (!ControlSystem.proj2.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
            //    {
            //        ControlSystem.proj2.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler proj2_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler proj2_BaseEvent Error  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " proj2_BaseEvent", e.Message);
            //}
        }
        private static void proj3_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("proj", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }
        private static void proj3_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        { return;
            //try
            //{
            //    if (ControlSystem.proj3.PowerOnFeedback.BoolValue && !Proj3On)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
            //        ControlSystem.proj3.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //        Mics.Mute("Off");
            //        Proj3On = true;
            //    }

            //    if (ControlSystem.proj3.PowerOffFeedback.BoolValue && Proj3On) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
            //        Proj3On = false;
            //        await Task.Delay(1000);
            //        if (ControlSystem.proj3.PowerOffFeedback.BoolValue)
            //        {
            //            NVX.RouteNVX($"{NVXOutputs.out_Proj3},{NVXInputs.in_Blank}");
            //            Mics.Mute("On");
            //        }
            //    }

            //    if (!ControlSystem.proj3.SourceSelectFeedbackSigs[((uint)SonyProjInputs.ProjHDMI)].BoolValue)
            //    {
            //        ControlSystem.proj3.SourceSelectSigs[((uint)SonyProjInputs.ProjHDMI)].Pulse();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler proj3_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler proj3_BaseEvent Error  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " proj3_BaseEvent", e.Message);
            //}
        }
        private static void disp2_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
                
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }
        private static void disp2_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {
            return;
            //try
            //{
            //    if (ControlSystem.disp2.Power.PowerOnFeedback.BoolValue && !ControlSystem.disp2.Power.PowerOffFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;
            //        Mics.Mute("OFF");

            //        if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
            //            ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
            //    }

            //    if (ControlSystem.disp2.Power.PowerOffFeedback.BoolValue && !ControlSystem.disp2.Power.PowerOnFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
            //        tp_ButtonStatus(((uint)NVXOutputs.out_Disp2).ToString(), ((uint)NVXInputs.in_Blank).ToString());
            //        Mics.Mute("ON");
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler disp2_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler disp2_BaseEvent Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " NVXHandler disp3_BaseEvent Error", e.Message);
            //}
            //try
            //{
            //    if (ControlSystem.disp2.Power.PowerOnFeedback.BoolValue && !ControlSystem.disp2.Power.PowerOffFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = false;

            //        if (ControlSystem.disp2.Video.Source.SourceSelect.UShortValue != 1)
            //            ControlSystem.disp2.Video.Source.SourceSelect.UShortValue = 1;

            //    }

            //    if (ControlSystem.disp2.Power.PowerOffFeedback.BoolValue && !ControlSystem.disp2.Power.PowerOnFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn2_PwrOnVis)].BoolValue = true;
            //        tp_ButtonStatus(((uint)NVXOutputs.out_Disp2).ToString(), ((uint)NVXInputs.in_Blank).ToString());

            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler disp2_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler disp2_BaseEvent Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " NVXHandler disp2_BaseEvent Error", e.Message);
            //}
        }
        private static void disp3_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            try
            {
             
                if (args.DeviceOnLine)
                {
                    if (Scheduling.errorCounts.TryGetValue("tv", out int x) && x > 0)
                    {
                        Scheduling.errorCounts["tv"] = 0;
                        Email.SendEmail(RoomSetup.MailSubject, $"{currentDevice.Name} is online {DateTime.Now}");
                    }
                }
                else if (!args.DeviceOnLine)
                {
                    Scheduling.Alert_Timer("tv", RoomSetup.Timeouts.ErrorCheckDelay, $"{currentDevice.Name} Offline at {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Display_OnlineStatusChange: {0}", e.Message);
                ErrorLog.Notice("Display_OnlineStatusChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Display_OnlineStatusChange", e.Message);
            }
        }
        private static void disp3_BaseEvent(GenericBase currentDevice, BaseEventArgs args)
        {//CrestronConsole.PrintLine("args: {0}", args.Index.ToString());
           
            //try
            //{
            //if (ControlSystem.disp3.Power.PowerOnFeedback.BoolValue) ;// && !ControlSystem.disp3.Power.PowerOffFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;
            //        //Mics.Mute("OFF");

            //        if (ControlSystem.disp1.Video.Source.SourceSelect.UShortValue != 1)
            //            ControlSystem.disp1.Video.Source.SourceSelect.UShortValue = 1;
            //    }
            //var x = ControlSystem.disp3.Power.PowerOnFeedback.UShortValue;
            //CrestronConsole.PrintLine("disp3 power on status {0}:", ControlSystem.disp3.Power.PowerOnFeedback.UShortValue);
            //if (ControlSystem.disp3.Power.PowerOffFeedback.UShortValue == 1) ;// && !ControlSystem.disp3.Power.PowerOnFeedback.BoolValue) //Power Off
            //    {CrestronConsole.PrintLine("disp3 power off status {0}:", ControlSystem.disp3.Power.PowerOffFeedback.BoolValue);
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
            //        tp_ButtonStatus(((uint)NVXOutputs.out_Disp3).ToString(), ((uint)NVXInputs.in_Blank).ToString());
            //        //Mics.Mute("ON");
            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler disp3_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler disp3_BaseEvent Error:  {e.Message}, {e.StackTrace}");
            //    Email.SendEmail(RoomSetup.MailSubject + " NVXHandler disp3_BaseEvent Error", e.Message);
            //}
            //try
            //{
            //    if (ControlSystem.disp3.Power.PowerOnFeedback.BoolValue && !ControlSystem.disp3.Power.PowerOffFeedback.BoolValue)  //Power On
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = false;

            //        if (ControlSystem.disp3.Video.Source.SourceSelect.UShortValue != 1)
            //            ControlSystem.disp3.Video.Source.SourceSelect.UShortValue = 1;

            //    }

            //    if (ControlSystem.disp3.Power.PowerOffFeedback.BoolValue && !ControlSystem.disp3.Power.PowerOnFeedback.BoolValue) //Power Off
            //    {
            //        ControlSystem.tp.BooleanInput[((uint)Join.btn3_PwrOnVis)].BoolValue = true;
            //        tp_ButtonStatus(((uint)NVXOutputs.out_Disp3).ToString(), ((uint)NVXInputs.in_Blank).ToString());

            //    }
            //}
            //catch (Exception e)
            //{
            //    ErrorLog.Notice($"NVXHandler disp3_BaseEvent Error:  {e.Message}");
            //    CrestronConsole.PrintLine($"NVXHandler disp3_BaseEvent Error:  {e.Message}");
            //    Email.SendEmail(RoomSetup.MailSubject + " NVXHandler disp3_BaseEvent Error", e.Message);
            //}
        }

        public static void Initialize()
        {
            try
            {
            
                ControlSystem.tp.ExtenderButtonToolbarReservedSigs.Use();
                ControlSystem.tp.ExtenderButtonToolbarReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystemReservedSigs.Use();
                ControlSystem.tp.ExtenderSystemReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.Use();
                ControlSystem.tp.ExtenderScreenSaverReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;
                ControlSystem.tp.ExtenderSystem3ReservedSigs.Use();
                ControlSystem.tp.ExtenderSystem3ReservedSigs.DeviceExtenderSigChange += tp_EXTSSSigChange;



                if (ControlSystem.tp.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.tp.RegistrationFailureReason.ToString());

                ControlSystem.tp.SigChange += new SigEventHandler(tp_SigChange);
                ControlSystem.tp.OnlineStatusChange += new OnlineStatusChangeEventHandler(tp_OnlineStatusChange);

                if (ControlSystem.am3200.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.am3200.RegistrationFailureReason.ToString());

                ControlSystem.am3200.OnlineStatusChange += new OnlineStatusChangeEventHandler(am3200_OnlineStatusChange);

                if (ControlSystem.EISC.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    throw new Exception(ControlSystem.EISC.RegistrationFailureReason.ToString());

                if (RoomSetup.Display1 == "proj")
                {
                    if (ControlSystem.proj1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj1.RegistrationFailureReason.ToString());

                    ControlSystem.proj1.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj1_OnlineStatusChange);
                    //ControlSystem.proj1.BaseEvent += new BaseEventHandler(proj1_BaseEvent);
                    //Proj1On = true;
                }
                if (RoomSetup.Display1 == "tv")
                {
                    if (ControlSystem.disp1.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp1.RegistrationFailureReason.ToString());

                    ControlSystem.disp1.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp1_OnlineStatusChange);
                    //ControlSystem.disp1.BaseEvent += new BaseEventHandler(disp1_BaseEvent);
                }

                if (RoomSetup.Display2 == "proj")
                {
                    if (ControlSystem.proj2.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj2.RegistrationFailureReason.ToString());

                    ControlSystem.proj2.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj2_OnlineStatusChange);
                    //ControlSystem.proj2.BaseEvent += new BaseEventHandler(proj2_BaseEvent);
                    //Proj2On = true;
                }
                if (RoomSetup.Display2 == "tv")
                {
                    if (ControlSystem.disp2.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp2.RegistrationFailureReason.ToString());

                    ControlSystem.disp2.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp2_OnlineStatusChange);
                    //ControlSystem.disp2.BaseEvent += new BaseEventHandler(disp2_BaseEvent);
                }


                if (RoomSetup.Display3 == "proj")
                {
                    if (ControlSystem.proj3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.proj3.RegistrationFailureReason.ToString());

                    ControlSystem.proj3.OnlineStatusChange += new OnlineStatusChangeEventHandler(proj3_OnlineStatusChange);
                    //ControlSystem.proj3.BaseEvent += new BaseEventHandler(proj3_BaseEvent);
                    //Proj3On = true;
                }
                if (RoomSetup.Display3 == "tv")
                {
                    if (ControlSystem.disp3.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        throw new Exception(ControlSystem.disp3.RegistrationFailureReason.ToString());

                    ControlSystem.disp3.OnlineStatusChange += new OnlineStatusChangeEventHandler(disp3_OnlineStatusChange);
                    //ControlSystem.disp3.BaseEvent += new BaseEventHandler(disp3_BaseEvent);
                }


#if DEBUG
                CrestronConsole.PrintLine("Touchpanel Layout: {0}", RoomSetup.Touchpanel.TP_RoomType.ToLower());
#endif
                //Determine which Touchpanel Layout to use
                switch (RoomSetup.Touchpanel.TP_RoomType.ToLower())
            {

                case "evertz_1":
                    
                    ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = true;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
                        
                        tp_ButtonStatus("2", "0");
                        
                        break;
                case "evertz_2":
                    
                    ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = true;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = false;
                        
                        tp_ButtonStatus("2", "0");
                        tp_ButtonStatus("3", "0");
                        break;
                case "evertz_3":
                   
                    ControlSystem.tp.BooleanInput[((uint)Join.pg1Proj)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg2Proj)].BoolValue = false;
                    ControlSystem.tp.BooleanInput[((uint)Join.pg3Display)].BoolValue = true;
                        tp_ButtonStatus("2", "0");
                        tp_ButtonStatus("3", "0");
                        tp_ButtonStatus("4", "0");
                        break;
            }

                string IP = CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, 0);

                Mics.MicList();
                ControlSystem.tp.StringInput[((uint)Join.lblRoomName)].StringValue = RoomSetup.Touchpanel.RoomText;
                Sony.MakeDictionary();
                ControlSystem.tp.BooleanInput[((uint)Join.mode_TeachCamVisibile)].BoolValue = true;
                ControlSystem.tp.BooleanInput[((uint)Join.mode_StuCamVisibile)].BoolValue = false;
                ControlSystem.tp.StringInput[((uint)Join.serial_Stream)].StringValue = Constants.rtsp + RoomSetup.SonyCameras.CommonProperties.TeacherIP + Constants.rtspStream;
                //ControlSystem.tp.UShortInput[((uint)Join.analog_StreamType)].UShortValue = ((ushort)Join.mode_H264);            
            
                ControlSystem.tp.StringInput[((uint)Join.serial_TPS1)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "1" + Constants.PresetFileSuffix;
                ControlSystem.tp.StringInput[((uint)Join.serial_TPS2)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "2" + Constants.PresetFileSuffix;
                ControlSystem.tp.StringInput[((uint)Join.serial_TPS3)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "3" + Constants.PresetFileSuffix;
                ControlSystem.tp.StringInput[((uint)Join.serial_SPS1)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "4" + Constants.PresetFileSuffix;
                ControlSystem.tp.StringInput[((uint)Join.serial_SPS2)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "5" + Constants.PresetFileSuffix;
                ControlSystem.tp.StringInput[((uint)Join.serial_SPS3)].StringValue = Constants.http + IP + Constants.PresetHTMLFolder + "6" + Constants.PresetFileSuffix;
                NVX.InitializeSystem();
            
                //if (RoomSetup.NvxSettings.OutputDictionary.ContainsKey("5"))
                //{
                //NVX.RouteNVX(((uint)NVXInputs.in_PCExtDesk).ToString() + "," + ((uint)NVXOutputs.out_EXTDisplay).ToString());
                NVX.RouteNVX(((uint)NVXInputs.in_TeachCam).ToString() + "," + ((uint)NVXOutputs.out_VTC).ToString());
                ControlSystem.tp.UShortInput[((ushort)Join.btn_VTCOpenVal)].UShortValue = ((ushort)Join.mode_VTCTeachCam);
                ControlSystem.tp.BooleanInput[((uint)Join.btn_VTCTeachCamOn)].BoolValue = true;
                //}

                switch (RoomSetup.Touchpanel.TP_RoomType.ToLower())
                {
                    case "evertz_1":

                        tp_ButtonStatus("13", "0");
                        break;
                    case "evertz_2":

                        tp_ButtonStatus("2", "0");
                        tp_ButtonStatus("3", "0");
                        break;
                    case "evertz_3":

                        tp_ButtonStatus("2", "0");
                        tp_ButtonStatus("3", "0");
                        tp_ButtonStatus("4", "0");
                        break;
                }

            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error initializing NVXHandler: {0}", e.Message);
                CrestronConsole.PrintLine("Error initializing NVXHandler: {0}", e.StackTrace);
                CrestronConsole.PrintLine("error initializing nvxhandler: {0}", e.Data);
                ErrorLog.Error("Error initializing NVXHandler: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Error initializing NVXHandler", e.Message);
            }
        }

        private static void tp_EXTSSSigChange(DeviceExtender currentDeviceExtender, SigEventArgs args)
        {
            try
            {
                if (args.Sig.Type == eSigType.Bool)
                {
                    if (args.Sig.Number == 18358 && Scheduling.Prox_Active == true)
                    {
                        ControlSystem.tp.SleepWakeManager.Wake();
                    }
                    return;
                }
                else if (args.Sig.Type == eSigType.UShort)
                {
                    return;
                }
                else if (args.Sig.Type == eSigType.String)
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in tp_EXTSSSigChange: {0}", e.Message);
                ErrorLog.Error("Error in tp_EXTSSSigChange: {0}", e.Message);
                Email.SendEmail(RoomSetup.MailSubject + " Error in tp_EXTSSSigChange", e.Message);
            }
        }
    }
}
