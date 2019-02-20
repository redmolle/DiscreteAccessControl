import string as strlib
import numpy


def checkName(string):
    if(len(string) == 0):
        return [False, None]
    i = 0
    if(not string[i] in letter_list):
        return [False]
    else:
        i += 1
        while(i < len(string)):
            if(not string[i] in symbol_list):
                return [False, string[i], string[:i]]
            else:
                i += 1
        return [True]
def checkId(string):
    for i in range(len(string)):
        if(not string[i] in digit_list and i == 0):
            return [False]
        elif(not string[i] in digit_list):
            return [False, string[i], string[:i]]
    if(len(string) == 0):
        return [False, None]
    else:
        return [True]
def checkValue(string):
    returned_result = checkId(string)
    if(not returned_result[0] and len(returned_result)==1):
        returned_result = checkName(string)
        if(not returned_result[0]):
            return returned_result
        else:
            return [True]
    else:
        if(not returned_result[0]):
            return returned_result
        else:
            return [True]
def checkAccessId(string):
    for i in range(len(string)):
        if(not string[i] in accessid_list):
            if(i == 0):
                return [False]
            else:
                return [False, string[i], string[:i]]
        else:
            continue
    return [True]
def checkParameterObject(string):
    if(string in objparameters_list):
        return [True]
    else:
        return [False]
        #most_suitable=objparameters_list[0]
        #for par_ex in objparameters_list:
        #    for i in range(len(string)):
        #        if(string[:i] == par_ex[:i]):
        #            most_suitable =
        #            continue
def printMatrix(matrix):
    max = 0
    for i in range(len(matrix)):
        for j in range(len(matrix[i])):
            if(len(matrix[i][j]) > max):
                max = len(matrix[i][j])

    max = str(max)
    toret = '{:^'+max+'}'
    for i in range(len(matrix)):
        for j in range(len(matrix[i])):
            print(toret.format(matrix[i][j]), end='|')
        print()

