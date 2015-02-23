using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Fragespiel_WPF
{
    class QuestionPool
    {
        private List<QuestionAnswerPair> _questions;
        private Random _rand;

        private static QuestionPool _instance;
        public static QuestionPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuestionPool();
                }
                return _instance;
            }
            private set { _instance = value; }
        }

        private QuestionPool()
        {
            _questions = new List<QuestionAnswerPair>();
            _questions.AddRange(DeserializeQuestions());
            _rand = new Random();
        }
        public void Add(String question, String answer, String topic)
        {
            _questions.Add(new QuestionAnswerPair(question, answer, topic));
        }

        public String[] GetTopics()
        {
            return _questions.Where(q => q.isTaken == false).GroupBy(q => q.Topic).Select(q => q.First().Topic).ToArray();
        }

        public QuestionAnswerPair GetRandomQuestion(String topic)
        {
            var usableQuestions = (from question in _questions
                                   where question.Topic.ToLower() == topic.ToLower() &&
                                         question.isTaken == false
                                   select question).ToArray();

            if (usableQuestions.Length == 0)
            {
                return null;
            }

            var chosenQuestion = usableQuestions[_rand.Next(usableQuestions.Length)];
            chosenQuestion.isTaken = true;

            return chosenQuestion;
        }

        public QuestionAnswerPair[] DeserializeQuestions()
        {
            List<QuestionAnswerPair> qList = new List<QuestionAnswerPair>();
            String path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var xmlFiles = (from file in new DirectoryInfo(path).EnumerateFiles()
                            where file.Name.EndsWith(".xml")
                            select file).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(QuestionAnswerPair));

            foreach (FileInfo file in xmlFiles)
            {
                using (FileStream filestream = new FileStream(file.FullName, FileMode.Open))
                {
                    qList.Add((QuestionAnswerPair)serializer.Deserialize(filestream));
                }
            }

            return qList.ToArray();
        }
    }
}
