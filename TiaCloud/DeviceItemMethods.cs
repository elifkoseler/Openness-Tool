using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.Hmi;

namespace TiaCloud
{
    class DeviceItemMethods
    {   
        public static DeviceItem GetHead(Device device) //gets Head of the device
        {
            foreach (DeviceItem deviceItem in device.DeviceItems)
            {
                if (deviceItem.Classification == DeviceItemClassifications.HM)
                    return deviceItem;
            }
            return null;
        }

        public static DeviceItem GetPlcHead(Device device)  //gets head of PLCs
        {
            return device.DeviceItems[1];   //rail=>[0], head=>[1]
        }

        public static PlcSoftware GetPlcSoftware(Device device) //gets Plc SW of the device
        {
          //  var allDeviceItems = device.DeviceItems;    //also can use DeviceItemComppsition = var
            foreach (DeviceItem deviceItem in device.DeviceItems)
            {
                SoftwareContainer softwareContainer = deviceItem.GetService<SoftwareContainer>();
                if (softwareContainer != null)
                {
                    Software softwareBase = softwareContainer.Software;
                    PlcSoftware plcSoftware = softwareBase as PlcSoftware;
                    return plcSoftware;
                }
            }
            return null;
        }

        public static HmiTarget GetHmiTarget(Device device) //gets HMI target of device
        {
            var allDeviceItems = device.DeviceItems;

            foreach(DeviceItem deviceItem in allDeviceItems)
            {
                SoftwareContainer softwareContainer = deviceItem.GetService<SoftwareContainer>();

                if(softwareContainer != null)
                {
                    Software software = softwareContainer.Software;
                    HmiTarget hmiTarget = software as HmiTarget;
                    return hmiTarget;
                }               
            }
            return null;
        }

        public static DeviceItem AccessDeviceItemFromDevice(Device device)
        {
            DeviceItem deviceItem = device.DeviceItems[0];
            return deviceItem;
        }

    }

}

