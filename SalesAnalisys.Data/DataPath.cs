using System;

namespace SalesAnalisys.Data
{
    public static class DataPath
    {
        private const string HOME_PATH_ENV = "HOMEPATH";
        private const string DATA_PATH = "data";
        private const string INPUT_PATH = "in";
        private const string OUTPUT_PATH = "out";

        private static string HOME_PATH => Environment.GetEnvironmentVariable(HOME_PATH_ENV);

        public static string InputPath => $"{HOME_PATH}\\{DATA_PATH}\\{INPUT_PATH}";

        public static string OutputPath => $"{HOME_PATH}\\{DATA_PATH}\\{OUTPUT_PATH}";
    }
}
