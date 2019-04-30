using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Windows.Markup;
using System.IO;
using System.Windows.Controls;

namespace ToDoApp
{
    class TasksLoader
    {
        private static BinaryFormatter formatter;
        static TasksLoader()
        {
            formatter = new BinaryFormatter();
        }

        public static List<string[]> Load()
        {
            List<string[]> result = new List<string[]>();
            using(Stream stream = File.Open("ToDo.bin", FileMode.Open))
            {
                result = (List<string[]>) formatter.Deserialize(stream);
            }
            return result;
        }
    }
}
