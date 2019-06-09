using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PixelZEngine.Clases.DataClases;
using PixelZEngine.Clases.DataClases.LoaderInfo;
using System.Drawing;

namespace PixelZEngine.Clases.WorkClases
{
    /// <summary>
    /// Класс загрузки сцен
    /// </summary>
    internal class SceneLoader
    {
        /// <summary>
        /// Класс работы с XML
        /// </summary>
        private XmlWorker xw;
        /// <summary>
        /// Класс загрузки спрайтов
        /// </summary>
        private SpriteLoader loader;

        /// <summary>
        /// Путь загрузки списка сцен
        /// </summary>
        private string scenesPath;
        /// <summary>
        /// Путь загрузки спрайтов
        /// </summary>
        private string spritesPath;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SceneLoader()
        {
            //Инициализируем класс работы с XML
            xw = new XmlWorker();
            //Инициализируем класс загрузки спрайтов
            loader = new SpriteLoader();
            //Инициализируем пути
            scenesPath = Environment.CurrentDirectory + @"\Files\scenes.xml";
            spritesPath = Environment.CurrentDirectory + @"\Files\Sprites\";
        }

        /// <summary>
        /// Загружаем список сцен
        /// </summary>
        /// <returns>Загруженные сцены</returns>
        public List<scene> loadScenes()
        {
            List<scene> ex = new List<scene>();
            List<sprite> sprites = new List<sprite>();

            try
            {
                //Если файл сцен существует
                if(File.Exists(scenesPath))
                {
                    //Считываем все байты
                    var bytes = File.ReadAllBytes(scenesPath);
                    //Считываем инфу о сценах
                    var scenes = (scenesL)xw.deserialize(typeof(scenesL), bytes);
                    //Проходимся по загруженному списку сцен
                    foreach(var sc in scenes.scenes)
                        //Загружаем спрайты для сцены, и добавляем новую сцену в список
                        ex.Add(new scene(loadSprites(sc.sprites), sc.bgColor, sc.id));                        
                    
                }
            }
            catch { ex = new List<scene>(); }

            return ex;
        } 

        /// <summary>
        /// Загружаем спрайты для сцены
        /// </summary>
        /// <param name="sprites">Список инфы о спрайтах</param>
        /// <returns>Список загруженных спрайтов</returns>
        private sprite[] loadSprites(List<spriteL> sprites)
        {
            //Инициализируем выходной массив
            List<sprite> ex = new List<sprite>();
            //Путь загрузки
            string path;
            //Буфер для загрузки спрайта
            sprite buff;

            //TODO: Добавить вывод ошибок загрузки.
            //т.е. выводить иконку битого спрайта.

            try
            {
                //Проходимся по списку спрайтов
                foreach(var sprite in sprites)
                {
                    //Формируем новый путь к файлу спрайта
                    path = spritesPath + sprite.filename;
                    //Если такой файл реально есть
                    if (File.Exists(path))
                    {
                        //Загружаем картинку
                        using(Bitmap pic = new Bitmap(path))
                        {
                            //Грузим спрайт
                            buff = loader.load(
                                    sprite.id,
                                    pic,
                                    sprite.position,
                                    sprite.replace,
                                    sprite.animation
                                );

                            //Если всё ок
                            if(buff != null)
                                //Добавляем спрайт в список
                                ex.Add(buff);
                        }
                    }

                }

            }
            catch { ex = new List<sprite>(); }

            return ex.ToArray();
        }

        /// <summary>
        /// Сохраняем инфу о сцене
        /// </summary>
        /// <param name="scL">Уже готовая инфа о сцене</param>
        public void saveScenes(scenesL scL)
        {
            try
            {
                //Сериализуем сцену
                var bytes = xw.serializeClass(scL);
                //Сохраняем байты сцен
                File.WriteAllBytes(scenesPath, bytes);
            }
            catch { }
        }
    }
}
