﻿using ScpDriverInterface;
using System;
using System.Windows.Forms;

namespace XboxKeyboardMouse
{
    class InputToController
    {
        const int CONTROLLER_NUMBER = 1; 
        static ScpBus scpbus = null;

        public static void startSCPBus()
        {
            X360Controller controller = new X360Controller();
            byte[] report = controller.GetReport();
            byte[] output = new byte[8];
     
            try
            {
                scpbus = new ScpBus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SCP Bus failed to initialize");
                MessageBox.Show(ex.ToString());
            }

            scpbus.PlugIn(1);

            while(true)
            {
                controller = TranslateInput.keyboardInput(controller);

                report = controller.GetReport();
                scpbus.Report(CONTROLLER_NUMBER, report, output);
            }
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            scpbus.Unplug(CONTROLLER_NUMBER);
        }
    }
}

