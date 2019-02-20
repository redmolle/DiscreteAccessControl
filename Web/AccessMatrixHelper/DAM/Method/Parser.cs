using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAM.Dict;
using DAM.Model;
using Newtonsoft.Json;

namespace DAM.Method {
    static class Parser {
        public static int? wordsCount { get; set; }

        public static string[] words { get; set; }

        public static string input { get; set; }
        public static string[] Split (string input, bool IsNeedWhiteSpaces) {
            if (!IsNeedWhiteSpaces) {
                input = Regex.Replace (input, @"\s+", string.Empty);
                input = input.TrimStart ();
            }
            var t = Regex.Split (input, CommonDict.SplitPattern);
            string[] toReturn = t.Where (s => s != string.Empty).ToArray<string> ();
            Parser.wordsCount = toReturn.Count ();
            return toReturn;
        }

        public static void print (List<string> list) {
            foreach (string l in list) {
                Console.WriteLine (l);
            }
        }

        public static void EndLineCheck (ForParse attribute) {
            if (attribute.wordIndex + 1 > (Parser.wordsCount == null ? 0 : Parser.wordsCount)) {
                Throw.UnexpectedEndException (new ForException {
                    wrongWordAt = attribute.wordIndex,
                        wrongLetterAt = 0,
                        //letter = ' ',
                        sentence = attribute.sentence,
                        sentenceForMessage = attribute.sentenceForMessage,
                        //expectedWord = attribute.separator
                });
            } //Конец строки
        }

        public static void ParseName (ForParse attribute) {
            int i = 0;

            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.Name.ToLower () + " " + attribute.sentenceForMessage
            });
            //attribute, Termi.Name.ToLower() + " " + attribute.sentenceForMessage);