def checkSystem(word_list):
    i = 0
    if(i == len(word_list)):
        print('Ошибка! Пусто')
        return [False]
    else:
        returned_result = checkName(word_list[i])
        if (not returned_result[0]):
            if(len(returned_result) != 1):
                if(returned_result[1] == None):#################Выражденный случай
                    print('Ошибка! Пусто в начале')#################Выражденный случай
                    return [False]#################Выражденный случай
                else:#################Выражденный случай
                    #print(returned_result[1])
                    print('Ошибка! Встречен символ \'',returned_result[1],'\' после имени системы \'',returned_result[2],'\'')
                    return [False]
            else:
                print('Ошибка! Встречен символ \'',word_list[i][0],'\' в начале строки (Имя системы должно начинаться с буквы латинского алфавита)')
                return [False]
        else:   #Имя системы корректно
            system_name = word_list[i]
            i += 1
            if(i == len(word_list)):
                print('Ошибка! Пусто после имени системы (\'',word_list[i-1],'\')')
                return [False]
            else:
                if(word_list[i] != '='):
                    print('Ошибка! Встречен символ \'',word_list[i],'\' после имени системы  (\'',word_list[i-1],'\')')
                    return [False]
                else:
                    i += 1
                    if (i == len(word_list)):
                        print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                        return [False]
                    else:
                        system_parameters = {}
                        while(True):
                            returned_result = checkName(word_list[i])
                            if (not returned_result[0]):
                                if (len(returned_result) != 1):
                                    if (returned_result[1] == None):#################Выражденный случай
                                        print('Ошибка! Пусто апролдж')#################Выражденный случай
                                        return [False]#################Выражденный случай
                                    else:#################Выражденный случай
                                        print('Ошибка! Встречен символ \'', returned_result[1], '\' после имени параметра системы \'',returned_result[2], '\'')
                                        return [False]
                                else:
                                    print('Ошибка! Встречен символ \'',word_list[i][0],'\' после \'',word_list[i-1],'\' (Имя параметра системы должно начинаться с буквы латинского алфавита)')
                                    return [False]
                            else:# Имя параметра системы корректно

                                parametr_name=word_list[i]
                                i += 1
                                if (i == len(word_list)):
                                    print('Ошибка! Пусто после имени параметра системы (\'',word_list[i-1],'\')')
                                    return [False]
                                else:
                                    if(word_list[i] != '/'):
                                        print('Ошибка! Встречен символ \'', word_list[i], '\' после имени параметра системы (\'',word_list[i-1],'\')')
                                        return [False]
                                    else:
                                        i += 1  ##############################################################################################
                                        if (i == len(word_list)):
                                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                            return [False]
                                        else:
                                            returned_result = checkId(word_list[i])
                                            if(not returned_result[0]):
                                                if(len(returned_result) != 1):
                                                    if(returned_result[1] == None):#################Выражденный случай
                                                        print('Ошибка! Пусто после \'',word_list[i-1],'\'')###########Выражденный случай
                                                        return [False]#################Выражденный случай
                                                    else:#################Выражденный случай
                                                        print('Ошибка! Встречен символ \'', returned_result[1],'\' после id параметра системы \'',returned_result[2], '\'')
                                                        return [False]
                                                else:
                                                    print('Ошибка! Встречен символ \'', word_list[i][0], '\'после \'',word_list[i - 1],'\' (id параметра системы должен состоять только из цифр)')  # встречен word_list[i] после word_list[i-1]
                                                    #print('Ошибка! Id параметра системы должен состоять только из цифры (\'',word_list[i],'\')') # встречен word_list[i] после word_list[i-1]
                                                    return [False]
                                            else:# Id параметра системы корректный
                                                parametr_id = word_list[i]
                                                i += 1
                                                if (i == len(word_list)):
                                                    print('Ошибка! Пусто после id параметра системы (\'',word_list[i-1],'\')')
                                                    return [False]
                                                else:
                                                    if(word_list[i] != ':'):
                                                        print('Ошибка! Встречен символ \'', word_list[i], '\' после id параметра системы (\'',word_list[i],'\')')
                                                        return [False]
                                                    else:
                                                        i += 1
                                                        if (i == len(word_list)):
                                                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                            return [False]
                                                        else:
                                                            returned_result = checkValue(word_list[i])
                                                            if (not returned_result[0]):
                                                                if (len(returned_result) != 1):
                                                                    print('Ошибка! Встречен символ \'', returned_result[1],'\' после значения параметра системы \'',returned_result[2], '\'')
                                                                    return [False]
                                                                else:
                                                                    print('Ошибка! Встречен символ \'', word_list[i][0],'\' после \'', word_list[i - 1],'\' (Значение параметра системы должно начинаться с буквы латинского алфавита либо состоять из цифр)')
                                                                    return [False]
                                                            else:# Значение параметра системы корректно
                                                                system_parameters[parametr_name, parametr_id] = word_list[i] ##Добавление нового параметра системы
                                                                i += 1
                                                                if (i == len(word_list)):
                                                                    print('Ошибка! Пусто после значения параметра системы (\'',word_list[i-1],'\')')
                                                                    return [False]
                                                                else:
                                                                    if(word_list[i] == '.'):
                                                                        system = [system_name, system_parameters]
                                                                        break
                                                                    elif(word_list[i] != ','):
                                                                        print('Ошибка! Встречен символ \'',word_list[i],'\' после значения параметра системы (\'',word_list[i-1],'\')')
                                                                        return [False]
                                                                    else:
                                                                        i +=1
                                                                        if(i == len(word_list)):
                                                                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                                            return [False]
                                                                        else:
                                                                            continue

                        for j in range(i+1):
                            del word_list[0]
                        return [True, word_list, system]

