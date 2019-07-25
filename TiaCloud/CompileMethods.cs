using System;
using Siemens.Engineering.Compiler;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Blocks;

namespace TiaCloud
{
    class CompileMethods
    {
        public static string CompilePlcSoftware(PlcSoftware plcSoftware) 
        {
            string message = null;
            ICompilable compilePLC = plcSoftware.GetService<ICompilable>(); 
            CompilerResult result = compilePLC.Compile();
            message = WriteCompilerResults(result);
            return message;
        }

        public static string CompileHmiTarget(HmiTarget hmiTarget)
        {
            string message = null;
            ICompilable compileHMI = hmiTarget.GetService<ICompilable>();
            CompilerResult result = compileHMI.Compile();
            message = WriteCompilerResults(result);
            return message;

        }

        public static string CompileCodeBlock(PlcSoftware plcSoftware)
        {
            string message = null;
            CodeBlock block = plcSoftware.BlockGroup.Blocks.Find("MyCodeBlock") as CodeBlock;
            if (block != null)
            {
                ICompilable compileService = block.GetService<ICompilable>();
                CompilerResult result = compileService.Compile();
                message = WriteCompilerResults(result);

            }
            return message;
        }

        private static string WriteCompilerResults(CompilerResult result)    //results on console when using withoutUserInterface mode       
        {
            string message = "";
            

            Console.WriteLine("State:" + result.State);
            Console.WriteLine("Warning Count:" + result.WarningCount);
            Console.WriteLine("Error Count:" + result.ErrorCount);
            // message =  RecursivelyWriteMessages(result.Messages); //it can be developed with recursive function
            foreach (CompilerResultMessage me in result.Messages)
            {
                message += "\n\n Path: " + me.Path;
                message += "\n DateTime: " + me.DateTime;
                message += "\n State: " + me.State;
                message += "\n Description: " + me.Description;
                message += "\n Warning Count:" + result.WarningCount;
                message += "\n Error Count: " + result.ErrorCount;

            }
            return message;
        }
        //private static string RecursivelyWriteMessages(CompilerResultMessageComposition messages, string indent = "") 
        //{
        //    indent += "\t";
        //    foreach (CompilerResultMessage message in messages)
        //    {
        //        Console.WriteLine(indent + "Path: " + message.Path);
        //        Console.WriteLine(indent + "DateTime: " + message.DateTime);
        //        Console.WriteLine(indent + "State: " + message.State);
        //        Console.WriteLine(indent + "Description: " + message.Description);
        //        Console.WriteLine(indent + "Warning Count: " + message.WarningCount);
        //        Console.WriteLine(indent + "Error Count: " + message.ErrorCount);
        //        indent = RecursivelyWriteMessages(message.Messages, indent);
        //    }
        //    return indent;
        //}
    }
}