            if (!CommonDict.alphabet.Contains (Parser.words[attribute.wordIndex][i])) {
                Throw.WrongNameException (new ForException {
                    wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                        wrongLetterAt = i,
                        letter = Parser.words[attribute.wordIndex][i],
                        sentence = attribute.sentence,
                        sentenceForMessage = attribute.sentenceForMessage
                });
                //Throw.WrongNameException(attribute.wordIndex, i, Parse.words[attribute.wordIndex][i], attribute.sentence, attribute.sentenceForMessage);
            } else {
                foreach (char c in Parser.words[attribute.wordIndex]) {
                    if (!CommonDict.alphabetWithNumbers.Contains (Parser.words[attribute.wordIndex][i])) {
                        Throw.WrongNameException (new ForException {
                            wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                wrongLetterAt = i,
                                letter = Parser.words[attribute.wordIndex][i],
                                sentence = attribute.sentence,
                                sentenceForMessage = attribute.sentenceForMessage,
                                previousWord = Parser.words[attribute.wordIndex].Substring (0, i)
                        });
                    }
                    i++;
                }
            }
        }

        public static void ParseID (ForParse attribute) {
            int i = 0;

            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.ID + " " + attribute.sentenceForMessage
            });

            foreach (char c in Parser.words[attribute.wordIndex]) {
                if (!CommonDict.numeric.Contains (c)) {
                    if (i == 0) {
                        Throw.WrongIDException (new ForException {
                            wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                wrongLetterAt = i,
                                letter = Parser.words[attribute.wordIndex][i],
                                sentence = attribute.sentence,
                                sentenceForMessage = attribute.sentenceForMessage
                        });
                    } else {
                        Throw.WrongIDException (new ForException {
                            
                            wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                wrongLetterAt = i,
                                letter = Parser.words[attribute.wordIndex][i],
                                sentence = attribute.sentence,
                                sentenceForMessage = attribute.sentenceForMessage,
                                previousWord = Parser.words[attribute.wordIndex].Substring (0, i)
                        });
                    }
                }
                i++;
            }
        }

        public static void ParceValue (ForParse attribute) {
            int i = 0;

            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.Value.ToLower () + " " + attribute.sentenceForMessage
            });

            if (!CommonDict.alphabet.Contains (Parser.words[attribute.wordIndex][i])) {
                foreach (char c in Parser.words[attribute.wordIndex]) {
                    if (!CommonDict.numeric.Contains (c)) {
                        if (i == 0) {
                            Throw.WrongValueException (new ForException {
                                wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                    wrongLetterAt = i,
                                    letter = Parser.words[attribute.wordIndex][i],
                                    sentence = attribute.sentence,
                                    sentenceForMessage = attribute.sentenceForMessage
                            });
                        } else {
                            Throw.WrongValueException (new ForException {
                                wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                    wrongLetterAt = i,
                                    letter = Parser.words[attribute.wordIndex][i],
                                    sentence = attribute.sentence,
                                    sentenceForMessage = attribute.sentenceForMessage,
                                    previousWord = Parser.words[attribute.wordIndex].Substring (0, i)
                            });
                        }
                    }
                    i++;
                }
            } else {
                foreach (char c in Parser.words[attribute.wordIndex]) {
                    if (!CommonDict.alphabetWithNumbers.Contains (Parser.words[attribute.wordIndex][i])) {
                        if (i == 0) {
                            Throw.WrongValueException (new ForException {
                                wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                    wrongLetterAt = i,
                                    letter = Parser.words[attribute.wordIndex][i],
                                    sentence = attribute.sentence,
                                    sentenceForMessage = attribute.sentenceForMessage
                            });
                        } else {
                            Throw.WrongValueException (new ForException {
                                wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                                    wrongLetterAt = i,
                                    letter = Parser.words[attribute.wordIndex][i],
                                    sentence = attribute.sentence,
                                    sentenceForMessage = attribute.sentenceForMessage,
                                    previousWord = Parser.words[attribute.wordIndex].Substring (0, i)
                            });
                        }
                    }
                    i++;
                }
            }
        }

        public static void ParseSeparator (ForParse attribute) {

            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    separator = attribute.separator,
                    sentenceForMessage = $"{Term.Separator.ToLower()} \"{attribute.separator}\" после {attribute.sentenceForMessage}"
            });
            if (!Parser.words[attribute.wordIndex].Equals (attribute.separator)) {
                Throw.WrongSeparatorException (new ForException {
                    wrongWordAt = attribute.wordIndex,
                    wrongWordLength = Parser.words[attribute.wordIndex].Length,
                        wrongLetterAt = 0,
                        letter = Parser.words[attribute.wordIndex][0],
                        sentence = attribute.sentence,
                        sentenceForMessage = attribute.sentenceForMessage,
                        expectedWord = attribute.separator,
                        previousWord = Parser.words[attribute.wordIndex - 1]
                });
                //Throw.WrongSeparatorException(attribute.wordIndex, 0, Parse.words[attribute.wordIndex][0], attribute.sentence, attribute.sentenceForMessage, attribute.separator, attribute.previousWord);
            }
        }

        public static void ParseObjectParamName (ForParse attribute) {
            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.Name.ToLower () + " " + WordInMessage.SystemObjectParam
            });

            if (!CommonDict.ObjectParamNameList.Contains (Parser.words[attribute.wordIndex])) {
                Throw.WrongObjectParamName (new ForException {
                    wrongWordAt = attribute.wordIndex,
                        found = Parser.words[attribute.wordIndex]
                });
            }
        }

        public static void ParseUserAccessParamName (ForParse attribute) {
            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.Name.ToLower () + " " + WordInMessage.SystemUserParam
            });

            if (!CommonDict.UserAccessParamNameList.Contains (Parser.words[attribute.wordIndex])) {
                Throw.WrongUserAccesstParamName (new ForException {
                    wrongWordAt = attribute.wordIndex,
                        found = Parser.words[attribute.wordIndex]
                });
            }
        }

        public static void IsObjectInSystem (DAM.Model.System system, ForParse attribute) {
            EndLineCheck (new ForParse {
                wordIndex = attribute.wordIndex,
                    sentence = attribute.sentence,
                    sentenceForMessage = Term.ID.ToLower () + " " + WordInMessage.SystemObject
            });

                Parser.ParseID (new ForParse {
                    wordIndex = attribute.wordIndex,
                        sentence = Sentence.SystemObject,
                        sentenceForMessage = WordInMessage.SystemObject
                }); //ID объекта
            if (!system.Objects.Any (x => x.ID == Convert.ToUInt32 (Parser.words[attribute.wordIndex]))) {
                Throw.NoSuchObject (new ForException {
                    wrongWordAt = attribute.wordIndex,
                        found = Parser.words[attribute.wordIndex]
                });
            }
        }

        public static DAM.Model.System Run (string input) {
            Parser.input = input;
            
            Parser.words = Parser.Split (Parser.input, false);

            int i = 0;
            Parser.ParseName (new ForParse {
                wordIndex = i,
                    sentence = Sentence.System,
                    sentenceForMessage = WordInMessage.System
            }); //Имя системы

            i++;
            Parser.ParseSeparator (new ForParse {
                wordIndex = i,
                    sentence = Sentence.System,
                    separator = SeparatorAfter.SystemName,
                    sentenceForMessage = WordInMessage.Name + " " + WordInMessage.System,
            }); //Разделитель после имени системы

            DAM.Model.System system = new DAM.Model.System {
                Name = words[i - 1],
                Params = new List<DAM.Model.Param> (),
                Objects = new List<DAM.Model.Object> (),
                Users = new List<DAM.Model.User> (),
                //AccessMatrix = new Dictionary<Tuple<string, int>, string>()
            };

            do {
                i++;
                Parser.ParseName (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemParam,
                        sentenceForMessage = WordInMessage.SystemParam
                }); //Имя параметра системы

                i++;
                Parser.ParseSeparator (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemParam,
                        separator = SeparatorAfter.SystemParamName,
                        sentenceForMessage = WordInMessage.Name + " " + WordInMessage.SystemParam,
                }); //Разделитель после имени параметра системы

                i++;
                Parser.ParseID (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemParam,
                        sentenceForMessage = WordInMessage.SystemParam
                }); //ID параметра системы

                i++;
                Parser.ParseSeparator (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemParam,
                        separator = SeparatorAfter.SystemParamID,
                        sentenceForMessage = WordInMessage.ID + " " + WordInMessage.SystemParam,
                }); //Разделитель после ID параметра системы

                i++;
                Parser.ParceValue (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemParam,
                        sentenceForMessage = WordInMessage.SystemParam
                }); //Значение параметра системы

                i++;
                try {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemParam,
                            separator = SeparatorAfter.SystemParam,
                            sentenceForMessage = WordInMessage.Value + " " + WordInMessage.SystemParam,
                    }); //Будет ли следующий параметр системы
                } catch (DACException) {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemParam,
                            separator = SeparatorAfter.System,
                            sentenceForMessage = WordInMessage.Value + " " + WordInMessage.SystemParam,
                    }); //Следующего параметра системы не будет, система закрыта?
                }

                system.Params.Add (new Param {
                    Name = words[i - 5],
                        ID = Convert.ToInt32 (words[i - 3]),
                        Value = words[i - 1]
                }); //Сохраняем параметр
            }
            while (i < Parser.wordsCount && words[i].Equals (SeparatorAfter.SystemParam) && !words[i].Equals (SeparatorAfter.System)); //Параметры системы

            do {
                i++;
                Parser.ParseName (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemObject,
                        sentenceForMessage = WordInMessage.SystemObject
                }); //Имя объекта

                i++;
                Parser.ParseSeparator (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemObject,
                        separator = SeparatorAfter.ObjectName,
                        sentenceForMessage = WordInMessage.Name + " " + WordInMessage.SystemObject,
                }); //Разделитель после имени объекта

                i++;
                Parser.ParseID (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemObject,
                        sentenceForMessage = WordInMessage.SystemObject
                }); //ID объекта

                i++;
                Parser.ParseSeparator (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemObject,
                        separator = SeparatorAfter.ObjectID,
                        sentenceForMessage = WordInMessage.ID + " " + WordInMessage.SystemObject,
                }); //Разделитель после ID объекта

                DAM.Model.Object systemObject = new DAM.Model.Object {
                    Name = words[i - 3],
                    ID = Convert.ToInt32 (words[i - 1]),
                    Params = new List<Param> ()
                };

                do {
                    i++;
                    Parser.ParseObjectParamName (new ForParse {
                        wordIndex = i,
                    }); //Имя параметра объекта

                    i++;
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemObjectParam,
                            separator = SeparatorAfter.ObjectParamName,
                            sentenceForMessage = WordInMessage.Name + " " + WordInMessage.SystemObjectParam,
                    }); //Разделитель после имени параметра объекта

                    i++;
                    Parser.ParceValue (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemObjectParam,
                            sentenceForMessage = WordInMessage.SystemObjectParam
                    }); //Значение параметра объекта

                    i++;
                    try {
                        Parser.ParseSeparator (new ForParse {
                            wordIndex = i,
                                sentence = Sentence.SystemObjectParam,
                                separator = SeparatorAfter.ObjectParam,
                                sentenceForMessage = WordInMessage.Value + " " + WordInMessage.SystemObjectParam,
                        }); //Разделитель почле значения параметра объекта
                    } catch (DACException) { }
                    systemObject.Params.Add (new Param {
                        Name = words[i - 3],
                            Value = words[i - 1]
                    }); //Добавляем параметр объекту
                }
                while (i < Parser.wordsCount && words[i].Equals (SeparatorAfter.ObjectParam) && !words[i].Equals (SeparatorAfter.Object)); //Будет следующий параметр

                try {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemObject,
                            separator = SeparatorAfter.Object,
                            sentenceForMessage = WordInMessage.Value + " " + WordInMessage.SystemObjectParam,
                    }); //Будет ли следующий параметр объекта
                } catch (DACException) {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemObject,
                            separator = SeparatorAfter.LastObject,
                            sentenceForMessage = WordInMessage.Value + " " + WordInMessage.SystemObjectParam,
                    }); //Это конец объекта?
                }

                system.Objects.Add (systemObject); //Добавляем объект в систему

            } while (i < Parser.wordsCount && words[i].Equals (SeparatorAfter.Object) && !words[i].Equals (SeparatorAfter.LastObject)); //Объектов больше нет

            do {
                i++;
                Parser.ParseName (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemUser,
                        sentenceForMessage = WordInMessage.SystemUser
                }); //Имя пользователя

                i++;
                Parser.ParseSeparator (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemUser,
                        separator = SeparatorAfter.UserName,
                        sentenceForMessage = WordInMessage.Name + " " + WordInMessage.SystemUser,
                }); //Разделитель после имени пользователя

                i++;
                Parser.ParseID (new ForParse {
                    wordIndex = i,
                        sentence = Sentence.SystemUser,
                        sentenceForMessage = WordInMessage.SystemUser
                }); //ID пользователя

                User systemUser = new User {
                    Name = words[i - 2],
                    ID = Convert.ToInt32 (words[i]),
                    Params = new List<Param> ()
                };

                i++;
                try {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUser,
                            separator = SeparatorAfter.UserID,
                            sentenceForMessage = WordInMessage.ID + " " + WordInMessage.SystemUser,
                    }); //Разделитель после ID пользователя
                } catch {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUser,
                            separator = SeparatorAfter.LastUser,
                            sentenceForMessage = WordInMessage.SystemUser /* + " " + WordInMessage.SystemObjectParam*/ ,
                    }); //Это конец пользователя?
                    break;
                }

                do {
                    i++;
                    Parser.IsObjectInSystem (system, new ForParse {
                        wordIndex = i,
                    }); //Объект в системе?

                    i++;
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUserParam,
                            separator = SeparatorAfter.UserParamID,
                            sentenceForMessage = WordInMessage.ID + " " + WordInMessage.SystemObject,
                    }); //Разделитель после ID объекта

                    i++;
                    Parser.ParseUserAccessParamName (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUserParam,
                            sentenceForMessage = WordInMessage.SystemUserParam
                    }); //Параметр доступа пользователя к объекту

                    i++;
                    try {
                        Parser.ParseSeparator (new ForParse {
                            wordIndex = i,
                                sentence = Sentence.SystemUserParam,
                                separator = SeparatorAfter.UserParam,
                                sentenceForMessage = WordInMessage.Access + " " + WordInMessage.SystemUser,
                        }); //Разделитель почле значения параметра доступа пользователя
                    } catch (DACException) { }
                    systemUser.Params.Add (new Param {
                        ID = Convert.ToInt32 (words[i - 3]),
                            Value = words[i - 1]
                    }); //Добавляем параметр пользователю
                    //SystemMethod.AddAccessParam(system, new Tuple<string, int>(systemUser.Name, Convert.ToInt32(words[i - 3])), words[i - 1]);
                }
                while (i < Parser.wordsCount && words[i].Equals (SeparatorAfter.UserParam) /* && !words[i].Equals(SeparatorAfter.User)*/ ); //Будет следующий параметр

                try {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUser,
                            separator = SeparatorAfter.User,
                            sentenceForMessage = WordInMessage.SystemUser,
                    }); //Будет еще пользватель?
                } catch (DACException) {
                    Parser.ParseSeparator (new ForParse {
                        wordIndex = i,
                            sentence = Sentence.SystemUser,
                            separator = SeparatorAfter.LastUser,
                            sentenceForMessage = WordInMessage.SystemUser /* + " " + WordInMessage.SystemObjectParam*/ ,
                    }); //Это конец пользователя?
                }

                system.Users.Add (systemUser); //Добавляем пользователя в систему

            } while (i < Parser.wordsCount && words[i].Equals (SeparatorAfter.User) /*&& !words[i].Equals(SeparatorAfter.LastObject)*/ ); //Объектов больше нет

            system.Length = SystemMethod.GetLength(i);
            //system.Json = JsonConvert.SerializeObject(system, Formatting.Indented);
            return system;

            //Парсинг пользователей
        }
    }
}