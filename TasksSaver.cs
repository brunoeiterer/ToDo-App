using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using System.Collections.Generic;
using System.IO;
using System;

namespace ToDoApp
{
    class TasksSaver
    {
        private static BinaryFormatter formatter;
        static TasksSaver()
        {
            formatter = new BinaryFormatter();
        }

        public static void Save(List<Task> objectToSave)
        {
            List<string[]> tempList = new List<string[]>();

            for(int i = 0; i < objectToSave.Count; i++)
            {
                string[] tempStringArray = new string[4]
                {
                    objectToSave[i].GetTaskName(),
                    objectToSave[i].GetTaskContent(),
                    objectToSave[i].GetTaskDate(),
                    ((objectToSave[i]).TaskComplete).ToString()
                };

                Console.WriteLine(objectToSave[i].GetTaskName());
                Console.WriteLine(objectToSave[i].GetTaskContent());
                Console.WriteLine(objectToSave[i].GetTaskDate());
                Console.WriteLine(((objectToSave[i]).TaskComplete).ToString());
                Console.WriteLine("");

                tempList.Add(tempStringArray);
            }

            using (Stream stream = File.Create("ToDo.bin"))
            {
                formatter.Serialize(stream, tempList);
            }
        }
    }
}