def checkUser(word_list):
    i = 0
    users = []
    while(True):# Проход по пользователям
        if(i == len(word_list)):
            if(i == 0):
                print('Ошибка! Пусто после \'.\'')
                return [False]
            else:
                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                return [False]
        else:# Не конец строки
            returned_result = checkName(word_list[i])
            if(not returned_result[0]):
                if (len(returned_result) != 1):
                    print('Ошибка! Встречен символ \'', returned_result[1], '\' после имени пользователя \'',returned_result[2], '\'')
                    return [False]
                else:
                    if(i == 0):
                        print('Ошибка! Встречен символ \'', word_list[i][0], '\' после \'.\' (Имя пользователя должно начинаться с буквы латинского алфавита)')
                        return [False]
                    else:
                        print('Ошибка! Встречен символ \'',word_list[i][0],'\' после \'',word_list[i-1],'\' (Имя пользователя должно начинаться с буквы латинского алфавита (\'', word_list[i], '\')')
                        return [False]
            else:# Имя пользователя корректно
                user_name = word_list[i]
                i += 1
                if(i == len(word_list)):
                    print('Ошибка! Пусто после имени пользователя (\'',word_list[i-1],'\')')
                    return [False]
                else:# Не конец строки после имени пользователя
                    if(word_list[i] != '/'):
                        print('Ошибка! Встречен символ \'', word_list[i], '\' после имени пользователя  (\'', word_list[i - 1],'\')')
                        return [False]
                    else:# после имени пользователя стоит разделитель
                        i += 1
                        if(i == len(word_list)):
                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                            return [False]
                        else:# Не пусто после разделителя
                            returned_result = checkId(word_list[i])
                            if(not returned_result[0]):
                                if (len(returned_result) != 1):
                                    print('Ошибка! Встречен символ \'', returned_result[1],'\' после id пользователя \'', returned_result[2], '\'')
                                    return [False]
                                else:
                                    print('Ошибка! Встречен символ \'', word_list[i][0], '\'после \'', word_list[i - 1],'\' (id пользователя должен состоять только из цифр)')  # встречен word_list[i] после word_list[i-1]
                                    #print('Ошибка! Id пользователя должен состоять только из цифры (\'', word_list[i],'\')')  # встречен word_list[i] после word_list[i-1]
                                    return [False]
                            else:# Корректный id пользователя
                                user_id = word_list[i]
                                i += 1
                                user_accessparameters = {}
                                if(i == len(word_list)):
                                    print('Ошибка! Пусто после id пользователя (\'', word_list[i-1],'\')')
                                    return [False]
                                else:# Не конец строки после id пользователя
                                    if(word_list[i] == '='):# Если у текущего пользователя существуют параметры доступа
                                        #print('=')##########################################################################
                                        i += 1
                                        if(i == len(word_list)):
                                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                            return [False]
                                        else:# неконец строки после разделителя
                                            while(True):
                                                returned_result = checkId(word_list[i])
                                                if(not returned_result[0]):
                                                    if (len(returned_result) != 1):
                                                        print('Ошибка! Встречен символ \'', returned_result[1],'\' после id параметра доступа пользователя \'', returned_result[2], '\'')
                                                        return [False]
                                                    else:
                                                        print('Ошибка! Встречен символ \'', word_list[i][0],'\'после \'', word_list[i - 1],'\' (id параметра доступа пользователя должен состоять только из цифр)')  # встречен word_list[i] после word_list[i-1]
                                                        #print('Ошибка! Id параметра доступа пользователя должен состоять только из цифры (\'',word_list[i],'\')')  # встречен word_list[i] после word_list[i-1]
                                                        return [False]
                                                else:# Корректны id параметра пользователя
                                                    parameter_id = word_list[i]
                                                    i += 1
                                                    if(i == len(word_list)):
                                                        print('Ошибка! Пусто после id параметра системы (\'',word_list[i-1],'\')')
                                                        return False
                                                    else:# Не конец строки после id параметра доступа
                                                        if(word_list[i] != ':'):
                                                            print('Ошибка! Встречен символ \'', word_list[i],'\' после id параметра доступа пользователя \'', word_list[i-1], '\'')
                                                            return [False]
                                                        else:# После id параметра доступа стоит разделитель
                                                            i += 1
                                                            if(i == len(word_list)):
                                                                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                                return [False]
                                                            else:# Не конец строки после разделителя
                                                                returned_result = checkAccessId(word_list[i])
                                                                if(not returned_result[0]):
                                                                    if(len(returned_result) != 1):
                                                                        print('Ошибка! Встречен символ \'',(word_list[i])[0],'\' после \'',word_list[i-1],'\'')
                                                                        return [False]
                                                                    else:
                                                                        print('Ошибка! Встречен символ \'',returned_result[1],'\' после идентификатора доступа \'',returned_result[2],'\'')
                                                                        return [False]
                                                                else:# Корректный идентификатор доступа
                                                                    user_accessparameters[parameter_id] = word_list[i]
                                                                    i += 1
                                                                    if(i == len(word_list)):
                                                                        print('Ошибка! Пусто после параметров доступа пользователя')
                                                                        return [False]
                                                                    else:# Не конец строки после идентификатора доступа
                                                                        if(word_list[i] == ','):# Будут параметры доступа
                                                                            i += 1
                                                                            if(i == len(word_list)):
                                                                                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                                                return [False]
                                                                            else:# Не пусто после разделителя параметров доступа
                                                                                continue
                                                                        elif(word_list[i] == ';' or word_list[i] == '.'):# Текущий пользователь закончился
                                                                            break;
                                                                        else:
                                                                            print('Ошибка! Встречен символ \'',word_list[i],'\' после параметров доступа пользователя \'',word_list[i-1],'\'')
                                                                            return [False]
                                    elif (not (word_list[i] == ';' or word_list[i] == '.')):# Текущий пользователь не имеет параметров доступа
                                            print('Ошибка! Встречен символ \'', word_list[i],'\' после параметров id пользователя \'',word_list[i - 1], '\'')
                                            return [False]

        if(word_list[i] == ';'):# Будут еще пользователи
            users.append([user_name, user_id, user_accessparameters])
            i += 1
            continue
        else:# Это был последний пользователь
            users.append([user_name, user_id, user_accessparameters])
            break
    for j in range(i+1):
        del word_list[0]
    return [True, word_list,users]

