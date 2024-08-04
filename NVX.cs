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


        public enum ItemNum
        {
            RouteOutput = 1,
        }
                
        public static void RouteNVX(string s)
        {
            CrestronConsole.PrintLine("RouteNVX: {0}", s);
            ControlSystem.EISC.StringInput[((uint)ItemNum.RouteOutput)].StringValue = s;

        }

        private static void EISC_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            switch (args.Sig.Type)
            {
                case eSigType.String:
                    if (args.Sig.Number == 1)
                    {
                        CrestronConsole.PrintLine("NVX Route: {0}", args.Sig.StringValue);

                        var data = args.Sig.StringValue.Split(',');
                        string input, output;
                        output = data[0];
                        input = data[1];

                        RoomSetup.NvxSettings.InputDictionary.TryGetValue(input, out var inputValue);                            
                        RoomSetup.NvxSettings.OutputDictionary.TryGetValue(output, out var outputValue);
                        
                        CrestronConsole.PrintLine($"Output : {output} Output Value:{outputValue.value} Input: {input} Input Value: {inputValue.value}");
                    }
                     break;
            }
        }
        public static void InitializeSystem()
        {
            try
            {
                if (ControlSystem.EISC.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                {
                    ErrorLog.Error("Error Registering EISC");
                }
                else
                {
                    ControlSystem.EISC.SigChange += EISC_SigChange;
                }                
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the NVX constructor: {0}", e.Message);
            }
        }

    }
}
          