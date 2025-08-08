using Crestron.SimplSharp;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFAHRooms
{
    public static class NVX
    {    
        public static void RouteNVX(string s)
        {
            try
            {
                CrestronConsole.PrintLine("RouteNVX: {0}", s);
                var data = s.Split(',');
                string input, output;
                input = data[0];
                output = data[1];

                //RoomSetup.NvxSettings.InputDictionary.TryGetValue(input, out var inputValue);
                //RoomSetup.NvxSettings.OutputDictionary.TryGetValue(output, out var outputValue);

                //if (ControlSystem.Is131)
                //{
                //    CrestronConsole.PrintLine("Is131: {0}", ControlSystem.Is131);
                    //if (output == "4")
                    //{
                        //CrestronConsole.PrintLine("output 4?: {0}", output);
                      //  ControlSystem.EISC.StringInput[((uint)NVXRoutes.RouteOutput)].StringValue = input + "," + output;
                
                      //  for (int i = 5; i <= 7; i++)
                        //{
                //            RoomSetup.NvxSettings.OutputDictionary.TryGetValue(i.ToString(), out var outputValue2);
                        //    ControlSystem.EISC.StringInput[((uint)NVXRoutes.RouteOutput)].StringValue = input + "," + i.ToString();
                        //}
                    //}
                //}
                //else
                //{
                    ControlSystem.EISC.StringInput[((uint)NVXRoutes.RouteOutput)].StringValue = input + "," + output;
                    CrestronConsole.PrintLine("NVX Route Sent to NVX: {0}", input + "," + output);
                //}

            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in RouteNVX: {0}", e.Message);
            }
        }

        private static void EISC_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            try
            {
                switch (args.Sig.Type)
                {
                    case eSigType.String:
                        if (args.Sig.Number == 1)
                        {
                            //CrestronConsole.PrintLine("NVX Route Returned from NVX: {0}", args.Sig.StringValue);

                            var data = args.Sig.StringValue.Split(',');
                            string input, output;
                            output = data[1];
                            input = data[0];

                            //CrestronConsole.PrintLine("Input: {0} Output: {1}", input, output);

                            //var ReverseInputDictionary = RoomSetup.NvxSettings.InputDictionary.ToDictionary(x => x.Value.InputNVX, x => x.Value);
                            //var ReverseOutputDictionary = RoomSetup.NvxSettings.OutputDictionary.ToDictionary(x => x.Value.OutputNVX, x => x.Value);

                            //ReverseInputDictionary.TryGetValue(input, out var inputValue);
                            //ReverseOutputDictionary.TryGetValue(output, out var outputValue);

                            //CrestronConsole.PrintLine($"Original input: {input} Original output: {output}");
                            //if(Convert.ToInt32(outputValue.OutputProg) < ((int)NVXOutputs.out_EXTDisplay))
                            //{
                            //if (Convert.ToInt16(output) > 1 && Convert.ToInt16(output) <= 4)
                                NVXHandler.tp_ButtonStatus(output, input);
                            //}
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in EISC_SigChange: {0}", e.Message);
            }
        }
        public static void InitializeSystem()
        {
            try
            {
                if (ControlSystem.EISC.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                {
                    ErrorLog.Error("Error Registering EISC");
                    CrestronConsole.PrintLine("Error Registering EISC");
                }
                else
                {
                    ControlSystem.EISC.SigChange += EISC_SigChange;
                }                
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the NVX constructor: {0}", e.Message);
                CrestronConsole.PrintLine("Error in the NVX constructor: {0}", e.Message);
            }
        }
    }
}
          