def checkObject(word_list):
    i = 0
    objects = []
    while(True):# Проход по объектам
        if(i == len(word_list)):
            if(i == 0):
                print('Ошибка! Пусто после \'.\'')
                return [False]
            else:
                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                return [False]
        else:
            returned_result = checkName(word_list[i])
            if(not returned_result[0]):
                if (len(returned_result) != 1):
                    print('Ошибка! Встречен символ \'', returned_result[1], '\' после имени объекта системы \'',returned_result[2], '\'')
                    return [False]
                else:
                    if(i == 0):
                        print('Ошибка! Встречен символ \'', word_list[i][0], '\' после \'.\' (Имя объекта системы должно начинаться с буквы латинского алфавита')
                        return [False]
                    else:
                        print('Ошибка! Встречен символ \'', word_list[i][0], '\' после \'', word_list[i - 1],'\' (Имя объекта системы должно начинаться с буквы латинского алфавита')
                        return [False]
            else:
                object_name = word_list[i]
                i += 1
                if(i == len(word_list)):
                    print('Ошибка! Пусто после имени объекта системы \'',word_list[i-1],'\'')
                    return [False]
                else:
                    if(word_list[i] != '/'):
                        print('Ошибка! Встречен символ \'', word_list[i], '\' после имени пользователя  (\'',word_list[i - 1], '\')')
                        return [False]
                    else:
                        i += 1
                        if(i == len(word_list)):
                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                            return [False]
                        else:
                            returned_result = checkId(word_list[i])
                            if(not returned_result[0]):
                                if (len(returned_result) != 1):
                                    print('Ошибка! Встречен символ \'', returned_result[1],'\' после id объекта системы \'', returned_result[2], '\'')
                                    return [False]
                                else:
                                    print('Ошибка! Встречен символ \'', word_list[i][0], '\'после \'', word_list[i - 1],'\' (id объекта должен состоять только из цифр)')  # встречен word_list[i] после word_list[i-1]
                                    return [False]
                            else:
                                object_id = word_list[i]
                                i += 1
                                if(i == len(word_list)):
                                    print('Ошибка! Пусто после id объекта системы (\'',word_list[i-1],'\')')
                                    return [False]
                                else:
                                    if(word_list[i] != '='):
                                        print('Ошибка! Встречен символ \'',word_list[i],'\' после id объекта системы \'',word_list[i-1],'\'')
                                        return [False]
                                    else:
                                        i += 1
                                        if(i == len(word_list)):
                                            print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                            return [False]
                                        else:
                                            object_spec = {}
                                            while(True):
                                                returned_result = checkParameterObject(word_list[i])
                                                if(not returned_result[0]):######################       Если слово не увляется параметром объекта
                                                    print('Ошибка! Встречено слово \'',word_list[i],'\' после \'',word_list[i-1],'\' (Параметрами объекта могут быть только уже зарезервированные слова)')
                                                    return [False]
                                                else:
                                                    tmp_parameter = word_list[i]
                                                    i += 1
                                                    if(i == len(word_list)):
                                                        print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                        return [False]
                                                    else:
                                                        if(word_list[i] != ':'):
                                                            print('Ошибка! Встречен символ\'',word_list[i],'\' после id объекта системы \'',word_list[i-1],'\'')
                                                            return [False]
                                                        else:
                                                            i += 1
                                                            if(i == len(word_list)):
                                                                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                                return [False]
                                                            else:
                                                                returned_result = checkValue(word_list[i])
                                                                if (not returned_result[0]):
                                                                    if (len(returned_result) != 1):
                                                                        print('Ошибка! Встречен символ \'', returned_result[1],'\' после значения параметра объекта \'',returned_result[2], '\'')
                                                                        return [False]
                                                                    else:
                                                                        print('Ошибка! Встречен символ \'', word_list[i][0],'\' после \'', word_list[i - 1],'\' (Значение параметра объекта должно начинаться с буквы латинского алфавита либо состоять из цифр)')
                                                                        return [False]
                                                                else:
                                                                    object_spec[tmp_parameter] = word_list[i]
                                                                    i += 1
                                                                    if(i == len(word_list)):
                                                                        #print('Ошибка! Пусто после значение параметра объекта \'',word_list[i-1],'\'')
                                                                        #return [False]
                                                                        break
                                                                    else:
                                                                        if (word_list[i] == ','):  # Будут параметры объекта
                                                                            i += 1
                                                                            if (i == len(word_list)):
                                                                                print('Ошибка! Пусто после \'',word_list[i-1],'\'')
                                                                                return [False]
                                                                            else:  # Не пусто после разделителя параметров доступа
                                                                                continue
                                                                        elif (word_list[i] == ';'):  # Текущий объект закончился
                                                                            break
                                                                        else:
                                                                            print('Ошибка! Встречен символ \'', word_list[i],'\' после параметров характеристик объекта \'',word_list[i - 1], '\'')
                                                                            return [False]
                                            if(i == len(word_list)):
                                                objects.append([object_name, object_id, object_spec])
                                                break
                                            elif(word_list[i] == ';'):
                                                objects.append([object_name, object_id, object_spec])
                                                i += 1
                                                continue
    for j in range(i):
        del word_list[0]
    return [True, word_list, objects]




