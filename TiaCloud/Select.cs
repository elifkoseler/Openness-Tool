using Siemens.Engineering;
using Siemens.Engineering.HW;
using System;
namespace TiaCloud
{
    class Select
    {
        public static string FunctionSelector(Project project,string [] tokens)
        {
            string command = tokens[0];

            switch (command)
            {
                case "add":
                case "ADD":
                    
                    string type = tokens[1];   
                    DeviceMethods.AddSpecificDevice(project,type);
                    MainMethods.DisplayNetwork(project);
                    break;

                case "CON":
                case "con":

                    int sourceDevice = int.Parse(tokens[1]);
                    int targetDevice = int.Parse(tokens[2]);
                    string typeIdentifier = tokens[3];

                    Network.selectConnection(project, sourceDevice, targetDevice,typeIdentifier);
                    MainMethods.DisplayNetwork(project);

                    break;

                case "COMPILE":
                case "compile":

                    int id = int.Parse(tokens[1]);
                    string message = MainMethods.Compile(project, id);
                    return message;
                    

                case "isCon":
                    int deviceIndex = int.Parse(tokens[1]);
                    bool res = false;
                    Device device =  project.Devices[deviceIndex];

                    if (device.Name.Contains("PLC"))
                        res = Network.isPLCconnected(project, device);
                    else
                        res =  Network.isConnected(project, project.Devices[deviceIndex]);

                    if (res == true)
                        Console.WriteLine("Connected");
                    else
                        Console.WriteLine("Not Connected");

                    break;

                case "SAVE":
                    MainMethods.Save(project);
                    break;

                case "CLOSE":
                    MainMethods.Close(project);
                    break;

                case "show":
                    MainMethods.DisplayNetwork(project);
                    break;

                case "change":
                case "CHANGE":
                    int index = int.Parse(tokens[1]);
                    string name = tokens[2];
                    string author = tokens[3];
                    string comment = tokens[4];

                    DeviceMethods.ChangeProperties(project, index, name, author, comment);

                    break;

                default:
                    Console.WriteLine("DEFAULT CASE!");
                    break;
            }
            return null;
        }
    }
}
