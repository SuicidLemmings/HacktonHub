using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Win32;

public class SerialInputs: MonoBehaviour
{
    private bool receivedJump;
    private bool receivedFire;
    public int JUMP = 1;
    public int FIRE = 2;
    private SerialPort serial = new SerialPort(AutodetectArduinoPort(), 9600);
    // Start is called before the first frame update
    void Start()
    {
        serial.Open();
    }
    // Update is called once per frame
    void Update()
    {
        string str = serial.ReadLine();

        serial.DiscardInBuffer();
        setValues(JUMP, false);
        setValues(FIRE, false);
        if (str != JUMP.ToString() && str != FIRE.ToString())
        {
            return;
        }
        int data = int.Parse(str);
        setValues(data, true);
    }

    public void setValues(int select, bool value)
    {
        if (select == JUMP)
            receivedJump = value;
        else if (select == FIRE)
            receivedFire = value;
    }

    public bool getValues(int select)
    {
        if (select == JUMP)
            return receivedJump;
        else if (select == FIRE)
            return receivedFire;
        return false;
    }

    private static string AutodetectArduinoPort()
    {
        List<string> comports = new List<string>();
        RegistryKey rk1 = Registry.LocalMachine;
        RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
        string temp;
        foreach (string s3 in rk2.GetSubKeyNames())
        {
            RegistryKey rk3 = rk2.OpenSubKey(s3);
            foreach (string s in rk3.GetSubKeyNames())
            {
                if (s.Contains("VID") && s.Contains("PID"))
                {
                    RegistryKey rk4 = rk3.OpenSubKey(s);
                    foreach (string s2 in rk4.GetSubKeyNames())
                    {
                        RegistryKey rk5 = rk4.OpenSubKey(s2);
                        if ((temp = (string)rk5.GetValue("FriendlyName")) != null && temp.Contains("Arduino"))
                        {
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            if (rk6 != null && (temp = (string)rk6.GetValue("PortName")) != null)
                            {
                                comports.Add(temp);
                            }
                        }
                    }
                }
            }
        }

        if (comports.Count > 0)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                if (comports.Contains(s))
                    return s;
            }
        }

        return "COM9";
    }

    public void closeDevice()
    {
        if (serial.IsOpen)
            serial.Close();
    }
}
