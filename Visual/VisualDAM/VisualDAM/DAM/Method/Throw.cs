using System;
using DAM.Dict;
using DAM.Model;

namespace DAM.Method
{
    static class Throw
    {

        //Метод возвращающий что отмечать
        public static int From(int w, int? l)
        {
            string[] ws = Parser.Split(Parser.input, true);
            int len = 0;
            for (int i = 0; i < w; i++)
            {
                len += ws[i].Length;
            }
            len += l == null ? 0 : Convert.ToInt32(l);
            return len;
        }

        public static void NoSuchObject(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            throw new DACException
            {
                markFrom = f,
                markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                wrongWordAt = f,
                wrongLetterAt = f + attribute.wrongWordLength,
                sentence = attribute.sentence,
                terminal = Term.ID,
                message = $"В системе не зарегистрирован объект с ID \"{attribute.found}\"."
            };
        }
        public static void UnexpectedEndException(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            throw new DACException
            {
                markFrom = f,
                markTo = attribute.wrongLetterAt = 1,
                terminal = Term.EndLine,
                message = $"Ожидалось встретить {attribute.sentenceForMessage}, а встречен конец строки"
            };
        }
        public static void WrongNameException(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            if (string.IsNullOrEmpty(attribute.previousWord))
            {
                throw new DACException
                {
                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    sentence = attribute.sentence,
                    terminal = Term.Name,
                    message = $"Встречен недопустимый символ \"{attribute.letter}\".\nИмя {attribute.sentenceForMessage} должно начинаться с буквы."
                };
            }
            else
            {
                throw new DACException
                {
                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    sentence = attribute.sentence,
                    terminal = Term.Name,
                    message = $"Встречен недоустимый символ \"{attribute.letter}\" после имени {attribute.sentenceForMessage} \"{attribute.previousWord}\"."
                };
            }
        }
        public static void WrongIDException(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            if (string.IsNullOrEmpty(attribute.previousWord))
            {
                throw new DACException
                {
                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    sentence = attribute.sentence,
                    terminal = Term.ID,
                    message = $"Встречен недопустимый символ \"{attribute.letter}\".\nID {attribute.sentenceForMessage} должен состоять только из цифр."
                };
            }
            else
            {
                throw new DACException
                {

                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    wrongWordAt = f,
                    wrongLetterAt = 1,
                    sentence = attribute.sentence,
                    terminal = Term.ID,
                    message = $"Встречен недоустимый символ \"{attribute.letter}\" после ID {attribute.sentenceForMessage} \"{attribute.previousWord}\"."
                };
            }
        }
        public static void WrongValueException(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            if (string.IsNullOrEmpty(attribute.previousWord))
            {
                throw new DACException
                {
                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    sentence = attribute.sentence,
                    terminal = Term.Value,
                    message = $"Встречен недопустимый символ \"{attribute.letter}\".\nЗначение {attribute.sentenceForMessage} должно состоять только из цифр или быть комбинацией букв и цифр с первой буквой."
                };
            }
            else
            {
                throw new DACException
                {
                    markFrom = f,
                    markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                    sentence = attribute.sentence,
                    terminal = Term.Value,
                    message = $"Встречен недоустимый символ \"{attribute.letter}\" после значения {attribute.sentenceForMessage} \"{attribute.previousWord}\"."
                };
            }
        }
        public static void WrongSeparatorException(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            throw new DACException
            {
                markFrom = f,
                markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                sentence = attribute.sentence,
                terminal = Term.Separator,
                message = $"Встречен недопустимый символ \"{attribute.letter}\".\n После {attribute.sentenceForMessage} \"{attribute.previousWord}\" ожидался разделитель \"{attribute.expectedWord}\""
            };
        }
        public static void WrongObjectParamName(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            throw new DACException
            {
                markFrom = f,
                markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                sentence = Sentence.SystemObjectParam,
                terminal = Term.Name,
                message = $"Встречено недопустимое слово \"{attribute.found}\".\n {Term.Name} \"{Sentence.SystemObjectParam}\" допустимо только из списка ниже:{CommonDict.ListToNumericString(CommonDict.ObjectParamNameList)}"
            };
        }
        public static void WrongUserAccesstParamName(ForException attribute)
        {
            int f = From(attribute.wrongWordAt, attribute.wrongLetterAt);
            throw new DACException
            {
                markFrom = f,
                markTo = attribute.wrongLetterAt == 0 ? Parser.words[attribute.wrongWordAt].Length : 1,
                sentence = Sentence.SystemUserParam,
                terminal = Term.Name,
                message = $"Встречено недопустимое слово \"{attribute.found}\".\n {Term.Name} \"{Sentence.SystemUserParam}\" допустимо только из списка ниже:{CommonDict.ListToNumericString(CommonDict.UserAccessParamNameList)}"
            };
        }
    }
}