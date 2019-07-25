using Siemens.Engineering;
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

                    Network.selectConnection(project, sourceDevice, targetDevice,typeIdentifier,command);
                    MainMethods.DisplayNetwork(project);

                    break;

                case "multiCON":
                case "multiCon":

                    sourceDevice = int.Parse(tokens[1]);
                    targetDevice = int.Parse(tokens[2]);
                    typeIdentifier = tokens[3];

                    Network.selectConnection(project, sourceDevice, targetDevice, typeIdentifier,command);
                    MainMethods.DisplayNetwork(project);

                    break;

                case "COMPILE":
                case "compile":

                    int index = int.Parse(tokens[1]);
                    string message = MainMethods.Compile(project, index);
                    return message;
                    

                //case "isCon":
                //    int deviceIndex = int.Parse(tokens[1]);
                //    subnet = Network.multiConnection(project, deviceIndex);
                //    Console.WriteLine(project.Devices[deviceIndex].Name + " is connected to " + subnet.Name);
                //    break;

                case "SAVE":
                case "save":
                    MainMethods.Save(project);
                    break;

                case "CLOSE":
                case "close":
                    MainMethods.Close(project);
                    break;

                case "show":
                    MainMethods.DisplayNetwork(project);
                    break;

                case "change":
                case "CHANGE":
                    index = int.Parse(tokens[1]);
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
