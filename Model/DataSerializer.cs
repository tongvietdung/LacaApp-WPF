using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LacaApp.Model
{
    class DataSerializer
    {
        /// <summary>
        /// Function to serialize an object to file path
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        public void BinarySerialize(object data, string filePath)
        {
            // Create a file stream
            FileStream fileStream;

            // Create a formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Delete existed file and create new one
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Now assign the file stream as new file
            fileStream = File.Create(filePath);

            // Serialize data and add to the file stream
            formatter.Serialize(fileStream, data);

            fileStream.Close();
        }

        public object BinaryDeserialize(string filePath)
        {
            // Create an empty object which is our data later
            object obj = null;

            // Create file stream
            FileStream fileStream;

            // Create formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // If file exists, use the formater to deserialize and assign to our object
            if (File.Exists(filePath))
            {
                // Open file and assign to file stream
                fileStream = File.OpenRead(filePath);

                // Deserialize the file stream
                obj = formatter.Deserialize(fileStream);

                fileStream.Close();
            }

            return obj;
        }
    }
}
