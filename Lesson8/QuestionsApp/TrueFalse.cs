using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuestionsApp
{
    public class TrueFalse
    {
        
        #region Private Fields

        private string fileName;
        private List<Question> list;

        #endregion

        #region Public Properties

        public string FileName
        {
            set { fileName = value; }
        }

        public Question this[int index]
        {
            get { return list[index]; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        #endregion

        #region Constructors

        public TrueFalse(string fileName)
        {
            this.fileName = fileName;
            list = new List<Question>();
        }

        #endregion

        #region Public Methods

        public void Add(string text, bool trueFalse)
        {
            list.Add(new Question(text, trueFalse));
        }

        public void Remove(int index)
        {
            if (list != null && index < list.Count && index >= 0)
                list.RemoveAt(index);
        }

        public void Load()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                list = (List<Question>)xmlSerializer.Deserialize(fileStream);
            }
        }

        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(fileStream, list);
            }
        }

        #endregion

    }
}
