using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PixelZEngine.Clases.WorkClases
{
    /// <summary>
    /// Базовый класс работы с XML
    /// </summary>
    public class XmlWorker
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public XmlWorker()
        {

        }

        /// <summary>
        /// Десериализуем xml в класс с данными
        /// </summary>
        /// <param name="t">Тип возвращаемого класса</param>
        /// <param name="data">Массив байт с данными</param>
        /// <returns>Объект указанного класса</returns>
        public object deserialize(Type t, byte[] data)
        {
            object ex = null;

            try
            {
                // передаем в конструктор тип класса
                XmlSerializer xs = new XmlSerializer(t);

                //Инициаализируем поток в памяти
                using (MemoryStream ms = new MemoryStream(data))
                {
                    //Десериализуем xml в объект
                    ex = xs.Deserialize(ms);
                }
            }
            catch { ex = null; }

            return ex;
        }


        /// <summary>
        /// Выполняет сериализацию класса в XML, и возвращает
        /// массив байтов итогового xml-файла
        /// </summary>
        /// <param name="data">Класс, для сериализации</param>
        /// <returns>Массив байт с данными</returns>
        public byte[] serializeClass(object data)
        {
            byte[] ex = null;

            try
            {
                // передаем в конструктор тип класса
                XmlSerializer xs = new XmlSerializer(data.GetType());

                //Инициаализируем поток в памяти
                using (MemoryStream ms = new MemoryStream())
                {
                    //Сериализуем класс в xml
                    xs.Serialize(ms, data);
                    //Возвращаем массив байт
                    ex = ms.ToArray();
                }
            }
            catch { ex = null; }

            return ex;
        }
    }
}