digit_list = list('0123456789')
letter_list = list(strlib.ascii_letters)
symbol_list = letter_list + digit_list
objparameters_list = ['size', 'visible']
accessid_list = ['r', 'w', 'a', 'e']




string = 'k=dds/123:a33,dds/123:ds.keka/12=8:e;kukareka/12=8:r,23:e.assdsads22/211=size:1,visible:true;ads22/21=size:0,visible:true'
#string = input('Введите строку: ')
string = string.replace(' ', '')
flag_to_append_dot = False
if(string[len(string)-1] != '.'):
    flag_to_append_dot = True
    string += '.'
#print(string)
words = []
tmp = 0
for i in range(len(string)):# Разбиение строки на подстроки в список, для удобства работы с ними
    char = string[i]
    if(char == '=' or char == '/' or char == ',' or char == ';' or char == ':' or char == '.'):
        words.append(string[tmp:i])
        words.append(string[i])
        tmp += len(string[tmp:i])+1
if(flag_to_append_dot):
    del words[len(words)-1]
#print(words)
#words = ['k', '=', 'f', '/', '123', ':', '33', ',', 'a1', '/', '123', ':', 'ds', '.', 'keka']
words = list(filter(None, words))# Удаление пустых строк из списка
original_words = words
checked_system = checkSystem(words)
if(checked_system[0]):
    system = checked_system[2]
    #print(returned_result)
    words = checked_system[1]
    checked_users = checkUser(words)
    if(checked_users[0]):
        #print(returned_result)
        users = checked_users[2]
        words = checked_users[1]
        checked_objects = checkObject(words)
        if(checked_objects[0]):
            #print(returned_result)
            objects = checked_objects[2]
            print('Параметры системы:\n', system, '\n\nПараметры пользователей:')

            for us in users:
                print(us)
            print('\n\nПараметры объектов:')
            for ob in objects:
                print(ob)

            allobjects = []
            for us in users:
                for object_ids in us[2]:
                    if(not object_ids in allobjects):
                        allobjects.append(object_ids)
                for object_ids in objects:
                    if(not object_ids[1] in allobjects):
                        allobjects.append(object_ids[1])

            objects_count = len(allobjects)
            users_count = len(users)







            output = [['None']*(objects_count+1) for i in range(users_count+1)]


            for i in range(users_count):
                output[i+1][0] = users[i][0]
            for i in range(objects_count):
                output[0][i+1] = allobjects[i]
            output[0][0] = ':)'





            for i in range(users_count):
                for j in range(objects_count):
                    if(allobjects[j] in users[i][2]):
                        output[i+1][j+1] = users[i][2][allobjects[j]]
                        #print(output)

            print('\n\nМатрица доступов:')
            printMatrix(output)
            input()













