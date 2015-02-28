using System;
using System.IO;
using System.Xml.Serialization;

namespace Fragespiel_WPF
{
    public class QuestionAnswerPair
    {
        public String Question { get; set; }
        public int Points { get; set; }
        public String Topic { get; set; }

        [XmlIgnore]
        public bool isTaken { get; set; }
        public QuestionAnswerPair()
        {

        }

        public QuestionAnswerPair(String question, int points, String topic)
        {
            Question = question;
            Points = points;
            Topic = topic;
            isTaken = false;
            Serialize();
        }

        private void Serialize()
        {
            XmlSerializer x = new XmlSerializer(this.GetType());
            TextWriter writer = new StreamWriter(Topic + Points + "-" + Guid.NewGuid() + ".xml");
            x.Serialize(writer, this);
        }
    }
}
