using Squirrel.Shared.Exceptions;
using System.Diagnostics;

namespace Squirrel.SqlService.BLL.Services.SqlFormatter
{
    public static class PythonScriptExecutor
    {
        public static void ExecuteScript(ProcessStartInfo startInfo, out string result)
        {
            using (Process process = Process.Start(startInfo)!)
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string errors = process.StandardError.ReadToEnd();
                    if (errors.Any())
                    {
                        throw new Exception(errors);
                    }
                    var hasErrors = reader.ReadLine();
                    if (hasErrors == "True")
                    {
                        throw new SqlSyntaxException(reader.ReadToEnd());
                    };
                    result = reader.ReadToEnd();
                    process.WaitForExit();
                }
            }
        }

    }
}